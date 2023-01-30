using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshButtonMgr : MonoBehaviour
{
    public Player player;
    public Button refreshButton;

    public void OnButtonPress() 
    {
        player.GetComponent<Player>().shop.GetComponent<ShopManager>().refreshShop(player);
    }

    //refresh on each frame
    void Update()
    {
        if(player.gold < SharedGameValues.shopRefreshCost) {
            refreshButton.interactable = false;
        }else{
            refreshButton.interactable = true;
        }
    }
}
