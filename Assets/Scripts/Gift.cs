using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Paddle")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
            return;
        }
    }
}
