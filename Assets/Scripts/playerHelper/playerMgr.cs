using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour {
    public int id;
    public int hp;
    public int streak;
    public int gold;
    public int xp;
    public int level;
    public int unitOnBench;
    public bool lastRoundWin;
    public GameObject shop;
    public GameObject[] benchUnits = new GameObject[SharedGameValues.benchMaxSize];
    public GameObject[][] boardUnits = new GameObject[SharedGameValues.benchMaxSize][];
    public GameObject canva;
    public GameObject map;

    public void updateChampSelectorButtons(int[] shop) {
        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            canva.GetComponent<CameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().unitID = shop[i];
            canva.GetComponent<CameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().button.GetComponentInChildren<Text>().text = SharedGameValues.Units[shop[i]].transform.name;
        }
    }

    public void activateChampSelectorButtons(int[] shop){
        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            canva.GetComponent<CameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().button.SetActive(true);
        }
    }

    public void initializeMap(GameObject map) {
        GameObject mapGO = Instantiate(map);
        mapGO.transform.parent = this.transform;
        mapGO.name = "map " + id;
        mapGO.GetComponent<MapMgr>().floor.transform.localScale = new Vector3(SharedGameValues.floorScale.x * SharedGameValues.benchMaxSize, SharedGameValues.floorScale.y, SharedGameValues.floorScale.z * 10);
        this.map = map;
    }

    public void initializeShop(Player player, string playerName) {
        GameObject shopGO = new GameObject("shop" + playerName);
        shopGO.transform.parent = this.transform;
        shopGO.AddComponent<ShopManager>();
        
        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            shopGO.GetComponent<ShopManager>().shop[i] = -1;
        }

        player.shop = shopGO;
    }

    public void initializeCanvas(GameObject canvas) {
        canva = Instantiate(canvas);
        canva.gameObject.name = "canvas " + id;
        canva.transform.SetParent(gameObject.transform);
        canva.GetComponent<CameraMgr>().health.GetComponent<HealthDisplay>().healthText.text = hp.ToString();
        canva.GetComponent<CameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
        canva.GetComponent<CameraMgr>().healthBar.GetComponent<HealthBar>().SetMaxHealth(hp);

        for (int i = 0; i < SharedGameValues.shopMaxSize; i++ ) {
            canva.GetComponent<CameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().player = this;
        }

        canva.GetComponent<CameraMgr>().refresh.GetComponent<RefreshButtonMgr>().player = this;
        
    }

    public void addRoundGold() {
        int interest = (int)Math.Floor(gold*0.1f);

        gold += interest > 5 ? 5 : interest;
        gold += SharedGameValues.baseIncome;

        if (lastRoundWin) {
            gold += 1;
        }

        int absStreak = Math.Abs(streak);

        if (absStreak == 2 || absStreak == 3) {
            gold += 1;
        } else if (absStreak == 4) {
            gold += 2;
        } else if (absStreak >= 5) {
            gold += 3;
        }

        canva.GetComponent<CameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
    }

    public void addGold(int value) {
        gold += value;
        canva.GetComponent<CameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
    }
    
    public void removeGold(int value) {
        gold -= value;
        canva.GetComponent<CameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
    }

    public void addXp(int addedXp) {
        if (level >= SharedGameValues.levelMax) {
            return ;
        }

        xp += addedXp;

        if (xp >= SharedGameValues.xpPerLevel[level-1]) {
            xp -= SharedGameValues.xpPerLevel[level-1];
            level++;
        }
    }

    public void buyXp() {
        if (gold >= SharedGameValues.buyXpCost) {
            gold -= SharedGameValues.buyXpCost;
            addXp(SharedGameValues.buyXpAmountGiven);
        }
    }

    // // Instantiate bench units 
    // var benchUnit = Instantiate(T1Units[0]);

    // benchUnit.transform.parent = transform;
    // benchUnit.transform.localPosition = new Vector3(15, 15, 0);
    // benchUnit.transform.localRotation = Quaternion.identity; // @TODO: set rotation


    // teams[i].benchUnits.Add(benchUnit);

    // // Instantiate field units
    // var fieldUnit = Instantiate(T1Units[0]);

    // benchUnit.transform.parent = transform;
    // fieldUnit.transform.localPosition = new Vector3(15, 15, 0);
    // fieldUnit.transform.localRotation = Quaternion.identity; // @TODO: set rotation

    // teams[i].fieldUnits.Add(fieldUnit);
}

public static class PlayerHelper {
    public static void initializePlayers(Player[] players, int playerCount, GameObject canvas, GameObject map) {
        for (int i = 0; i < playerCount; i++) {
            string playerName = "Player " + i.ToString();

            GameObject PlayerGO = new GameObject(playerName);
            players[i] = PlayerGO.AddComponent<Player>();
            players[i].id = i;
            players[i].hp = 100;
            players[i].gold = 1;
            players[i].streak = 0;
            players[i].xp = 0;
            players[i].level = 1;
            players[i].unitOnBench = 0;

            for (int j = 0; j < SharedGameValues.benchMaxSize; j++) {
                players[i].benchUnits[j] = null;
                players[i].boardUnits[j] = new GameObject[4];
                for (int k = 0; k < 4; k++) {
                    players[i].boardUnits[j][k] = null;
                }
            }

            players[i].initializeMap(map);
            players[i].initializeShop(players[i], playerName);
            players[i].initializeCanvas(canvas);

            PlayerGO.transform.position = new Vector3(i * 20, 0, 0);
        }
    }
}
