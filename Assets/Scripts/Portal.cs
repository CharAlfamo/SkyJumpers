using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.WinGame();
            }
            else
            {
                Debug.LogError("GameManager no encontrado");
            }
        }
    }
}