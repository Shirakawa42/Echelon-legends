using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonMgr : MonoBehaviour
{
    public int buttonIndex;
    public int unitID;
    public void OnButtonPress() {
        ShopHelper.buyUnit(gameObject.GetComponentInParent<ChampSelectorManager>().GetComponentInParent<ShopManager>().GetComponentInParent<Player>(), unitID);
    }
}
