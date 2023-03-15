using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableSpawnScript : MonoBehaviour
{

    [SerializeField] GameObject Breakable1;
    [SerializeField] GameObject Breakable2;
    [SerializeField] GameObject Breakable3;
    [SerializeField] GameObject Breakable4;


    [SerializeField] GameObject Collectable;


    [SerializeField] int FractalLevel1;
    [SerializeField] float collectableOffsetY;

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


    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectsWithTag("Breakable").Length <= 0)
        {
            if (FractalLevel1 < 3)
            {
                FractalLevel1++;

                logic.setDegree(FractalLevel1);
            }

            randomizer = new System.Random().Next(0, 5);
            Debug.Log(randomizer);
            if (randomizer == 4) //triangle
            {
                randomizer = 3;
                temp = Instantiate(centerBreakables[randomizer]);
                temp.transform.localScale = temp.transform.localScale * 1.6f;
                CenterBreakable = Instantiate(temp, new Vector3(0, transform.position.y, 0), temp.transform.rotation);

                spawnAroundTriangle(CenterBreakable, FractalLevel1+1);
                Destroy(temp);
            }
            else // 0 -> square, 1 -> circle, 2 -> triangle
            { 
                CenterBreakable = Instantiate(centerBreakables[randomizer], new Vector3(0, transform.position.y, 0), centerBreakables[randomizer].transform.rotation);
                spawnAround(CenterBreakable, FractalLevel1);
            }
            logic.increaseBall(5);


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

            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, collectableOffsetY+ smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y - breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x - breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x, collectableOffsetY+ smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);
            Instantiate(Collectable, new Vector3(smallBreakableObject.transform.position.x + breakableObject.transform.localScale.x, collectableOffsetY+ smallBreakableObject.transform.position.y + breakableObject.transform.localScale.x, 0), transform.rotation);

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




    void spawnAroundTriangle(GameObject breakableObject, int maxDepth)
    {
        if (maxDepth <= 0)
        {
            return;
        }

        GameObject smallBreakableObject = Instantiate(breakableObject);

        Vector3 smallScale = new Vector3(smallBreakableObject.transform.localScale.x / 2, smallBreakableObject.transform.localScale.y / 2, smallBreakableObject.transform.localScale.z);

        smallBreakableObject.transform.localScale = smallScale;

        GameObject around1 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x, breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);
        GameObject around2 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);
        GameObject around3 = Instantiate(smallBreakableObject, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), smallBreakableObject.transform.rotation);
        

        if (maxDepth > 1)
        {
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x, collectableOffsetY+ breakableObject.transform.position.y + (1.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x + smallBreakableObject.transform.localScale.x, collectableOffsetY+ breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);
            Instantiate(Collectable, new Vector3(breakableObject.transform.position.x - smallBreakableObject.transform.localScale.x, collectableOffsetY+ breakableObject.transform.position.y - (0.5f * smallBreakableObject.transform.localScale.y), 0), transform.rotation);



        }

        Destroy(smallBreakableObject);

        // Call spawnAround for each around object, with maxDepth decreased by 1
        spawnAroundTriangle(around1, maxDepth - 1);
        spawnAroundTriangle(around2, maxDepth - 1);
        spawnAroundTriangle(around3, maxDepth - 1);

    }
}