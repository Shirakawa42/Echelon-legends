using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public GameObject Player;
    public Text HealthText;

    // Start is called before the first frame update
    void Start()
    {
        HealthText.text = Player.GetComponent<Player>().hp.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        HealthText.text = Player.GetComponent<Player>().hp.ToString();
    }
}
