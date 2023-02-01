using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;

    private void OnMouseUp() {
        leftClickNum++;

        if (leftClickNum == 1 && isTimeCheckAllowed) {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleLeftClick());
        }
    }
    private IEnumerator DetectDoubleLeftClick() {
        isTimeCheckAllowed = false;
        
        while (Time.time < firstLeftClickTime + timeBetweenLeftClick) {
            if (leftClickNum == 2) {
                ShopHelper.sellUnit(this.gameObject.GetComponentInParent<Player>(), this.gameObject);
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }
}
