using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    public LogicScript logic;

    private void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        PlayerPrefs.SetInt("CoinAmount", PlayerPrefs.GetInt("CoinAmount")+1);
        logic.setCoin(PlayerPrefs.GetInt("CoinAmount"));

    }
}
