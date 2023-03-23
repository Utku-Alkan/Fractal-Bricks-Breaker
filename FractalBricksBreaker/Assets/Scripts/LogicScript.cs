using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] Text ballCount;
    [SerializeField] Text score;
    [SerializeField] Text degree;
    [SerializeField] Text fractalName;
    [SerializeField] Text levelCount;
    [SerializeField] Text highscore;
    [SerializeField] Text scoreEnd;
    [SerializeField] Text newHighscoreText;
    [SerializeField] GameObject panel;
    [SerializeField] Text coinCountText;




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

    public void collectBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }
    }

    public void goMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");

    }


    public void increaseLevel(int a)
    {
        int temp = int.Parse(levelCount.text);
        temp = temp + a;
        levelCount.text = temp.ToString();
    }

    public int getLevel()
    {
        return int.Parse(levelCount.text);
    }

    public void setLevel(int a)
    {
        levelCount.text = a.ToString();
    }

    public void setHighscore(int a)
    {
        highscore.text = "Highscore: " + a.ToString();
    }

    public void setScoreEnd(string a)
    {
        scoreEnd.text = a;
    }

    public void setNewHighscoreText(string a)
    {
        newHighscoreText.text = a;
    }

    public void panelSetActive()
    {
        panel.SetActive(true);
    }

    public void panelSetInactive()
    {
        panel.SetActive(false);
    }

    public bool isPanelActive()
    {
        return panel.activeInHierarchy;
    }

    public void coinIncrease(int a)
    {
        int temp = int.Parse(coinCountText.text);
        temp = temp + a;
        coinCountText.text = temp.ToString();
    }

    public void coinDecrease(int a)
    {
        int temp = int.Parse(coinCountText.text);
        temp = temp - a;
        coinCountText.text = temp.ToString();
    }

    public int getCoin()
    {
        return int.Parse(coinCountText.text);
    }

    public void setCoin(int a)
    {
        coinCountText.text = a.ToString();
    }

    public bool isEnough(int limit)
    {
        if(int.Parse(coinCountText.text) >= limit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
