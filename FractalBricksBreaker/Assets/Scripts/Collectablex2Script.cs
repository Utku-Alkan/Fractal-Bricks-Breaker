using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectablex2Script : MonoBehaviour
{
    [SerializeField] int ballSpeed;
    private Vector3 direction1;
    private Vector3 direction2;

    // Start is called before the first frame update
    void Start()
    {
        direction1 = new Vector3(1, 1, 0);
        direction2 = new Vector3(-1, 1, 0);

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject newBall1 = Instantiate(collision.gameObject, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        Rigidbody2D rb1 = newBall1.GetComponent<Rigidbody2D>();
        Vector3 force1 = direction1.normalized * ballSpeed;
        rb1.AddForce(force1);

        GameObject newBall2 = Instantiate(collision.gameObject, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
        Rigidbody2D rb2 = newBall2.GetComponent<Rigidbody2D>();
        Vector3 force2 = direction2.normalized * ballSpeed;
        rb2.AddForce(force2);

        Destroy(gameObject);
    }
}
