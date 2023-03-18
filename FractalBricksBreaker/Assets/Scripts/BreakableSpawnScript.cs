using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawnScript : MonoBehaviour
{

    [SerializeField] GameObject Breakable1; // square
    [SerializeField] GameObject Breakable2; // circle
    [SerializeField] GameObject Breakable3; // hexagon
    [SerializeField] GameObject Breakable4; // triangle   
    [SerializeField] GameObject Breakable5; // line


    [SerializeField] GameObject Collectable;
    [SerializeField] GameObject Collectable2;
    [SerializeField] GameObject Collectable3;




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

    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("Breakable").Length <= 0)
        {
            levelCounter++;
            if (FractalLevel1 < 3 && levelCounter > 1)
            {
                levelCounter = 0;
                FractalLevel1++;

                logic.setDegree(FractalLevel1);
            }

            randomizer = new System.Random().Next(0, 9);

            Debug.Log(randomizer);
            if (randomizer == 8) // line
            {
                logic.setFractalName("Cantor Set (original)");
                randomizer = 4;
                temp = Instantiate(centerBreakables[randomizer]);
                //temp.transform.localScale = temp.transform.localScale * 1.6f;
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                cantorSet(CenterBreakable, FractalLevel1 + 1, true);
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

                cantorSet(CenterBreakable, FractalLevel1 + 1, true);
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
            else // 0 -> square, 1 -> circle, 2 -> hexagon, 3 -> triangle
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
            logic.increaseBall(3);


            //destroy all balls after completing the fractal
            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

            foreach (GameObject ball in balls)
            {
                Destroy(ball);
            }

            levelCounter++;
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

            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);


            Instantiate(Collectable2, new Vector3(collectableOffsetX+smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x,  smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);

            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, -collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, -collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, -collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);


            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY + smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);

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

            Instantiate(Collectable3, new Vector3(-collectableOffsetX + breakableObject.transform.position.x, breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable3, new Vector3(-collectableOffsetX + breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);


            Instantiate(Collectable2, new Vector3(collectableOffsetX + breakableObject.transform.position.x, breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable2, new Vector3(collectableOffsetX + breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
    
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x, -collectableOffsetY + breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, -collectableOffsetY + breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, -collectableOffsetY + breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);



        }

        Destroy(smallBreakableObject);

        // Call spawnAround for each around object, with maxDepth decreased by 1
        spawnAroundTriangle(around1, maxDepth - 1, isTriangle);
        spawnAroundTriangle(around2, maxDepth - 1, isTriangle);
        spawnAroundTriangle(around3, maxDepth - 1, isTriangle);

    }



    void cantorSet(GameObject breakableObject, int maxDepth, bool isLine)
    {
        float distanceY = 0.8f;

        if (maxDepth <= 0)
        {
            return;
        }


        GameObject smallBreakableObject = Instantiate(breakableObject);

        Vector3 smallScale = new Vector3(smallBreakableObject.transform.localScale.x / 3, smallBreakableObject.transform.localScale.y, smallBreakableObject.transform.localScale.z);

        smallBreakableObject.transform.localScale = smallScale;

        GameObject around1 = new GameObject();
        GameObject around2 = new GameObject();
        
        if (isLine)
        {
            around1 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), smallBreakableObject.transform.rotation);
            around2 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), smallBreakableObject.transform.rotation);

        }
        else
        {

        }


        if (maxDepth > 1)
        {

            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y + distanceY, 0), transform.rotation);

        }


        Destroy(smallBreakableObject);

        cantorSet(around1, maxDepth-1, isLine);
        cantorSet(around2, maxDepth-1, isLine);

    }
}
