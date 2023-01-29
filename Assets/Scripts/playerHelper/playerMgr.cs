using UnityEngine;
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

    public void updateChampSelectorButtons(int[] shop) {
        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            canva.GetComponent<cameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().unitID = shop[i];
            // canva.GetComponent<cameraMgr>().champSelector.GetComponent<ChampSelectorManager>().ChampSelectButtons[i].GetComponent<BuyButtonMgr>().updateButton();
        }
    }

    public void initializeCamera(GameObject Camera) {
        canva = Instantiate(Camera);
        canva.gameObject.name = "camera "+id;
        canva.transform.SetParent(gameObject.transform);
        canva.GetComponent<cameraMgr>().health.GetComponent<HealthDisplay>().healthText.text = hp.ToString();
        canva.GetComponent<cameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
        canva.GetComponent<cameraMgr>().healthBar.GetComponent<HealthBar>().slider.value = hp;
        canva.GetComponent<cameraMgr>().healthBar.GetComponent<HealthBar>().slider.maxValue = hp;
    }

    public void addGold() {
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

        canva.GetComponent<cameraMgr>().goldDisplay.GetComponent<GoldDisplay>().goldText.text = gold.ToString();
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
    public static void initializePlayers(Player[] players, int playerCount) {
        for (int i = 0; i < playerCount; i++) {
            string PlayerName = "Player " + i.ToString();

            GameObject PlayerGO = new GameObject(PlayerName);
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

            GameObject shopGO = new GameObject("shop" + PlayerName);
            shopGO.transform.parent = PlayerGO.transform;
            shopGO.AddComponent<ShopManager>();
            players[i].shop = shopGO;
        }
    }
}
