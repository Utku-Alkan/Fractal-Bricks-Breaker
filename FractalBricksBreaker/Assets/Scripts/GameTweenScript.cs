using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTweenScript : MonoBehaviour
{
    [SerializeField] GameObject star1, star2, star3;

    [SerializeField] RectTransform blackScreen;


    [SerializeField] GameObject congratsText;

    [SerializeField] GameObject pauseGameButton;
    public void StarsAnimation()
    {
        pauseGameButton.SetActive(false);
        LeanTween.scale(star1, new Vector3(1f, 1f, 1f), 2f).setDelay(0.75f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star2, new Vector3(1f, 1f, 1f), 2f).setDelay(0.50f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star3, new Vector3(1f, 1f, 1f), 2f).setDelay(0.25f).setEase(LeanTweenType.easeOutElastic);


        LeanTween.scale(star1, new Vector3(0f, 0f, 0f), 1f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star2, new Vector3(0f, 0f, 0f), 1f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
        LeanTween.scale(star3, new Vector3(0f, 0f, 0f), 1f).setDelay(2f).setEase(LeanTweenType.easeOutElastic);
    }

    public void CongratsTextPopUp()
    {
        LeanTween.moveLocal(congratsText, new Vector3(0, -200f, 2f), .2f).setDelay(1f).setEase(LeanTweenType.easeInOutCubic);

        LeanTween.moveLocal(congratsText, new Vector3(0, -1000f, 2f), .2f).setDelay(2f).setEase(LeanTweenType.easeInOutCubic);

    }

    public void BlackScreenVisible()
    {
        blackScreen.gameObject.SetActive(true);

        LeanTween.alpha(blackScreen, 0, 0);

        LeanTween.alpha(blackScreen, 1, 1.5f);
    }

    public void BlackScreenInvisible()
    {
        pauseGameButton.transform.localScale = new Vector3(0, 0, 0);
        LeanTween.alpha(blackScreen, 0, 0.5f).setDelay(2f).setOnComplete(() => pauseGameButton.SetActive(true));

        
        LeanTween.scale(pauseGameButton, new Vector3(1f, 1f, 1f), 1f).setEase(LeanTweenType.easeInOutCubic).setDelay(3f);
    }
}
