using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ShowLevelComplete();
        }
    }
}