using UnityEngine;
using System.Collections.Generic;

public class Team : MonoBehaviour {
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

public static class TeamHelper {
    public static void initializeTeams(Team[] teams, int teamCount) {
        for (int i = 0; i < teamCount; i++) {
            string teamName = "Team " + (i == 0 ? "Shaman" : "DK");

            GameObject teamGO = new GameObject(teamName);
            teams[i] = teamGO.AddComponent<Team>();
            teams[i].id = i;
            teams[i].hp = 100;
            teams[i].gold = 1;
            teams[i].streak = 0;
            teams[i].currentXp = 0;
            teams[i].level = 1;
        }
    }
}
