using UnityEngine;

public class Relic : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddRelic();

            Destroy(gameObject);
        }
    }
}