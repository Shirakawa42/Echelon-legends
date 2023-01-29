using System.Collections.Generic;
using UnityEngine;

public static class ShopHelper {
    public static void initializeShopPools(Dictionary<int,int>[] shopPools, GameObject[] units, int tier) {
        shopPools[tier] = new Dictionary<int,int>();

        for (int i = 0; i < units.Length; i++) {
            shopPools[tier][units[i].GetComponent<BasicBehavior>().id] = 20;
        }
    }

    // Unit place is used to determined if unit is coming from bench or board.
    // 0 = bench
    // 1 = board
    public static int sellUnit(Player player, GameObject unit, int unitPlace) {
        player.gold += unit.GetComponent<UnitStats>().price;

        SharedGameValues.shopPools[unit.GetComponent<UnitStats>().tier][unit.GetComponent<UnitStats>().id] += 1;

        if (unitPlace == 0) {
            player.benchUnits.Remove(unit);
        } else {
            player.fieldUnits.Remove(unit);
        }

        return 0;
    }

    public static int buyUnit(Player player, GameObject unit) {
        int price = unit.GetComponent<UnitStats>().price;

        if (player.gold < price && player.benchUnits.Count >= SharedGameValues.benchMaxSize) {
            Debug.Log("Not enough gold or bench is full");
            return -1;
        }

        player.gold -= price;
        player.benchUnits.Add(unit);

        Debug.Log("Bought unit " + unit.GetComponent<UnitStats>().id + " for " + price + " gold");

        return 0;
    }
}