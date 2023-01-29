using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameHelper {
    public static List<Player> getAlivePlayers(Player[] players, int playerCount) {
        List<Player> alivePlayers = new List<Player>();

        for (int i = 0; i < playerCount; i++) {
            if (players[i].hp > 0) {
                alivePlayers.Add(players[i]);
            }
        }

        return alivePlayers;
    }

    public static bool isEndGame(Player[] players, int playerCount, int round) {
        if (round >= 10) {
            return true;
        }

        return getAlivePlayers(players, playerCount).Count <= 1;
    }

    public static void endGame(Player[] players, int playerCount, int round) {
        List<Player> alivePlayer = getAlivePlayers(players, playerCount);
        Debug.Log("Game ended, winner is " + alivePlayer[0].id + ":" + alivePlayer[0].name + " after " + round + " rounds");
    }

    public static IEnumerator startPreparePhase(Player[] players, int playerCount) {
        Debug.Log("Prepare for combat");

        yield return new WaitForSeconds(5);

        Debug.Log("Prepare phase over");

        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PLAY;
        SharedGameValues.phaseStatus = 0;
    }

    public static IEnumerator startPlayPhase(Player[] players, int playerCount) {
        Debug.Log("Combat in progress");

        yield return new WaitForSeconds(5);

        Debug.Log("Combat ended");

        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PREPARE;
        SharedGameValues.phaseStatus = 0;
    }
}
