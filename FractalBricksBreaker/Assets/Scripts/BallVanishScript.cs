using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVanishScript : MonoBehaviour
{
    [SerializeField] GameObject ballSpawn;
    private LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the ball object when it collides with an object with the "BallVanish" tag
            Destroy(collision.gameObject);
            if(this.name == "BallVanishMain")
            {
                if (logic.getIsBallSpawnMoveAllowed())
                {
                    ChangeBallSpawn(collision.transform.position.x);
                    logic.setBallSpawnMoveAllowed(false);
                }
            }
        }
    }

    private void ChangeBallSpawn(float AxisX)
    {
        if (AxisX < 2 && AxisX > -2)
        {
            LeanTween.moveLocal(ballSpawn, new Vector3(AxisX, ballSpawn.transform.position.y, ballSpawn.transform.position.z), 1f).setEase(LeanTweenType.easeInOutCubic);

        }else if (AxisX >= 1)
        {
            LeanTween.moveLocal(ballSpawn, new Vector3(2, ballSpawn.transform.position.y, ballSpawn.transform.position.z), 1f).setEase(LeanTweenType.easeInOutCubic);
            Debug.Log("Characther too right");
        }
        else if (AxisX <= -1)
        {
            LeanTween.moveLocal(ballSpawn, new Vector3(-2, ballSpawn.transform.position.y, ballSpawn.transform.position.z), 1f).setEase(LeanTweenType.easeInOutCubic);
            Debug.Log("Characther too left");
        }

    }
}
