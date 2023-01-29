using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldDisplay : MonoBehaviour
{
    public GameObject Player;
    public Text GoldText;

    // Start is called before the first frame update
    void Start()
    {
        GoldText.text = Player.GetComponent<Player>().gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = Player.GetComponent<Player>().gold.ToString();
    }
}
