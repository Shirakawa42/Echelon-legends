using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public int id;
    public int price;
    public int tier;
    public bool onBench;
    public int benchCoord;
    public struct BoardCoord {
        public int x;
        public int y;
    }
    public BoardCoord boardCoord;
}