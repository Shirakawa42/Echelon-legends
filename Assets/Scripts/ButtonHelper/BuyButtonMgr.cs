using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonMgr : MonoBehaviour
{
    public int buttonIndex;
    public int unitID;
    public Player player;
    public void OnButtonPress() {
        ShopHelper.buyUnit(player, unitID);
    }
}
