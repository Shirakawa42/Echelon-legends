using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    public int id;
    public int hp;
    public int streak;
    public int gold;
    public int currentXp;
    public int level;
    public List<int> currentShop = new List<int>();
    public List<GameObject> benchUnits = new List<GameObject>();
    public List<GameObject> fieldUnits = new List<GameObject>();

    
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
            players[i].currentXp = 0;
            players[i].level = 1;
        }
    }
}
