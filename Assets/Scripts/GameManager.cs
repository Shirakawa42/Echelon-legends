using System.Collections.Generic;
using UnityEngine;

public static class SharedGameValues {
    public static float currentTime = 0f;
    public static int round;
    public static int phaseStatus = 0;
    public static int gamePhase;
    public enum GamePhase {
        PREPARE,
        PLAY,
        END
    }
}

public class GameManager : MonoBehaviour
{
    public GameObject[] T1Units;
    public Dictionary<int,int> poolT1Units = new Dictionary<int,int>();
    public const int teamCount = 2;
    public Team[] teams = new Team[teamCount];

    // Start is called before the first frame update
    void Start() {
        SharedGameValues.round = 0;
        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PREPARE;

        ShopHelper.createShopPools(T1Units, poolT1Units, 1);
        TeamHelper.initializeTeams(teams, teamCount);
    }

    // Update is called once per frame
    void Update() {
        if (SharedGameValues.phaseStatus == 0) {
            Debug.Log("Start phase" + SharedGameValues.round);
            SharedGameValues.phaseStatus = 1;

            switch (SharedGameValues.gamePhase) {
                case (int)SharedGameValues.GamePhase.PREPARE:
                    Debug.Log("Prepare phase");

                    if (GameHelper.isEndGame(teams, teamCount, SharedGameValues.round)) {
                        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.END;
                        SharedGameValues.phaseStatus = 0;
                    } else {
                        StartCoroutine(GameHelper.startPreparePhase(teams, teamCount));
                    }
                    
                    break;
                case (int)SharedGameValues.GamePhase.PLAY:
                    Debug.Log("Play phase");
                    StartCoroutine(GameHelper.startPlayPhase(teams, teamCount));
                    break;
                case (int)SharedGameValues.GamePhase.END:
                    Debug.Log("End phase");

                    GameHelper.endGame(teams, teamCount, SharedGameValues.round);
                    // Changer de scène pour tlm et faire un truc de fin
                    break;
            }

            SharedGameValues.round++;
            Debug.Log("End phase" + SharedGameValues.round);
        }
    
    }
}
