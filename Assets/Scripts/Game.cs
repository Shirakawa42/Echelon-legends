using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] T1Units;
    public Dictionary<int,int> poolT1Units = new Dictionary<int,int>();
    public const int teamCount = 2;
    private int round;
    private Team[] teams = new Team[teamCount];
    private int gamePhase;
    enum GamePhase {
        PREPARE,
        PLAY,
        END
    }

    // Start is called before the first frame update
    void Start() {
        round = 0;
        gamePhase = (int)GamePhase.PREPARE;

        ShopHelper.createShopPools(T1Units, poolT1Units, 1);
        TeamHelper.initializeTeams(teams, teamCount);
    }

    // Update is called once per frame
    void Update() {
        switch (gamePhase) {
            case (int)GamePhase.PREPARE:
                break;
            case (int)GamePhase.PLAY:
                break;
            case (int)GamePhase.END:
                break;
        }
    }
}
