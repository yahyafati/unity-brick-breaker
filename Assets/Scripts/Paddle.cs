
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 70f;

    public void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }

    public void FixedUpdate()
    {
        if (this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce(this.direction * this.speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gift")
        {
            GameManager.instance.IncreaseScoreBy(500);
            Destroy(collision.gameObject);
            return;
        }
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

        Vector3 paddlePosition = this.transform.position;
        Vector3 contactPosition = collision.GetContact(0).point;

        float offset = paddlePosition.x - contactPosition.x;
        float width = collision.otherCollider.bounds.size.x / 2;
        float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
        float bounceAngle = (offset / width) * this.maxBounceAngle;
        float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);
        // float newAngle = currentAngle + bounceAngle;

        Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        Vector2 preVelocity = ball.rigidbody.velocity;
        ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        Vector2 postVelocity = ball.rigidbody.velocity;

        AudioManager.instance.Play("PaddleBounce");
        // if (postVelocity.magnitude != preVelocity.magnitude)
        // {
        //     Debug.Log("pre: v:" + preVelocity + " mag:" + preVelocity.magnitude);
        //     Debug.Log("post: v:" + postVelocity + " mag:" + postVelocity.magnitude);
        // }
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }

}
