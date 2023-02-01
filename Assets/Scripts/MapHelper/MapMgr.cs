using UnityEngine;

public class MapMgr : MonoBehaviour {
    public GameObject floor;

    public void addUnitOnBench(GameObject unit, int coord) {
        unit.transform.localPosition = new Vector3(-7 + (coord * 2), unit.transform.localPosition.y, -9.461f);
    }
}
