using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }
    public int lives = 3;
    public int level = 1;
    public int score { get; private set; }
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Brick[] bricks { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        // NewGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SetScore(0);
        this.lives = 3;
        LoadLevel(1);
    }

    public void IncreaseScoreBy(int by)
    {
        SetScore(this.score + by);
    }

    private void SetScore(int score)
    {
        this.score = score;
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu") return;

        this.ball = FindObjectOfType<Ball>();
        this.paddle = FindObjectOfType<Paddle>();
        this.bricks = FindObjectsOfType<Brick>();

        // if (this.ball != null)
        // {
        //     this.ball.StartMoving();
        // }
    }

    public void Hit(Brick brick)
    {
        SetScore(this.score + Brick.POINT);
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        return !Array.Exists(bricks, (brick) => brick.gameObject.activeInHierarchy && !brick.unbreakable);
    }

    private void ResetLevel()
    {
        this.ball.ResetBall(resetPosition: true);
        this.paddle.ResetPaddle();
        // Array.ForEach(bricks, (brick) => brick.ResetBrick());

        this.ball.StartMoving();
    }

    private void GameOver()
    {
        NewGame();
    }

    public void Miss(Ball ball)
    {
        ball.gameObject.SetActive(false);
        bool boolExists = Array.Exists(FindObjectsOfType<Ball>(), (ball) => ball.gameObject.activeInHierarchy);
        if (boolExists)
        {
            Destroy(ball.gameObject);
            return;
        }
        this.ball = ball;
        this.lives--;
        if (this.lives > 0)
        {
            ResetLevel();
        }
        else
        {
            GameOver();
        }
    }


}
