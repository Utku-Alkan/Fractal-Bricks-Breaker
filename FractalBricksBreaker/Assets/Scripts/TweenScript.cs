using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenScript : MonoBehaviour
{
    [SerializeField] GameObject treeRoot;
    [SerializeField] GameObject gameName;



    void Start()
    {
        fractalCanopy(treeRoot, 8);
    }




    void fractalCanopy(GameObject rootLine, int maxDepth)
    {

        if (maxDepth <= 0)
        {
            BringGameName(gameName);
            return;
        }
        Vector3 rootVect3 = new Vector3(rootLine.transform.localScale.x * 0.75f, rootLine.transform.localScale.y * 0.75f, rootLine.transform.localScale.z * 0.75f);

        float rotationAngle = 360.0f / 22.0f;


        GameObject leftNode = Instantiate(rootLine, new Vector3(rootLine.transform.position.x, rootLine.transform.position.y, rootLine.transform.position.z), rootLine.transform.rotation);
        GameObject rightNode = Instantiate(rootLine, new Vector3(rootLine.transform.position.x, rootLine.transform.position.y, rootLine.transform.position.z), rootLine.transform.rotation);

        leftNode.transform.localScale = rootVect3;
        rightNode.transform.localScale = rootVect3;



        float rootRotationAngle = rootLine.transform.rotation.eulerAngles.z;
        Vector3 rootDirection = Quaternion.Euler(0, 0, rootRotationAngle) * Vector3.up;




        leftNode.transform.Rotate(Vector3.forward, rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction = Quaternion.Euler(0, 0, leftNode.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        leftNode.transform.position += rootLine.transform.localScale.y / 2 * rootDirection;
        leftNode.transform.position += leftNode.transform.localScale.y / 2 * direction;





        rightNode.transform.Rotate(Vector3.forward, -rotationAngle);

        // Calculate the direction vector based on the rotation angle
        Vector3 direction2 = Quaternion.Euler(0, 0, rightNode.transform.rotation.eulerAngles.z) * Vector3.up;

        // Move the object in the calculated direction
        rightNode.transform.position += rootLine.transform.localScale.y / 2 * rootDirection;
        rightNode.transform.position += rightNode.transform.localScale.y / 2 * direction2;


        // animation start
        leftNode.transform.localScale = leftNode.transform.localScale * 0f;
        rightNode.transform.localScale = rightNode.transform.localScale * 0f;

        LeanTween.scale(leftNode, rootVect3, 0.9f).setEase(LeanTweenType.easeOutElastic);

        LeanTween.scale(rightNode, rootVect3, 0.9f).setEase(LeanTweenType.easeOutElastic);
        // animation finish

        StartCoroutine(Waiter(leftNode, rightNode, maxDepth));

        

    }

    IEnumerator Waiter(GameObject left, GameObject right, int maxDepth)
    {
        yield return new WaitForSeconds(1);

        fractalCanopy(left, maxDepth - 1);
        fractalCanopy(right, maxDepth - 1);
    }

    void BringGameName(GameObject text)
    {
        LeanTween.moveLocal(text, new Vector3(0f, 600f, 2f), 4f).setDelay(.1f).setEase(LeanTweenType.easeInOutCubic);
    }

}
