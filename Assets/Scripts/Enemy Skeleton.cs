using UnityEngine;

// Clase del enemigo
public class EnemySimple : MonoBehaviour
{
    // 🔹 Velocidad de movimiento del enemigo
    public float speed = 5f;

    // 🔹 Distancia en la que detecta al jugador
    public float rango = 10f;

    // 🔹 Referencia al jugador
    private Transform player;

    // 🔹 Referencia al Rigidbody del enemigo (para moverlo con física)
    private Rigidbody rb;

    // --- NUEVAS VARIABLES PARA EL SONIDO ---
    public AudioSource enemyAudio;
    private bool isPlayingSound = false;

    // 🔹 Se ejecuta al iniciar el juego
    void Start()
    {
        // Obtenemos el Rigidbody del enemigo
        rb = GetComponent<Rigidbody>();

        // Buscamos el objeto que tenga el tag "Player"
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        // Si encuentra al jugador, guardamos su Transform
        if (obj != null)
        {
            player = obj.transform;
        }
        else
        {
            // Si no lo encuentra, muestra error en consola
            Debug.LogError("❌ No se encontró el Player");
        }
        // Si no se asignó en el inspector, lo buscamos en el objeto
        if (enemyAudio == null)
            enemyAudio = GetComponent<AudioSource>();
    }

    // 🔹 Se ejecuta constantemente (mejor para física)
    void FixedUpdate()
    {
        // Si no hay jugador, no hace nada
        if (player == null) return;

        // Calcula la distancia en el eje X entre el enemigo y el jugador
        float distancia = player.position.x - transform.position.x;

        // 👇 SOLO se mueve en el eje X (juego 2.5D)
        if (Mathf.Abs(distancia) < rango)
        {
            // Determina la dirección:
            // 1 = derecha, -1 = izquierda
            float dir = Mathf.Sign(distancia);

            // Mueve al enemigo hacia el jugador
            // Mantiene la velocidad en Y (para gravedad)
            rb.velocity = new Vector3(dir * speed, rb.velocity.y, 0);

            // Hace que el enemigo mire hacia donde se mueve
            transform.localScale = new Vector3(dir, 1, 1);
            // --- ACTIVAR SONIDO AL DETECTAR AL JUGADOR ---
            if (!isPlayingSound && enemyAudio != null)
            {
                enemyAudio.Play();
                isPlayingSound = true;
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // --- APAGAR SONIDO AL ALEJARSE ---
            if (isPlayingSound && enemyAudio != null)
            {
                enemyAudio.Stop();
                isPlayingSound = false;
            }
        }
    }

    // 🔹 Se ejecuta cuando el enemigo colisiona con algo
    void OnCollisionEnter(Collision col)
    {
        // Verifica si chocó con el jugador
        if (col.gameObject.CompareTag("Player"))
        {
            // Obtiene el Rigidbody del jugador
            Rigidbody playerRb = col.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Calcula hacia qué lado empujar al jugador
                float dir = Mathf.Sign(col.transform.position.x - transform.position.x);

                // 💥 Aplica empuje fuerte:
                // X = lo lanza hacia el lado
                // Y = lo levanta un poco
                playerRb.velocity = new Vector3(dir * 10f, 5f, 0);
            }
        }
    }
}