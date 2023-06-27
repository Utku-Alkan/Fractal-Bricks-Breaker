using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawnScript : MonoBehaviour
{
    private float TimeInterval = 0;

    [SerializeField] GameObject Breakable1; // square
    [SerializeField] GameObject Breakable2; // circle
    [SerializeField] GameObject Breakable3; // hexagon
    [SerializeField] GameObject Breakable4; // triangle   
    [SerializeField] GameObject Breakable5; // line

    [SerializeField] GameObject BreakableVerticalLine; // dik cizgi
    [SerializeField] GameObject BreakableHeart; // heart
    [SerializeField] GameObject Ring; // ring


    [SerializeField] GameObject Collectable; // x3 top
    [SerializeField] GameObject Collectable2;
    [SerializeField] GameObject Collectable3;
    [SerializeField] GameObject Coin;




    [SerializeField] int FractalLevel1;
    [SerializeField] float collectableOffsetY;
    [SerializeField] float collectableOffsetX;


    public LogicScript logic;

    private GameObject CenterBreakable;
    private GameObject temp;
    private int levelCounter = 0;
    private int randomizer;
    private List<GameObject> centerBreakables = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

        centerBreakables.Add(Breakable1);
        centerBreakables.Add(Breakable2);
        centerBreakables.Add(Breakable3);
        centerBreakables.Add(Breakable4);
        centerBreakables.Add(Breakable5);
        logic.setCoin(PlayerPrefs.GetInt("CoinAmount"));

        CheckFinished();
    }

    void Update()
    {
        // ones per in seconds
        TimeInterval += Time.deltaTime;
        if (TimeInterval >= 3)
        {
            TimeInterval = 0;

            CheckFinished();

        }
    }

    private void CheckFinished()
    {

        if (GameObject.FindGameObjectsWithTag("Breakable").Length <= 0)
        {
            DestroyCollectables();

            levelCounter++;
            logic.setLevel(levelCounter);


            if (FractalLevel1 < 3)
            {
                FractalLevel1++;

                logic.setDegree(FractalLevel1);
            }else if (levelCounter % 5 == 0)
            {
                FractalLevel1 = -1;
                Debug.Log("Special level");

                logic.setDegreeString();
                logic.setFractalName("COIN RUSH!");

                SpecialLevelSpawn();
                logic.increaseBall(2);


                //destroy all balls after completing the fractal


                GameObject[] ballss = GameObject.FindGameObjectsWithTag("Ball");

                foreach (GameObject balll in ballss)
                {
                    Destroy(balll);
                }

                return;
            }

            randomizer = new System.Random().Next(0, 23);

            Debug.Log(randomizer);

            Instantiate(Coin, new Vector3(0, transform.position.y, 0), transform.rotation); //coin init


            // ALL FRACTAL SPAWNS START

            if (randomizer == 22)
            {
                logic.setFractalName("Circular Infinity");
                CenterBreakable = Instantiate(Ring, new Vector3(0, transform.position.y, 0), Breakable2.transform.rotation);
                CircularInfinity(CenterBreakable, FractalLevel1+2);
            }
            else if (randomizer >= 19 && randomizer <= 21)
            {
                logic.setFractalName("Pythagoras tree with " + centerBreakables[randomizer-18].name);

                CenterBreakable = Instantiate(centerBreakables[randomizer - 18], new Vector3(0, transform.position.y, 0), Breakable1.transform.rotation);
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 0.65f;

                PythagorasTree(CenterBreakable, FractalLevel1 + 2);
            }
            else if (randomizer == 18)
            {
                logic.setFractalName("Pythagoras tree (original)");

                CenterBreakable = Instantiate(Breakable1, new Vector3(0, transform.position.y, 0), Breakable1.transform.rotation);
                CenterBreakable.transform.localScale = CenterBreakable.transform.localScale * 0.65f;

                PythagorasTree(CenterBreakable, FractalLevel1 + 2);

            }
            else if (randomizer == 14 || randomizer == 15 || randomizer == 16 || randomizer == 17)
            {
                if(randomizer == 14)
                {
                    logic.setFractalName("T-square (original)");
                    CenterBreakable = Instantiate(Breakable1, new Vector3(0, transform.position.y, 0), Breakable1.transform.rotation);

                }else if(randomizer == 15)
                {
                    logic.setFractalName("T-square with Circle");
                    CenterBreakable = Instantiate(Breakable2, new Vector3(0, transform.position.y, 0), Breakable2.transform.rotation);

                }else if(randomizer == 16)
                {
                    logic.setFractalName("T-square with Hexagon");
                    CenterBreakable = Instantiate(Breakable3, new Vector3(0, transform.position.y, 0), Breakable3.transform.rotation);
                }else if(randomizer == 17)
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

                CenterBreakable = Instantiate(BreakableVerticalLine, new Vector3(0, transform.position.y, 0), BreakableVerticalLine.transform.rotation);
                fractalCanopy(CenterBreakable, FractalLevel1+3, 0);
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
            else if (randomizer > 8 && randomizer <= 12)
            {
                randomizer = randomizer - 9;
                if(randomizer == 1) // deleting circle since it looks disgusting
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

            else if (randomizer > 4 && randomizer <= 7)
            {

                randomizer = randomizer - 5;

                logic.setFractalName("Sierpinski Triangle with " + centerBreakables[randomizer].name);

                temp = Instantiate(centerBreakables[randomizer]);
                temp.transform.localScale = temp.transform.localScale * 1.6f;
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                spawnAroundTriangle(CenterBreakable, FractalLevel1 + 1, false);
                Destroy(temp);
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


            logic.increaseBall(2);


            //ALL FRACTAL SPAWNS END



            //destroy all balls after completing the fractal


            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject ball in balls)
            {
                Destroy(ball);
            }


        }
        else if (GameObject.FindGameObjectsWithTag("Ball").Length <= 0 && logic.returnBallCount() == 0 && !logic.isPanelActive()) // game over
        {

            logic.panelSetActive();
            if(PlayerPrefs.GetInt("Highscore") < levelCounter)
            {
                
                PlayerPrefs.SetInt("Highscore", levelCounter);
                logic.setNewHighscoreText("NEW HIGHSCORE!!!");
            }
            else
            {

                logic.setNewHighscoreText("Game Over");
            }

            logic.setScoreEnd("Your Score: " + levelCounter.ToString());
            
            logic.setHighscore(PlayerPrefs.GetInt("Highscore"));


            //logic.goMainMenuScene();
        }
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


    void fractalCanopy(GameObject rootLine, int maxDepth, int parentInfo) // root = 0, left child = 1, right child = 2
    {
        float rotationAngle = 360.0f / 22.0f;

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

        leftNode.transform.localScale = rootLine.transform.localScale * 0.75f;
        rightNode.transform.localScale = rootLine.transform.localScale * 0.75f;



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





        fractalCanopy(leftNode, maxDepth - 1, 1);
        fractalCanopy(rightNode, maxDepth - 1, 2);
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

        if (maxDepth >= 3)
        {
            Instantiate(Collectable, new Vector3(leftRing.transform.position.x, leftRing.transform.position.y + leftRing.transform.localScale.y, leftRing.transform.position.z), leftRing.transform.rotation);
            Instantiate(Collectable, new Vector3(rightRing.transform.position.x, rightRing.transform.position.y - rightRing.transform.localScale.y, rightRing.transform.position.z), rightRing.transform.rotation);
        }

        CircularInfinity(leftRing, maxDepth - 1);
        CircularInfinity(rightRing, maxDepth - 1);
    }
    void SpecialLevelSpawn()
    {
        //randomizer = new System.Random().Next(0, 3);

        //Debug.Log("Special Level Randomizer: " + randomizer.ToString());

        GameObject newCoin = Instantiate(Coin, new Vector3(0, transform.position.y, 0), transform.rotation); //coin init

        newCoin.transform.localScale = newCoin.transform.localScale * 8;

        spawnAround(newCoin, 2);
        

        Instantiate(BreakableHeart, new Vector3(transform.position.x, transform.position.y + 3, 0), transform.rotation);
        Instantiate(BreakableHeart, new Vector3(transform.position.x + 1, transform.position.y + 3, 0), transform.rotation);
        Instantiate(BreakableHeart, new Vector3(transform.position.x - 1, transform.position.y + 3, 0), transform.rotation);
        Instantiate(BreakableHeart, new Vector3(transform.position.x + 2, transform.position.y + 3, 0), transform.rotation);
        Instantiate(BreakableHeart, new Vector3(transform.position.x - 2, transform.position.y + 3, 0), transform.rotation);




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
