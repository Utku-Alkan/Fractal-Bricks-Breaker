using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMinimizeScript : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.localScale = collision.gameObject.transform.localScale / 1.5f;
        Destroy(gameObject);

    }
}
