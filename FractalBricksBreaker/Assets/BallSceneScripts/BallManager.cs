using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallManager : MonoBehaviour
{
    public BallDatabase ballDB;

    public Text nameText;
    public Text rarityText;
    public SpriteRenderer ballSprite2;
    public GameObject selectedBall;

    public Button selectButton;
    public Text price;
    public Text money;
    public Button buyButton;


    private int selectedOption = 0;
    // Start is called before the first frame update
    void Start()
    {


        foreach (BallSpecify ball in ballDB.balls)
        {
            ball.LoadState();
        }
        ballDB.balls[0].unlocked = true;


        money.text = "Coin: " + PlayerPrefs.GetInt("CoinAmount".ToString());
        if (!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
            PlayerPrefs.SetInt("selectedOption", selectedOption);
        }
        else
        {
            selectedOption = PlayerPrefs.GetInt("selectedOption");
        }

        UpdateBall(selectedOption);

    }

    public void NextOption()
    {
        selectedOption++;

        if (selectedOption >= ballDB.ballSpecifyCount)
        {
            selectedOption = 0;
        }
        UpdateBall(selectedOption);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;

        if (selectedOption < 0 )
        {
            selectedOption = ballDB.ballSpecifyCount-1;
        }
        UpdateBall(selectedOption);
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    private void UpdateBall(int selectedOption2)
    {
        BallSpecify ball = ballDB.GetBall(selectedOption2);
        ballSprite2.sprite = ball.ballSprite.GetComponent<SpriteRenderer>().sprite;
        ballSprite2.color = ball.ballSprite.GetComponent<SpriteRenderer>().color;
        selectedBall.transform.localScale = ball.ballSprite.transform.localScale * 6;
        nameText.text = ball.ballName;
        rarityText.text = ball.ballRarity;

        if (ball.unlocked)
        {
            price.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);


        }
        else
        {
            price.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            price.text = "Price: " + ball.cost.ToString();
        }
    }


    public void gotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void buyBall()
    {
        BallSpecify ball = ballDB.GetBall(selectedOption);

        if(ball.cost <= PlayerPrefs.GetInt("CoinAmount"))
        {
            ball.unlocked = true;
            price.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);

            PlayerPrefs.SetInt("CoinAmount", PlayerPrefs.GetInt("CoinAmount") - ball.cost);
        }

        money.text = "Coin: " + PlayerPrefs.GetInt("CoinAmount".ToString());

        UpdateBall(selectedOption);
    }
}
