using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    void Awake()
    {
        instance = this;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void FinishLevel()
    {
        Debug.Log("LEVEL COMPLETE");

        Debug.Log("Score: " + GameManager.instance.score);
        Debug.Log("Relics: " + GameManager.instance.relics);
        Debug.Log("Falls: " + GameManager.instance.falls);
        Debug.Log("Rank: " + GameManager.instance.GetRank());
    }
}