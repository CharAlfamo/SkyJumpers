using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public float speed = 4f;      // velocidad del ghost
    public float rango = 12f;     // distancia para detectar al jugador

    private Transform player;
    private Rigidbody rb;

    void Start()
    {
        // Obtener Rigidbody
        rb = GetComponent<Rigidbody>();

        // Buscar al jugador por tag
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
        {
            player = obj.transform;
        }
        else
        {
            Debug.LogError("❌ No se encontró el Player");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Distancia total (X y Y)
        float distancia = Vector3.Distance(transform.position, player.position);

        if (distancia < rango)
        {
            // Dirección hacia el jugador
            Vector3 direccion = (player.position - transform.position).normalized;

            // Movimiento flotante
            rb.velocity = new Vector3(direccion.x * speed, direccion.y * speed, 0);

            // Girar hacia el jugador
            if (direccion.x != 0)
            {
                transform.localScale = new Vector3(Mathf.Sign(direccion.x), 1, 1);
            }
        }
        else
        {
            // Se queda quieto si está lejos
            rb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = col.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Dirección del empuje
                Vector3 dir = (col.transform.position - transform.position).normalized;

                // 💥 Empuje
                playerRb.velocity = new Vector3(dir.x * 8f, 6f, 0);
            }
        }
    }
}