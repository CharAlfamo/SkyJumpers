using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LoseSceneUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI relicsText;
    public TextMeshProUGUI fallsText;
    public TextMeshProUGUI rankText;

    void Start()
    {
        Time.timeScale = 1f;

        scoreText.text = "Score: " + GameManager.instance.score;
        relicsText.text = "Relics: " + GameManager.instance.relics;
        fallsText.text = "Falls: " + GameManager.instance.falls;
        rankText.text = "Rank: " + GameManager.instance.GetRank();
    }

    public void Retry()
    {
        GameManager.instance.ResetStats();
        SceneManager.LoadScene("SkyJumpersLevel1Scena");
    }

    public void GoToMenu()
    {
        GameManager.instance.ResetStats();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}