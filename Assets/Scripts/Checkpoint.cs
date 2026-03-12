using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            activated = true;

            PlayerController player = other.GetComponent<PlayerController>();
            player.SetCheckpoint(transform.position);

            GameManager.instance.ReachCheckpoint();

            Debug.Log("Checkpoint activado");
        }
    }
}