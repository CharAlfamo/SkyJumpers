using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinSceneUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI relicsText;
    public TextMeshProUGUI fallsText;
    public TextMeshProUGUI rankText;

    void Start()
    {
        Time.timeScale = 1f;

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no encontrado");
            return;
        }

        scoreText.text = GameManager.instance.score.ToString();
        relicsText.text = "" + GameManager.instance.relics;
        fallsText.text = "" + GameManager.instance.falls;
        rankText.text = GameManager.instance.GetRank();
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
}