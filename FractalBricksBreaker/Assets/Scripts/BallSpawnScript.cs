using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject ball;
    [SerializeField] int ballSpeed;
    [SerializeField] GameObject line;
    private LineRenderer lineRenderer;

    public LogicScript logic;
    
    private bool isDragging = false;
    private Vector3 initialPosition;
    private Color lineColorStart = Color.white;
    private Color lineColorEnd = Color.clear;
    private float lineWidthStart = 0.01f;
    private float lineWidthEnd = 0.1f;


    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.startColor = lineColorStart;
        lineRenderer.endColor = lineColorEnd;
        lineRenderer.startWidth = lineWidthStart;
        lineRenderer.endWidth = lineWidthEnd;
        
    }



    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            Vector3 direction = (initialPosition - Input.mousePosition ).normalized;

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (9*direction));
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

        if (logic.returnBallCount() > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                initialPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                Vector3 releasePosition = Input.mousePosition;
                Vector3 direction = (initialPosition - releasePosition).normalized;
                if(direction.y < 0.1)
                {
                    isDragging = false;
                    return;

                }
                logic.decreaseBall();
                GameObject newBall = spawnBall();
                Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();

                
                Vector3 force = direction * ballSpeed;
                rb.AddForce(force);
                isDragging = false;
            }
        }

    }

    GameObject spawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(0, transform.position.y, 0), transform.rotation);
        return newBall;
    }


}
