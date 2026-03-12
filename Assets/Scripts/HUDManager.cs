using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI relicsText;
    public TextMeshProUGUI fallsText;

    void Update()
    {
        if (GameManager.instance == null) return;

        scoreText.text = "Score: " + GameManager.instance.score;
        relicsText.text = "Relics: " + GameManager.instance.relics;
        fallsText.text = "Falls: " + GameManager.instance.falls;
    }
}