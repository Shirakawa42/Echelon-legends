using System.Collections.Generic;
using UnityEngine;

public static class ShopHelper {
    public static void createShopPools(GameObject[] units, Dictionary<int,int> pool, int price) {
        for (int i = 0; i < units.Length; i++) {
            pool[units[i].GetComponent<BasicBehavior>().id] = 20;
        }
    }
}