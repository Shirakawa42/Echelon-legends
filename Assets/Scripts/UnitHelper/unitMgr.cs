using System.Collections.Generic;
using UnityEngine;

public static class UnitHelper {
    public static void initializeTotalUnits(Dictionary<int,GameObject> totalUnits, GameObject[] tierUnits) {
        for (int i = 0; i < tierUnits.Length; i++) {
            totalUnits[tierUnits[i].GetComponent<UnitStats>().id] = tierUnits[i];
        }
    }
}
