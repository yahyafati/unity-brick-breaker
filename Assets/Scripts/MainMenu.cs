using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        FindObjectOfType<GameManager>().NewGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
