using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] Text ballCount;
    [SerializeField] Text score;
    [SerializeField] Text degree;
    [SerializeField] Text fractalName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void increaseBall(int a)
    {
        int currentCount = int.Parse(ballCount.text);
        currentCount = currentCount + a;
        ballCount.text = currentCount.ToString();
    }


    public void decreaseBall()
    {
        int currentCount = int.Parse(ballCount.text);
        currentCount--;
        ballCount.text = currentCount.ToString();
    }

    public int returnBallCount()
    {
        return int.Parse(ballCount.text);
    }

    public void increaseScore(int a)
    {
        int temp = int.Parse(score.text);
        temp = temp+a;
        score.text = temp.ToString();
    }

    public void setDegree(int a)
    {
        degree.text = "Degree: " + a.ToString();
    }

    public void setFractalName(string name)
    {
        fractalName.text = name;
    }
}
