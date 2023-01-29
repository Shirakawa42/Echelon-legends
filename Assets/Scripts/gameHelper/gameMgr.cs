using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
        
        for (int i = 0; i < playerCount; i++) {
            if (!players[i].shop.GetComponent<ShopManager>().isLocked) {
                players[i].shop.GetComponent<ShopManager>().createNewShop(players[i]);
            }
        }

        yield return new WaitForSeconds(5);

        Debug.Log("Prepare phase over");

        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PLAY;
        SharedGameValues.phaseStatus = 0;
    }

    public static IEnumerator startPlayPhase(Player[] players, int playerCount) {
        Debug.Log("Combat in progress");

        yield return new WaitForSeconds(5);

        Debug.Log("Combat ended");


        if (SharedGameValues.round <= 5) {
            updateBaseIncome();
        }
        
        for (int i = 0; i < playerCount; i++) {
            players[i].addXp(2);
            players[i].addGold();
        }

        SharedGameValues.gamePhase = (int)SharedGameValues.GamePhase.PREPARE;
        SharedGameValues.phaseStatus = 0;

        SharedGameValues.round++;
    }

    public static void updateBaseIncome() {
        SharedGameValues.baseIncome = 0;

        if (SharedGameValues.round == 1) {
            SharedGameValues.baseIncome = 2;
        } else if (SharedGameValues.round == 2) {
            SharedGameValues.baseIncome = 2;
        } else if (SharedGameValues.round == 3) {
            SharedGameValues.baseIncome = 3;
        } else if (SharedGameValues.round == 4) {
            SharedGameValues.baseIncome = 4;
        } else if (SharedGameValues.round >= 5) {
            SharedGameValues.baseIncome = 5;
        }
    }
}
