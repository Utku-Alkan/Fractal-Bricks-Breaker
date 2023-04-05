using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BallSpecify
{
    public string ballName;
    public string ballRarity;
    public GameObject ballSprite;
    public bool _unlocked;
    public int cost;


    public bool unlocked
    {
        get
        {
            return _unlocked;
        }
        set
        {
            _unlocked = value;
            PlayerPrefs.SetInt(ballName + "_unlocked", value ? 1 : 0);
        }
    }

    public void LoadState()
    {
        unlocked = PlayerPrefs.GetInt(ballName + "_unlocked", 0) == 1;
    }
}
