using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVanishScript : MonoBehaviour
{

    public LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Ball"))
        {
            // Destroy the ball object when it collides with an object with the "BallVanish" tag
            Destroy(collision.gameObject);
        }
    }
}
