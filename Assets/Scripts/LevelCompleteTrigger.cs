using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Ya no usamos ShowLevelComplete
            // La victoria se maneja en GameManager con reliquias
        }
    }
}