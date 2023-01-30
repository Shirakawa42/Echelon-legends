using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonMgr : MonoBehaviour
{
    public GameObject button;
    public int buttonIndex;
    public int unitID;
    public Player player;
    public void OnButtonPress() {
        if (unitID >= 0) {
            if (ShopHelper.buyUnit(player, unitID) != -1) {
                player.GetComponent<Player>().shop.GetComponent<ShopManager>().shop[buttonIndex] = -1;
                unitID = -1;
                button.SetActive(false);
            }
        }
    }
}
