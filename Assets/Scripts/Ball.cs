using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public new Rigidbody2D rigidbody { get; private set; }
    public float initialSpeed = 500f;
    public float speed = 20f;

    public void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBall();
    }

    public void StartMoving(float after = .5f)
    {
        Invoke(nameof(SetRandomTrajectory), after);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.x = 0;
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.initialSpeed);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (this.rigidbody.velocity.normalized.y == 0)
        {
            this.rigidbody.AddForce(new Vector2(0, 2));
        }
        this.rigidbody.velocity = this.rigidbody.velocity.normalized * speed;
    }

    public void ResetBall(bool resetPosition = false)
    {
        if (resetPosition)
        {
            this.transform.position = Vector2.zero;
        }
        this.gameObject.SetActive(true);
        this.rigidbody.velocity = Vector2.zero;

        StartMoving(0f);
    }


}
