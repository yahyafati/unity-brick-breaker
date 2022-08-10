using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    private GameManager gameManager;
    private int score;
    public void Awake()
    {
        this.textMeshPro = GetComponent<TextMeshProUGUI>();
        this.gameManager = FindObjectOfType<GameManager>();
        setScore(gameManager.score);
    }

    public void Update()
    {
        setScore(gameManager.score);
    }

    public void setScore(int score)
    {
        textMeshPro.text = score.ToString();
    }
}
