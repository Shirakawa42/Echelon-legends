using System.Collections.Generic;
using UnityEngine;

public static class SharedGameValues {
    public static float currentTime = 0f;
    public static int benchMaxSize = 8;
    public static int round;
    public static int phaseStatus = 0;
    public static int gamePhase;
    public enum GamePhase {
        PREPARE,
        PLAY,
        END
    }

    public static Dictionary<int,GameObject> Units = new Dictionary<int,GameObject>();
    public static Dictionary<int,int>[] shopPools = new Dictionary<int,int>[5];
}

public class GameManager : MonoBehaviour {
    public GameObject[] T1Units;
    public GameObject[] T2Units;
    public GameObject[] T3Units;
    public GameObject[] T4Units;
    public GameObject[] T5Units;
    public const int playerCount = 2;
    public Player[] players = new Player[playerCount];

    // Start is called before the first frame update
    void Start() {
        SharedGameValues.round = 0;
        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PREPARE;

        ShopHelper.initializeShopPools(SharedGameValues.shopPools, T1Units, 0);
        ShopHelper.initializeShopPools(SharedGameValues.shopPools, T2Units, 1);
        ShopHelper.initializeShopPools(SharedGameValues.shopPools, T3Units, 2);
        ShopHelper.initializeShopPools(SharedGameValues.shopPools, T4Units, 3);
        ShopHelper.initializeShopPools(SharedGameValues.shopPools, T5Units, 4);
        UnitHelper.initializeTotalUnits(SharedGameValues.Units, T1Units);
        UnitHelper.initializeTotalUnits(SharedGameValues.Units, T2Units);
        UnitHelper.initializeTotalUnits(SharedGameValues.Units, T3Units);
        UnitHelper.initializeTotalUnits(SharedGameValues.Units, T4Units);
        UnitHelper.initializeTotalUnits(SharedGameValues.Units, T5Units);
        PlayerHelper.initializePlayers(players, playerCount);
    }

    // FixedUpdate is called 60 times per second.
    void FixedUpdate() {
        if (SharedGameValues.phaseStatus == 0) {
            Debug.Log("Start phase" + SharedGameValues.round);
            SharedGameValues.phaseStatus = 1;

            switch (SharedGameValues.gamePhase) {
                case (int)SharedGameValues.GamePhase.PREPARE:
                    Debug.Log("Prepare phase");

                    if (GameHelper.isEndGame(players, playerCount, SharedGameValues.round)) {
                        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.END;
                        SharedGameValues.phaseStatus = 0;
                    } else {
                        StartCoroutine(GameHelper.startPreparePhase(players, playerCount));
                    }
                    
                    break;
                case (int)SharedGameValues.GamePhase.PLAY:
                    Debug.Log("Play phase");
                    StartCoroutine(GameHelper.startPlayPhase(players, playerCount));
                    break;
                case (int)SharedGameValues.GamePhase.END:
                    Debug.Log("End phase");

                    GameHelper.endGame(players, playerCount, SharedGameValues.round);
                    // Changer de scène pour tlm et faire un truc de fin
                    break;
            }

            SharedGameValues.round++;
            Debug.Log("End phase" + SharedGameValues.round);
        }
    
    }
}
