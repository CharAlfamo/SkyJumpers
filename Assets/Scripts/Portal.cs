using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.finalScore = GameManager.instance.score;
                GameManager.finalRelics = GameManager.instance.relics;
                GameManager.finalFalls = GameManager.instance.falls;
                GameManager.finalRank = GameManager.instance.GetRank();
            }
            else
            {
                Debug.LogError("GameManager es NULL en portal");
            }

            SceneManager.LoadScene("WinScene");
        }
    }
}