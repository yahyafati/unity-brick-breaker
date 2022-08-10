using UnityEngine;

public class MissZone : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            Destroy(collision.gameObject);
            return;
        }
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;
        FindObjectOfType<GameManager>().Miss(ball);
    }
}
