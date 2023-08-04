using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    [SerializeField] Text ballCount;
    [SerializeField] Text fractalName;
    [SerializeField] Text levelCount;
    [SerializeField] Text highscore;
    [SerializeField] Text scoreEnd;
    [SerializeField] Text newHighscoreText;
    [SerializeField] GameObject panel;
    [SerializeField] Text coinCountText;
    [SerializeField] GameObject gameOverCloud;
    [SerializeField] AudioSource GameMusic;
    [SerializeField] AudioSource BallHitAudio;
    [SerializeField] AudioSource YouWinAudio;
    [SerializeField] AudioSource YouLoseAudio;
    [SerializeField] AudioSource NewHighscoreAudio;
    [SerializeField] AudioSource UIButtonClickAudio;
    private bool isBallSpawnMoveAllowed = true;

    [SerializeField] Button MusicOnOffButton;
    [SerializeField] Sprite MusicOff;
    [SerializeField] Sprite MusicOn;

    [SerializeField] Button SfxOnOffButton;
    [SerializeField] Sprite SfxOff;
    [SerializeField] Sprite SfxOn;

    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject PauseCloud;

    [SerializeField] GameObject PauseButton;

    void Start()
    {
        if(PlayerPrefs.HasKey("MusicOn") && PlayerPrefs.GetInt("MusicOn") == 0)
        {
            TurnOnOffMusic();
        }
        if(PlayerPrefs.HasKey("SfxOn") && PlayerPrefs.GetInt("SfxOn") == 0)
        {
            TurnOnOffSfx();
        }
    }

    public bool isPausePanelActive()
    {
        return PausePanel.activeInHierarchy;
    }

    public void PausePanelOn()
    {
        PauseCloud.SetActive(true);
        LeanTween.scale(PausePanel, new Vector3(1f, 1f, 1f), 0.2f).setEase(LeanTweenType.easeOutSine).setOnComplete(()=>
                Time.timeScale = 0);
    }

    public void PausePanelOff()
    {
        PauseCloud.SetActive(false);
        PausePanel.transform.localScale = new Vector3(0f, 0f, 0f);
        Time.timeScale = 1;
    }

    public void TurnOnOffMusic()
    {
        if (GameMusic.mute)
        {
            GameMusic.mute = false;
            MusicOnOffButton.GetComponent<Image>().sprite = MusicOn;
            PlayerPrefs.SetInt("MusicOn", 1);
        }
        else
        {
            GameMusic.mute = true;
            MusicOnOffButton.GetComponent<Image>().sprite = MusicOff;
            PlayerPrefs.SetInt("MusicOn", 0);
        }
    }

    public void TurnOnOffSfx()
    {
        if (BallHitAudio.mute)
        {
            BallHitAudio.mute = false;
            YouWinAudio.mute = false;
            YouLoseAudio.mute = false;
            NewHighscoreAudio.mute = false;
            UIButtonClickAudio.mute = false;
            PlayerPrefs.SetInt("SfxOn", 1);

            SfxOnOffButton.GetComponent<Image>().sprite = SfxOn;
        }
        else
        {
            PlayerPrefs.SetInt("SfxOn", 0);

            BallHitAudio.mute = true;
            YouWinAudio.mute = true;
            YouLoseAudio.mute = true;
            NewHighscoreAudio.mute = true;
            UIButtonClickAudio.mute = true;

            SfxOnOffButton.GetComponent<Image>().sprite = SfxOff;
        }
    }

    public void PlayUIButtonClickAudio()
    {
        UIButtonClickAudio.Play();
    }

    public void PlayNewHighscoreAudio()
    {
        NewHighscoreAudio.Play();
    }
    public void PlayYouLoseAudio()
    {
        YouLoseAudio.Play();
    }
    public void PlayYouWinAudio()
    {
        YouWinAudio.Play();
    }
    public void PlayBallHitAudio()
    {
        BallHitAudio.Play();
    }

    public void setBallSpawnMoveAllowed(bool isAllowed)
    {
        isBallSpawnMoveAllowed = isAllowed;
    }

    public bool getIsBallSpawnMoveAllowed()
    {
        return isBallSpawnMoveAllowed;
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
        Debug.Log("Collect Clicked!");
    }

    public void goMainMenuScene()
    {
        Time.timeScale = 1;
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
        gameOverCloud.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void panelSetInactive()
    {
        panel.SetActive(false);
        gameOverCloud.SetActive(false);
        PauseButton.SetActive(true);
    }

    public bool isPanelActive()
    {
        return panel.activeInHierarchy;
    }

    public void panelScaleAnimationToOne()
    {
        LeanTween.scale(panel, new Vector3(1f, 1f, 1f), 2f).setEase(LeanTweenType.easeInOutSine);
        LeanTween.rotateAroundLocal(panel, Vector3.forward, 720f, 1f).setEase(LeanTweenType.easeOutCubic);

    }

    public void panelScaleToZero()
    {
        panel.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void coinIncrease(int a)
    {
        int temp = int.Parse(coinCountText.text);
        temp = temp + a;
        coinCountText.text = "Coins: " + temp.ToString();
    }

    public void coinDecrease(int a)
    {
        int temp = int.Parse(coinCountText.text);
        temp = temp - a;
        coinCountText.text = "Coins: " + temp.ToString();
    }


    public void setCoin(int a)
    {
        coinCountText.text = "Coins: " + a.ToString();
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

    public void AddMoneyHack()
    {
        PlayerPrefs.SetInt("CoinAmount", PlayerPrefs.GetInt("CoinAmount") + 50);
        setCoin(PlayerPrefs.GetInt("CoinAmount"));
    }
}
