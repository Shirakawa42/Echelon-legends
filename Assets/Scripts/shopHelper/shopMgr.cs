using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public bool isLocked = false;
    public int[] shop;

    public void createNewShop(Player player) {
        shop = new int[SharedGameValues.shopMaxSize];

        for (int i = 0; i < SharedGameValues.shopMaxSize; i++) {
            int tier  = ShopHelper.getRandomShopTierForCurrentLevel(player.level) - 1;
            var unitsTier = SharedGameValues.shopPools[tier];

            int[] range = new int[unitsTier.Count * 29];
            int rangeLength = 0;

            foreach (var tierUnits in unitsTier) {
                for (int j = 0; j < tierUnits.Value; j++) {
                    range[rangeLength] = tierUnits.Key;
                    rangeLength++;
                }
            }

            shop[i] = range[Random.Range(0, rangeLength)];
        }

        player.updateChampSelectorButtons(shop);
    }

    public void refreshShop(Player player) {
        if (player.gold >= SharedGameValues.shopRefreshCost) {
            player.removeGold(SharedGameValues.shopRefreshCost);
            createNewShop(player);
        }
    }
}

public static class ShopHelper {
    static int getRandomShopTierNumber(int number, int tier1, int tier2, int tier3, int tier4, int tier5) {
        if (number <= tier1) {
            return 1;
        } else if (number <= tier1 + tier2) {
            return 2;
        } else if (number <= tier1 + tier2 + tier3) {
            return 3;
        } else if (number <= tier1 + tier2 + tier3 + tier4) {
            return 4;
        } else {
            return 5;
        }
    }

    public static int getRandomShopTierForCurrentLevel(int level) {
        if (level <= 2) {
            return 1;
        }

        int number = Random.Range(1, 101);
        
        switch (level) {
            case 3:
                return getRandomShopTierNumber(number, 75, 25, 0, 0, 0);
            case 4:
                return getRandomShopTierNumber(number, 55, 30, 15, 0, 0);
            case 5:
                return getRandomShopTierNumber(number, 45, 33, 20, 2, 0);
            case 6:
                return getRandomShopTierNumber(number, 25, 40, 30, 5, 0);
            case 7:
                return getRandomShopTierNumber(number, 19, 30, 35, 15, 1);
            case 8:
                return getRandomShopTierNumber(number, 16, 20, 35, 25, 4);
            default:
                return getRandomShopTierNumber(number, 9, 15, 30, 30, 16);
        };
    }
    public static void initializeShopPools(Dictionary<int,int>[] shopPools, GameObject[] units, int tier, int poolSize) {
        shopPools[tier] = new Dictionary<int,int>();

        for (int i = 0; i < units.Length; i++) {
            shopPools[tier][units[i].GetComponent<UnitStats>().id] = poolSize;
        }
    }

    public static int sellUnit(Player player, GameObject unit) {
        player.addGold(unit.GetComponent<UnitStats>().price);

        SharedGameValues.shopPools[unit.GetComponent<UnitStats>().tier - 1][unit.GetComponent<UnitStats>().id] += 1;

        if (unit.GetComponent<UnitStats>().onBench) {
            player.benchUnits[unit.GetComponent<UnitStats>().benchCoord] = null;
            player.unitOnBench--;
        } else {
            player.boardUnits[unit.GetComponent<UnitStats>().boardCoord.y][unit.GetComponent<UnitStats>().boardCoord.x] = null;
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

        player.removeGold(price);

        Debug.Log("Bought unit " + unit.GetComponent<UnitStats>().id + " for " + price + " gold");

        return 0;
    }
}