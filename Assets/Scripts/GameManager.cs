using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game Stats")]
    public int score = 0;
    public int relics = 0;
    public int falls = 0;

    [Header("Score Settings")]
    public int startScore = 100;
    public int relicScore = 50;
    public int checkpointScore = 25;
    public int fallPenalty = 10;

    [Header("Level Objective")]
    public int relicsNeeded = 5;
    private bool levelCompleted = false;

    [Header("Level Complete UI")]
    public GameObject levelCompletePanel;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        score = startScore;
        Debug.Log("Score inicial: " + score);
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score < 0)
            score = 0;

        Debug.Log("Score: " + score);
    }

    public void AddRelic()
    {
        relics++;
        AddScore(relicScore);

        Debug.Log("Reliquias: " + relics);

        CheckLevelComplete();
    }

    public void ReachCheckpoint()
    {
        AddScore(checkpointScore);
        Debug.Log("Checkpoint alcanzado");
    }

    public void RegisterFall()
    {
        falls++;
        AddScore(-fallPenalty);

        Debug.Log("Caídas: " + falls);
    }

    void CheckLevelComplete()
    {
        if (!levelCompleted && relics >= relicsNeeded)
        {
            levelCompleted = true;

            Debug.Log("Todas las reliquias recogidas");

            ShowLevelComplete();
        }
    }

    public void ShowLevelComplete()
    {
        Debug.Log("LEVEL COMPLETE!");

        string rank = GetRank();

        if (levelCompletePanel != null)
            levelCompletePanel.SetActive(true);

        if (finalScoreText != null)
        {
          finalScoreText.text =
         "¡LEVEL COMPLETE!\n\n" +
         "FINAL SCORE :\n" +
         score +
         "\n\nRANK: " + rank;
            }

        Time.timeScale = 0f;
    }

    public string GetRank()
    {
        if (score >= 1000) return "S";
        if (score >= 700) return "A";
        if (score >= 400) return "B";
        if (score >= 200) return "C";
        return "D";
    }
}