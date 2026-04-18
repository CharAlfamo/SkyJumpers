using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int finalScore;
    public static int finalRelics;
    public static int finalFalls;
    public static string finalRank;

    [Header("Game Stats")]
    public int score;
    public int relics;
    public int falls;

    [Header("Score Settings")]
    public int startScore = 100;
    public int relicScore = 50;
    public int checkpointScore = 25;
    public int fallPenalty = 10;

    [Header("Level Objective")]
    public int relicsNeeded = 5;

    private bool levelCompleted = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

        void Start()
    {
        // Solo inicializa si es primera vez
        if (score == 0 && relics == 0 && falls == 0)
        {
            ResetStats();
        }
    }

    // =========================
    // SCORE
    // =========================

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

    // =========================
    // WIN CONDITION
    // =========================

    void CheckLevelComplete()
    {
        if (!levelCompleted && relics >= relicsNeeded)
        {
            levelCompleted = true;

            Debug.Log("VICTORIA");

            SceneManager.LoadScene("WinScene");
        }
    }

    // =========================
    // RANKING
    // =========================

    public string GetRank()
    {
        if (score >= 1000) return "S";
        if (score >= 700) return "A";
        if (score >= 400) return "B";
        if (score >= 200) return "C";
        return "D";
    }

    // =========================
    // RESET (IMPORTANTE)
    // =========================

    public void ResetStats()
    {
        score = startScore;
        relics = 0;
        falls = 0;
        levelCompleted = false;

        Debug.Log("Stats reiniciados");
    }
}