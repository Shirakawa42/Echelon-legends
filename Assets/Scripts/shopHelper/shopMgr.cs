using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public bool isLocked = false;
    public int[] shop = new int[SharedGameValues.shopMaxSize];

    public void createNewShop(Player player) {
        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            shop[i] = ShopHelper.getRandomShopTierForCurrentLevel(player.level);
        }
    }

    public void refreshShop(Player player) {
        if (player.gold >= SharedGameValues.shopRefreshCost) {
            player.gold -= SharedGameValues.shopRefreshCost;
            createNewShop(player);
        }
    }
}

public static class ShopHelper {
    static int[] buildIntArray(int tier1, int tier2, int tier3, int tier4, int tier5) {
        int[] range = new int[tier1 + tier2 + tier3 + tier4 + tier5];

        int i = 0;

        for (;i < tier1; i++) {
            range[i] = 1;
        }

        for (; i < tier2; i++) {
            range[i] = 2;
        }

        for (; i < tier3; i++) {
            range[i] = 3;
        }

        for (; i < tier4; i++) {
            range[i] = 4;
        }

        for (; i < tier5; i++) {
            range[i] = 5;
        }

        return range;
    }
    public static int getRandomShopTierForCurrentLevel(int level) {
        if (level <= 2) {
            return 1;
        }

        int[] range;
        
        switch (level) {
            case 3:
                range = new int[] {1, 1, 1, 2};
                break;
            case 4:
                range = buildIntArray(55, 30, 15, 0, 0);
                break;
            case 5:
                range = buildIntArray(45, 33, 20, 2, 0);
                break;
            case 6:
                range = buildIntArray(25, 40, 30, 5, 0);
                break;
            case 7:
                range = buildIntArray(19, 30, 35, 15, 1);
                break;
            case 8:
                range = buildIntArray(16, 20, 35, 25, 4);
                break;
            default:
                range = buildIntArray(9, 15, 30, 30, 16);
                break;
        };

        return range[Random.Range(0, range.Length)];
    }
    public static void initializeShopPools(Dictionary<int,int>[] shopPools, GameObject[] units, int tier, int poolSize) {
        shopPools[tier] = new Dictionary<int,int>();

        for (int i = 0; i < units.Length; i++) {
            shopPools[tier][units[i].GetComponent<BasicBehavior>().id] = poolSize;
        }
    }

    public static int sellUnit(Player player, GameObject unit) {
        player.gold += unit.GetComponent<UnitStats>().price;

        SharedGameValues.shopPools[unit.GetComponent<UnitStats>().tier - 1][unit.GetComponent<UnitStats>().id] += 1;

        if (unit.GetComponent<UnitStats>().onBench) {
            player.benchUnits[unit.GetComponent<UnitStats>().benchCoord] = null;
            player.unitOnBench--;
        } else {
            player.boardUnits[unit.GetComponent<UnitStats>().boardCoord[0]][unit.GetComponent<UnitStats>().boardCoord[1]] = null;
        }

        unit.GetComponent<BasicBehavior>().selfDestroy();

        return 0;
    }

    public static int buyUnit(Player player, int id) {
        GameObject unit = SharedGameValues.Units[id];

        int price = unit.GetComponent<UnitStats>().price;

        if (player.gold < price || player.unitOnBench >= SharedGameValues.benchMaxSize) {
            Debug.Log("Not enough gold or bench is full");
            return -1;
        }

        bool assigned = false;

        for (int i = 0; i < SharedGameValues.benchMaxSize; i++) {
            if (player.benchUnits[i] == null) {
                player.benchUnits[i] = unit.GetComponent<BasicBehavior>().selfInstanciate();
                player.benchUnits[i].transform.parent = player.transform;
                player.benchUnits[i].GetComponent<UnitStats>().onBench = true;
                player.benchUnits[i].GetComponent<UnitStats>().benchCoord = i;
                player.unitOnBench++;
                assigned = true;
                break;
            }
        }

        if (!assigned) {
            Debug.Log("Bench is full");
            return -1;
        }

        player.gold -= price;

        Debug.Log("Bought unit " + unit.GetComponent<UnitStats>().id + " for " + price + " gold");

        return 0;
    }
}