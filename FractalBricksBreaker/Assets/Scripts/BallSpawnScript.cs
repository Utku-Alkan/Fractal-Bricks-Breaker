using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpawnScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject ball;
    [SerializeField] int ballSpeed;
    [SerializeField] GameObject line;  
    [SerializeField] GameObject LaserBallLaser;
    [SerializeField] Image ballDisplay;

    private LineRenderer lineRenderer;

    public RectTransform buttonMainMenu;
    private LogicScript logic;

    private bool isDragging = false;
    private Vector3 initialPosition;
    private Color lineColorStart = Color.white;
    private Color lineColorEnd = Color.clear;
    private float lineWidthStart = 0.01f;
    private float lineWidthEnd = 0.1f;
    public BallDatabase ballDB;

    [SerializeField] GameObject character;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        ball = ballDB.GetBall(PlayerPrefs.GetInt("selectedOption")).ballSprite;
        ballDisplay.sprite = ball.GetComponent<SpriteRenderer>().sprite;
        ballDisplay.color = ball.GetComponent<SpriteRenderer>().color;


        lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.startColor = lineColorStart;
        lineRenderer.endColor = lineColorEnd;
        lineRenderer.startWidth = lineWidthStart;
        lineRenderer.endWidth = lineWidthEnd;

        Color inverseColor = new Color(1f - ballDisplay.color.r, 1f - ballDisplay.color.g, 1f - ballDisplay.color.b);

        character.GetComponent<SpriteRenderer>().color = inverseColor;
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




        if (Input.GetMouseButtonDown(0) && !logic.isPausePanelActive())
        {
            if (logic.returnBallCount() > 0)
            {
                isDragging = true;
                initialPosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            if (logic.returnBallCount() > 0)
            {
                Vector3 releasePosition = Input.mousePosition;
                Vector3 direction = (initialPosition - releasePosition).normalized;
                if (direction.y < 0.1)
                {
                    isDragging = false;
                    return;

                }
                logic.setBallSpawnMoveAllowed(true); // allowing first ball to change ball spawner's place
                logic.decreaseBall();
                GameObject newBall = spawnBall();
                Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();


                Vector3 force = direction * ballSpeed;
                rb.AddForce(force);
                isDragging = false;


                // handling X2 Ball
                if (newBall.name == "X2Ball(Clone)" || newBall.name == "GravityBall(Clone)")
                {
                    GameObject newBall2 = spawnBall();

                    Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();

                    rb2.AddForce(force * 1.2f);
                }
                else if (newBall.name == "LaserBall(Clone)")
                {
                    GameObject newBall2 = Instantiate(LaserBallLaser, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();

                    rb2.AddForce(force * 2f);

                }
                else if (newBall.name == "X3Ball(Clone)")
                {
                    GameObject newBall2 = spawnBall();

                    Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();

                    rb2.AddForce(force * 1.2f);


                    GameObject newBall3 = spawnBall();

                    Rigidbody2D rb3 = newBall3.GetComponent<Rigidbody2D>();

                    rb3.AddForce(force * 1.4f);
                }
                else if (newBall.name == "Trio(Clone)")
                {
                    GameObject newBall2 = spawnBall();

                    newBall2.transform.localScale = ball.transform.localScale * 2;

                    Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();

                    rb2.AddForce(force);



                    GameObject newBall3 = spawnBall();

                    newBall3.transform.localScale = ball.transform.localScale * 3;

                    Rigidbody2D rb3 = newBall3.GetComponent<Rigidbody2D>();

                    rb3.AddForce(force);
                }
                else if (newBall.name == "HackerBall(Clone)")
                {
                    GameObject newBall2 = Instantiate(ball, new Vector3(0, transform.position.y + 8.5f, 0), transform.rotation);

                    Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();

                    rb2.AddForce(force);
                }
            }
        }
        
    }

    GameObject spawnBall()
    {
        GameObject newBall = Instantiate(ball, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);

        return newBall;
    }


}
