using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameHelper {
    public static List<Team> getAlivePlayers(Team[] teams, int teamCount) {
        List<Team> alivePlayers = new List<Team>();

        for (int i = 0; i < teamCount; i++) {
            if (teams[i].hp > 0) {
                alivePlayers.Add(teams[i]);
            }
        }

        return alivePlayers;
    }

    public static bool isEndGame(Team[] teams, int teamCount, int round) {
        if (round >= 10) {
            return true;
        }
        return getAlivePlayers(teams, teamCount).Count <= 1;
    }

    public static void endGame(Team[] teams, int teamCount, int round) {
        List<Team> alivePlayer = getAlivePlayers(teams, teamCount);
        Debug.Log("Game ended, winner is " + alivePlayer[0].id + ":" + alivePlayer[0].name + " after " + round + " rounds");
    }

    public static IEnumerator startPreparePhase(Team[] teams, int teamCount) {
        Debug.Log("Prepare for combat");

        yield return new WaitForSeconds(5);

        Debug.Log("Prepare phase over");

        SharedGameValues.phaseStatus = 0;
        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PLAY;
    }

    public static IEnumerator startPlayPhase(Team[] teams, int teamCount) {
        Debug.Log("Combat in progress");

        yield return new WaitForSeconds(5);

        Debug.Log("Combat ended");

        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PREPARE;
        SharedGameValues.phaseStatus = 0;
    }
}
