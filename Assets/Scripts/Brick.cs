using UnityEngine;

public class Brick : MonoBehaviour
{
    public static int POINT = 100;
    private const int GIFT_THRESHOLD = 50;
    public SpriteRenderer spriteRenderer { get; private set; }
    public Sprite[] states;
    public int health;
    public bool unbreakable;
    private int giftIndex;
    // public Ball ballPrefab;
    public GameObject[] gifts;
    private bool fadeOut;

    public void Awake()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        int random = Random.Range(0, 100);
        this.giftIndex = -1;
        if (random > GIFT_THRESHOLD)
        {
            int buckets = (100 - GIFT_THRESHOLD) / gifts.Length;
            this.giftIndex = (random - GIFT_THRESHOLD) / buckets;
        }
    }

    public void Start()
    {
        ResetBrick();
    }

    public void Update()
    {
        if (fadeOut)
        {
            
        }
    }

    public void ResetBrick()
    {
        this.gameObject.SetActive(true);
        if (unbreakable) return;

        // this.health = 1; // TODO delete me

        this.spriteRenderer.sprite = this.states[this.health - 1];
    }

    public void FadeToDistraction()
    {
        this.fadeOut = true;
    }

    private void Hit()
    {
        if (unbreakable) return;
        this.health--;
        if (this.health <= 0)
        {
            this.gameObject.SetActive(false);
            if (giftIndex > -1 && giftIndex < gifts.Length)
            {
                GameObject gameObject = Instantiate(gifts[giftIndex], this.transform.position, Quaternion.identity);
                // Ball ball = Instantiate<Ball>(ballPrefab, this.transform.position, Quaternion.identity);
                // // ball.transform.position = new Vector3(7, 7, 0);
                // ball.StartMoving(0f);
            }
        }
        else
        {
            this.spriteRenderer.sprite = this.states[this.health - 1];
        }
        FindObjectOfType<GameManager>().Hit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null) return;
        AudioManager.instance.Play("BrickCollide");
        // FindObjectOfType<AudioManager>().Play("BrickCollide");
        Hit();
    }


}
