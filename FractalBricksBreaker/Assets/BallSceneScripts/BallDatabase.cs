using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class BallDatabase : ScriptableObject
{
    public BallSpecify[] balls;

    public int ballSpecifyCount
    {
        get
        {
            return balls.Length;
        }
    }

    public BallSpecify GetBall(int index)
    {
        return balls[index];
    }
}
