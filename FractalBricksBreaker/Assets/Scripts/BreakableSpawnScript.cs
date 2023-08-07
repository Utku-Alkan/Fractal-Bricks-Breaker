using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawnScript : MonoBehaviour
{
    private float TimeInterval = 0;

    [SerializeField] GameObject BreakableSquare; // square
    [SerializeField] GameObject BreakableCircle; // circle
    [SerializeField] GameObject BreakableHexagon; // hexagon
    [SerializeField] GameObject BreakableTriangle; // triangle   
    [SerializeField] GameObject BreakableHorizontalLine; // line

    [SerializeField] GameObject BreakableVerticalLine; // dik cizgi
    [SerializeField] GameObject BreakableHeart; // heart
    [SerializeField] GameObject Ring; // ring
    [SerializeField] GameObject SquareEmpty; // square empty


    [SerializeField] GameObject Collectable; // x3 top
    [SerializeField] GameObject Collectable2;
    [SerializeField] GameObject Collectable3;
    [SerializeField] GameObject Coin;




    [SerializeField] int FractalLevel1;
    [SerializeField] float collectableOffsetY;
    [SerializeField] float collectableOffsetX;


    private LogicScript logic;

    private GameTweenScript tweenScript;


    private GameObject CenterBreakable;
    private GameObject temp;
    private int levelCounter = 0;
    private int randomizer;
    private List<GameObject> centerBreakables = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        tweenScript = GameObject.FindGameObjectWithTag("GameTween").GetComponent<GameTweenScript>();

        centerBreakables.Add(BreakableSquare);
        centerBreakables.Add(BreakableCircle);
        centerBreakables.Add(BreakableHexagon);
        centerBreakables.Add(BreakableTriangle);
        centerBreakables.Add(BreakableHorizontalLine);
        logic.setCoin(PlayerPrefs.GetInt("CoinAmount"));

        StartCoroutine(BreakablesFinished(0f));

    }

    void Update()
    {
        // ones per in seconds
        TimeInterval += Time.deltaTime;
        if (TimeInterval >= 3f)
        {
            TimeInterval = 0;

            CheckFinished();

        }
    }

    private void CheckFinished()
    {

        if (GameObject.FindGameObjectsWithTag("Breakable").Length <= 0)
        {
            ParticleSystem ps = GameObject.Find("Particle").GetComponent<ParticleSystem>();
            ps.Play();
            StartCoroutine(BreakablesFinished(2f));
            tweenScript.BlackScreenVisible();
            tweenScript.StarsAnimation();
            tweenScript.CongratsTextPopUp();
            tweenScript.BlackScreenInvisible();
            logic.PlayYouWinAudio();
        }
        else if (GameObject.FindGameObjectsWithTag("Ball").Length <= 0 && logic.returnBallCount() == 0 && !logic.isPanelActive()) // game over
        {
            StartCoroutine(GameOver());


        }
    }

    IEnumerator BreakablesFinished(float waitSecs)
    {
        yield return new WaitForSeconds(waitSecs);


        DestroyCollectables();

        levelCounter++;
        logic.setLevel(levelCounter);


        if (FractalLevel1 < 3)
        {
            FractalLevel1++;


            randomizer = new System.Random().Next(0, 26);
            Debug.Log(randomizer);

            Instantiate(Coin, new Vector3(0, transform.position.y, 0), transform.rotation); //coin init


            // ALL FRACTAL SPAWNS START
            if(randomizer == 25)
            {
                logic.setFractalName("Nested Square Fractal");

                CenterBreakable = Instantiate(SquareEmpty, new Vector3(transform.position.x, transform.position.y, transform.position.z), SquareEmpty.transform.rotation);
                NestedSquareFractal(CenterBreakable, FractalLevel1);
            }
            else if(randomizer == 24)
            {
                logic.setFractalName("Wide Fractal Tree");

                CenterBreakable = Instantiate(BreakableVerticalLine, new Vector3(0, transform.position.y, 0), BreakableVerticalLine.transform.rotation);
                fractalCanopy(CenterBreakable, FractalLevel1 + 3, 45f, 0.707f);
            }
            else if (randomizer == 23)
            {
                logic.setFractalName("H Tree");

                CenterBreakable = Instantiate(BreakableVerticalLine, new Vector3(0, transform.position.y, 0), BreakableVerticalLine.transform.rotation);
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 1.4f;
                fractalCanopy(CenterBreakable, FractalLevel1 + 3, 90f, 0.707f);
            }
            else if (randomizer == 22)
            {
                logic.setFractalName("Circular Infinity");
                CenterBreakable = Instantiate(Ring, new Vector3(0, transform.position.y, 0), BreakableCircle.transform.rotation);
                CircularInfinity(CenterBreakable, FractalLevel1 + 2);
            }
            else if (randomizer >= 19 && randomizer <= 21)
            {
                logic.setFractalName("Pythagoras tree with " + centerBreakables[randomizer - 18].name);

                CenterBreakable = Instantiate(centerBreakables[randomizer - 18], new Vector3(0, transform.position.y, 0), BreakableSquare.transform.rotation);
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 0.62f;

                PythagorasTree(CenterBreakable, FractalLevel1 + 2);
            }
            else if (randomizer == 18)
            {
                logic.setFractalName("Pythagoras tree (original)");

                CenterBreakable = Instantiate(BreakableSquare, new Vector3(0, transform.position.y, 0), BreakableSquare.transform.rotation);
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 0.61f;

                PythagorasTree(CenterBreakable, FractalLevel1 + 2);

            }
            else if (randomizer == 14 || randomizer == 15 || randomizer == 16 || randomizer == 17)
            {
                if (randomizer == 14)
                {
                    logic.setFractalName("T-square (original)");
                    CenterBreakable = Instantiate(BreakableSquare, new Vector3(0, transform.position.y, 0), BreakableSquare.transform.rotation);

                }
                else if (randomizer == 15)
                {
                    logic.setFractalName("T-square with Circle");
                    CenterBreakable = Instantiate(BreakableCircle, new Vector3(0, transform.position.y, 0), BreakableCircle.transform.rotation);

                }
                else if (randomizer == 16)
                {
                    logic.setFractalName("T-square with Hexagon");
                    CenterBreakable = Instantiate(BreakableHexagon, new Vector3(0, transform.position.y, 0), BreakableHexagon.transform.rotation);
                }
                else if (randomizer == 17)
                {
                    logic.setFractalName("T-square with Heart");
                    CenterBreakable = Instantiate(BreakableHeart, new Vector3(0, transform.position.y, 0), BreakableHeart.transform.rotation);
                    CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 1.5f;
                }
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 1.5f;
                CenterBreakable.GetComponent<SpriteRenderer>().color = Color.yellow;
                TSquare(CenterBreakable, FractalLevel1 + 1);
            }
            else if (randomizer == 13) // fractal canopy
            {
                logic.setFractalName("Fractal Canopy (Tree)");

                CenterBreakable = Instantiate(BreakableVerticalLine, new Vector3(0, transform.position.y-1, 0), BreakableVerticalLine.transform.rotation);
                fractalCanopy(CenterBreakable, FractalLevel1 + 3, 360.0f / 22.0f, 0.75f);
            }

            else if (randomizer > 8 && randomizer <= 12)
            {
                randomizer = randomizer - 9;
                if (randomizer == 1) // deleting circle since it looks disgusting
                {
                    randomizer = 0;
                }
                logic.setFractalName("Cantor Set with " + centerBreakables[randomizer].name);
                temp = Instantiate(centerBreakables[randomizer]);
                temp.transform.localScale = new Vector3(temp.transform.localScale.x * 2, temp.transform.localScale.y, temp.transform.localScale.z);
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                cantorSet(CenterBreakable, FractalLevel1 + 1);
                Destroy(temp);

            }
            else if (randomizer == 8) // line
            {
                logic.setFractalName("Cantor Set (original)");
                randomizer = 4;
                temp = Instantiate(centerBreakables[randomizer]);
                //temp.transform.localScale = temp.transform.localScale * 1.6f;
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                cantorSet(CenterBreakable, FractalLevel1 + 1);
                Destroy(temp);
            }
            else if (randomizer > 4 && randomizer <= 7) // guzel fractallarin oranini artirdik
            {

                if (randomizer == 5)
                {
                    logic.setFractalName("T-square (original)");
                    CenterBreakable = Instantiate(BreakableSquare, new Vector3(0, transform.position.y, 0), BreakableSquare.transform.rotation);
                    CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 1.5f;
                    CenterBreakable.GetComponent<SpriteRenderer>().color = Color.yellow;
                    TSquare(CenterBreakable, FractalLevel1 + 1);
                }
                else if (randomizer == 6)
                {
                    logic.setFractalName("Pythagoras tree (original)");

                    CenterBreakable = Instantiate(BreakableSquare, new Vector3(0, transform.position.y, 0), BreakableSquare.transform.rotation);
                    CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 0.61f;

                    PythagorasTree(CenterBreakable, FractalLevel1 + 2);
                }
                else if (randomizer == 7)
                {
                    logic.setFractalName("Fractal Canopy (Tree)");

                    CenterBreakable = Instantiate(BreakableVerticalLine, new Vector3(0, transform.position.y-1, 0), BreakableVerticalLine.transform.rotation);
                    CenterBreakable.GetComponent<SpriteRenderer>().color = Color.cyan;

                    fractalCanopy(CenterBreakable, FractalLevel1 + 3, 360.0f / 22.0f, 0.75f);
                }
            }
            else if (randomizer == 4) //triangle
            {
                logic.setFractalName("Sierpinski Triangle (original)");
                randomizer = 3;
                temp = Instantiate(centerBreakables[randomizer]);
                temp.transform.localScale = temp.transform.localScale * 1.6f;
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                spawnAroundTriangle(CenterBreakable, FractalLevel1 + 1, true);
                Destroy(temp);

            }
            else if (randomizer >= 0 && randomizer <= 3)// 0 -> square, 1 -> circle, 2 -> hexagon, 3 -> triangle
            {
                if (randomizer == 0)
                {
                    logic.setFractalName("Sierpinski Carpet (original)");

                }
                else
                {
                    logic.setFractalName("Sierpinski Carpet with " + centerBreakables[randomizer].name);

                }
                CenterBreakable = Instantiate(centerBreakables[randomizer], new Vector3(0, transform.position.y, 0), centerBreakables[randomizer].transform.rotation);
                spawnAround(CenterBreakable, FractalLevel1);
            }


            logic.increaseBall(1);


            //ALL FRACTAL SPAWNS END



            //destroy all balls after completing the fractal


            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject ball in balls)
            {
                Destroy(ball);
            }

        }
        else if (levelCounter % 5 == 0)
        {
            FractalLevel1 = -1;
            Debug.Log("Special level");

            logic.setFractalName("COIN RUSH!");

            SpecialLevelSpawn();
            logic.increaseBall(1);


            //destroy all balls after completing the fractal


            GameObject[] ballss = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject balll in ballss)
            {
                Destroy(balll);
            }

        }
    
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.1f);
        logic.panelSetActive();
        logic.panelScaleAnimationToOne();
        if (PlayerPrefs.GetInt("Highscore") < levelCounter)
        {

            PlayerPrefs.SetInt("Highscore", levelCounter);
            logic.setNewHighscoreText("NEW HIGHSCORE!!!");
            logic.PlayNewHighscoreAudio();
        }
        else
        {

            logic.setNewHighscoreText("Game Over");
            logic.PlayYouLoseAudio();

        }

        logic.setScoreEnd("Your Score: " + levelCounter.ToString());

        logic.setHighscore(PlayerPrefs.GetInt("Highscore"));


        //logic.goMainMenuScene();
    }
    void spawnAround(GameObject breakableObject, int maxDepth)
    {
        if (maxDepth <= 0)
        {
            return;
        }

        GameObject smallBreakableObject = Instantiate(breakableObject);
        
        Vector3 smallScale = new Vector3(smallBreakableObject.transform.localScale.x/3, smallBreakableObject.transform.localScale.y / 3, smallBreakableObject.transform.localScale.z);

        smallBreakableObject.transform.localScale = smallScale;

        GameObject around1 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);
        GameObject around2 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);
        GameObject around3 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);
        GameObject around4 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), smallBreakableObject.transform.rotation);
        GameObject around5 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), smallBreakableObject.transform.rotation);
        GameObject around6 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);
        GameObject around7 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);
        GameObject around8 = Instantiate(smallBreakableObject, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), smallBreakableObject.transform.rotation);

        if(maxDepth > 1)
        {

            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);

        }

        Destroy(smallBreakableObject);

        // Call spawnAround for each around object, with maxDepth decreased by 1
        spawnAround(around1, maxDepth - 1);
        spawnAround(around2, maxDepth - 1);
        spawnAround(around3, maxDepth - 1);
        spawnAround(around4, maxDepth - 1);
        spawnAround(around5, maxDepth - 1);
        spawnAround(around6, maxDepth - 1);
        spawnAround(around7, maxDepth - 1);
        spawnAround(around8, maxDepth - 1);
    }




    void spawnAroundTriangle(GameObject breakableObject, int maxDepth, bool isTriangle)
    {
        if (maxDepth <= 0)
        {
            return;
        }

        GameObject smallBreakableObject = Instantiate(breakableObject);

        Vector3 smallScale = new Vector3(smallBreakableObject.transform.localScale.x / 2, smallBreakableObject.transform.localScale.y / 2, smallBreakableObject.transform.localScale.z);

        smallBreakableObject.transform.localScale = smallScale;

        //GameObject around1 = new GameObject();
        //GameObject around2 = new GameObject();
        //GameObject around3 = new GameObject();

        GameObject around1 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x, breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);
        GameObject around2 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);
        GameObject around3 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);




        if (maxDepth > 1)
        {


            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x, breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);



        }

        Destroy(smallBreakableObject);

        // Call spawnAround for each around object, with maxDepth decreased by 1
        spawnAroundTriangle(around1, maxDepth - 1, isTriangle);
        spawnAroundTriangle(around2, maxDepth - 1, isTriangle);
        spawnAroundTriangle(around3, maxDepth - 1, isTriangle);

    }



    void cantorSet(GameObject breakableObject, int maxDepth)
    {
        float distanceY = 0.8f;

        if (maxDepth <= 0)
        {
            return;
        }


        GameObject smallBreakableObject = Instantiate(breakableObject);

        Vector3 smallScale = new Vector3(smallBreakableObject.transform.localScale.x / 3, smallBreakableObject.transform.localScale.y, smallBreakableObject.transform.localScale.z);

        smallBreakableObject.transform.localScale = smallScale;


        

        GameObject around1 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), smallBreakableObject.transform.rotation);
        GameObject around2 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), smallBreakableObject.transform.rotation);



        if (maxDepth > 1)
        {

            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), transform.rotation);

        }


        Destroy(smallBreakableObject);

        cantorSet(around1, maxDepth-1);
        cantorSet(around2, maxDepth-1);

    }


    void fractalCanopy(GameObject rootLine, int maxDepth, float rotationAngle, float lengthRatio) // root = 0, left child = 1, right child = 2
    {
        //float rotationAngle = 360.0f / 22.0f;

        if (maxDepth <= 0)
        {
            return;
        }

        if(maxDepth >= 2)
        {
            Instantiate(Collectable, new Vector3(rootLine.transform.position.x, rootLine.transform.position.y, rootLine.transform.position.z), rootLine.transform.rotation);
        }

        GameObject leftNode = Instantiate(rootLine, new Vector3(rootLine.transform.position.x, rootLine.transform.position.y, rootLine.transform.position.z), rootLine.transform.rotation);
        GameObject rightNode = Instantiate(rootLine, new Vector3(rootLine.transform.position.x, rootLine.transform.position.y, rootLine.transform.position.z), rootLine.transform.rotation);

        leftNode.transform.localScale = rootLine.transform.localScale * lengthRatio;
        rightNode.transform.localScale = rootLine.transform.localScale * lengthRatio;



        float rootRotationAngle = rootLine.transform.rotation.eulerAngles.z;
        Vector3 rootDirection = Quaternion.Euler(0, 0, rootRotationAngle) * Vector3.up;




        leftNode.transform.Rotate(Vector3.forward, rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction = Quaternion.Euler(0, 0, leftNode.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        leftNode.transform.position += rootLine.transform.localScale.y / 2 * rootDirection;
        leftNode.transform.position += leftNode.transform.localScale.y/2 * direction;





        rightNode.transform.Rotate(Vector3.forward, -rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction2 = Quaternion.Euler(0, 0, rightNode.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        rightNode.transform.position += rootLine.transform.localScale.y / 2 * rootDirection;
        rightNode.transform.position += rightNode.transform.localScale.y /2* direction2;





        fractalCanopy(leftNode, maxDepth - 1, rotationAngle, lengthRatio);
        fractalCanopy(rightNode, maxDepth - 1, rotationAngle, lengthRatio);
    }



    void TSquare(GameObject breakableObject, int maxDepth)
    {
        if (maxDepth <= 0)
        {
            return;
        }

        GameObject leftTop = Instantiate(breakableObject, new Vector3(breakableObject.transform.position.x - breakableObject.transform.localScale.x/2, breakableObject.transform.position.y + breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
        leftTop.transform.localScale = leftTop.transform.localScale / 2;
        GameObject rightTop = Instantiate(breakableObject, new Vector3(breakableObject.transform.position.x + breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y + breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
        rightTop.transform.localScale = rightTop.transform.localScale / 2;   
        GameObject leftBottom = Instantiate(breakableObject, new Vector3(breakableObject.transform.position.x - breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y - breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
        leftBottom.transform.localScale = leftBottom.transform.localScale / 2;
        GameObject rightBottom = Instantiate(breakableObject, new Vector3(breakableObject.transform.position.x + breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y - breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
        rightBottom.transform.localScale = rightBottom.transform.localScale / 2;

        if(maxDepth >= 2)
        {
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y + breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y + breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y - breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + breakableObject.transform.localScale.x / 2, breakableObject.transform.position.y - breakableObject.transform.localScale.y / 2, breakableObject.transform.position.z), breakableObject.transform.rotation);
        }

        TSquare(leftTop, maxDepth-1);
        TSquare(rightTop, maxDepth - 1);
        TSquare(leftBottom, maxDepth - 1);
        TSquare(rightBottom, maxDepth - 1);

    }


    void PythagorasTree(GameObject breakableObject, int maxDepth)
    {
        if (maxDepth <= 0)
        {
            return;
        }
        float rotationAngle = 45f;

        float rootRotationAngle = breakableObject.transform.rotation.eulerAngles.z;

        Vector3 rootDirection = Quaternion.Euler(0, 0, rootRotationAngle) * Vector3.up;


        GameObject left = Instantiate(breakableObject);
        left.transform.localScale = left.transform.localScale * 0.707f;
        GameObject right = Instantiate(breakableObject);
        right.transform.localScale = right.transform.localScale * 0.707f;

        left.transform.Rotate(Vector3.forward, rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction = Quaternion.Euler(0, 0, left.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        left.transform.position += breakableObject.transform.localScale.y / 2 * rootDirection;
        left.transform.position += left.transform.localScale.y * direction;






        right.transform.Rotate(Vector3.forward, - rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction2 = Quaternion.Euler(0, 0, right.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        right.transform.position += breakableObject.transform.localScale.y / 2 * rootDirection;
        right.transform.position += right.transform.localScale.y * direction2;


        if(maxDepth >= 2)
        {
            Instantiate(Collectable, new Vector3(left.transform.position.x, left.transform.position.y, left.transform.position.z), left.transform.rotation);
            Instantiate(Collectable, new Vector3(right.transform.position.x, right.transform.position.y, right.transform.position.z), right.transform.rotation);
        }




        PythagorasTree(left, maxDepth - 1);
        PythagorasTree(right, maxDepth - 1);

    }

    void CircularInfinity(GameObject mainRing, int maxDepth)
    {
        if(maxDepth <= 0)
        {
            return;
        }

        GameObject leftRing = Instantiate(mainRing);
        GameObject rightRing = Instantiate(mainRing);

        leftRing.transform.localScale /= 2;
        rightRing.transform.localScale /= 2;

        leftRing.transform.position = new Vector3(leftRing.transform.position.x - mainRing.transform.localScale.x, leftRing.transform.position.y, leftRing.transform.position.z);
        rightRing.transform.position = new Vector3(rightRing.transform.position.x + mainRing.transform.localScale.x, rightRing.transform.position.y, rightRing.transform.position.z);

        if (maxDepth >= 2)
        {
            Instantiate(Collectable, new Vector3(leftRing.transform.position.x, leftRing.transform.position.y + leftRing.transform.localScale.y*2, leftRing.transform.position.z), leftRing.transform.rotation);
            Instantiate(Collectable, new Vector3(rightRing.transform.position.x, rightRing.transform.position.y - rightRing.transform.localScale.y*2, rightRing.transform.position.z), rightRing.transform.rotation);
        }

        CircularInfinity(leftRing, maxDepth - 1);
        CircularInfinity(rightRing, maxDepth - 1);
    }

    void NestedSquareFractal(GameObject center, int maxDepth)
    {
        if(maxDepth <= 0)
        {
            return;
        }
        float offset = ((center.transform.localScale.x) - ((center.transform.localScale.x * 0.4f)) * 5f);
        GameObject leftTop = Instantiate(center);
        GameObject rightTop = Instantiate(center);
        GameObject leftBottom = Instantiate(center);
        GameObject rightBottom = Instantiate(center);

        leftTop.transform.localScale *= 0.4f;
        rightTop.transform.localScale *= 0.4f;
        leftBottom.transform.localScale *= 0.4f;
        rightBottom.transform.localScale *= 0.4f;

        leftTop.transform.position += new Vector3(-offset, offset, 0);
        rightTop.transform.position += new Vector3(offset, offset, 0);
        leftBottom.transform.position += new Vector3(-offset, -offset, 0);
        rightBottom.transform.position += new Vector3(offset, -offset, 0);

        NestedSquareFractal(leftTop, maxDepth - 1);
        NestedSquareFractal(rightTop, maxDepth - 1);
        NestedSquareFractal(leftBottom, maxDepth - 1);
        NestedSquareFractal(rightBottom, maxDepth - 1);

        if (maxDepth == 1)
        {
            Instantiate(Collectable, new Vector3(leftTop.transform.position.x, leftTop.transform.position.y, leftTop.transform.position.z), leftTop.transform.rotation);
            // Instantiate(Collectable, new Vector3(rightTop.transform.position.x, rightTop.transform.position.y, rightTop.transform.position.z), rightTop.transform.rotation);
            // Instantiate(Collectable, new Vector3(leftBottom.transform.position.x, leftBottom.transform.position.y, leftBottom.transform.position.z), leftBottom.transform.rotation);
            Instantiate(Collectable, new Vector3(rightBottom.transform.position.x, rightBottom.transform.position.y, rightBottom.transform.position.z), rightBottom.transform.rotation);
        }
    }
    void SpecialLevelSpawn()
    {
        int randomNum = new System.Random().Next(0, 3);
        if (randomNum == 0)
        {
            GameObject newCoin = Instantiate(Coin, new Vector3(0, transform.position.y, 0), transform.rotation); //coin init

            newCoin.transform.localScale = newCoin.transform.localScale * 8;

            spawnAround(newCoin, 1);
            newCoin.transform.localScale = newCoin.transform.localScale / 3;

            GameObject newCollectable = Instantiate(Collectable, new Vector3(0, transform.position.y, 0), transform.rotation);
            newCollectable.transform.localScale = newCollectable.transform.localScale * 24;

            spawnAround(newCollectable, 1);
            newCollectable.transform.localScale = newCollectable.transform.localScale / 3;



            Instantiate(BreakableHeart, new Vector3(transform.position.x, transform.position.y + 3, 0), transform.rotation);
            Instantiate(BreakableHeart, new Vector3(transform.position.x + 0.9f, transform.position.y + 3, 0), transform.rotation);
            Instantiate(BreakableHeart, new Vector3(transform.position.x - 0.9f, transform.position.y + 3, 0), transform.rotation);
            Instantiate(BreakableHeart, new Vector3(transform.position.x + 1.8f, transform.position.y + 3, 0), transform.rotation);
            Instantiate(BreakableHeart, new Vector3(transform.position.x - 1.8f, transform.position.y + 3, 0), transform.rotation);
        }else if (randomNum == 1)
        {
            Instantiate(BreakableHeart, new Vector3(transform.position.x, transform.position.y + 3, 0), transform.rotation);

            for(int i = 0; i<10; i++)
            {
                Instantiate(Coin, new Vector3(transform.position.x-2+(i*(4.0f/9.0f)), transform.position.y + 2, 0), transform.rotation);
            }

            Instantiate(Collectable2, new Vector3(transform.position.x, transform.position.y - 3, 0), transform.rotation);

            Instantiate(Collectable3, new Vector3(transform.position.x-0.25f, transform.position.y - 2, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(transform.position.x+0.25f, transform.position.y - 2, 0), transform.rotation);

            Instantiate(Collectable2, new Vector3(transform.position.x+0.5f, transform.position.y-1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x, transform.position.y-1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x-0.5f, transform.position.y-1, 0), transform.rotation);

            Instantiate(Collectable3, new Vector3(transform.position.x + 0.75f, transform.position.y, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(transform.position.x + 0.25f, transform.position.y, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(transform.position.x - 0.75f, transform.position.y, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(transform.position.x - 0.25f, transform.position.y, 0), transform.rotation);

            Instantiate(Collectable2, new Vector3(transform.position.x + 1f, transform.position.y + 1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x + 0.5f, transform.position.y + 1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x, transform.position.y + 1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x - 0.5f, transform.position.y + 1, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(transform.position.x - 1f, transform.position.y + 1, 0), transform.rotation);

        }
        else if (randomNum == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Coin, new Vector3(transform.position.x - 2 + i, transform.position.y + 2, 0), transform.rotation);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Coin, new Vector3(transform.position.x - 2 + i, transform.position.y + 1, 0), transform.rotation);
            }
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Coin, new Vector3(transform.position.x - 2 + i, transform.position.y, 0), transform.rotation);
            }

            GameObject turningLine = Instantiate(BreakableVerticalLine, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.Euler(0, 0, 0));
            turningLine.transform.localScale *= 2;
            LeanTween.rotateAround(turningLine, Vector3.forward, -360, 2f).setLoopClamp();
            GameObject turningLine2 = Instantiate(BreakableVerticalLine, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.Euler(0, 0, 90));
            turningLine2.transform.localScale *= 2;
            LeanTween.rotateAround(turningLine2, Vector3.forward, 360, 2f).setLoopClamp();

        }






    }

    void DestroyCollectables()
    {
        GameObject[] collectables = GameObject.FindGameObjectsWithTag("Collectable");

        foreach (GameObject collectable in collectables)
        {
            Destroy(collectable);
        }
    }
}
