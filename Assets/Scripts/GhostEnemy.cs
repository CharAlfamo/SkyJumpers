using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float speed = 4f;
    public float rango = 12f;

    [Header("Sistema de Cansancio")]
    public float tiempoMaximoPersecucion = 4f; // Segundos que aguanta corriendo
    public float tiempoDescanso = 6f;         // Segundos que se queda quieto

    private float cronometroPersecucion;
    private float cronometroDescanso;
    private bool estaCansado = false;

    private Transform player;
    private Rigidbody rb;

    [Header("Sonido")]
    public AudioSource ghostAudio;
    private bool isPlayingSound = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null) player = obj.transform;

        if (ghostAudio == null)
            ghostAudio = GetComponent<AudioSource>();

        cronometroPersecucion = tiempoMaximoPersecucion;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        float distancia = Vector3.Distance(transform.position, player.position);

        // LÓGICA DE ESTADOS
        if (estaCansado)
        {
            Descansar();
        }
        else if (distancia < rango)
        {
            Perseguir();
        }
        else
        {
            Detenerse();
        }
    }

    void Perseguir()
    {
        Vector3 direccion = (player.position - transform.position).normalized;
        rb.velocity = new Vector3(direccion.x * speed, direccion.y * speed, 0);

        if (direccion.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(direccion.x), 1, 1);

        // Control de tiempo de persecución
        cronometroPersecucion -= Time.fixedDeltaTime;
        if (cronometroPersecucion <= 0)
        {
            estaCansado = true;
            cronometroDescanso = tiempoDescanso;
        }

        // Sonido
        if (!isPlayingSound && ghostAudio != null)
        {
            ghostAudio.Play();
            isPlayingSound = true;
        }
    }

    void Descansar()
    {
        rb.velocity = Vector3.zero;
        cronometroDescanso -= Time.fixedDeltaTime;

        if (cronometroDescanso <= 0)
        {
            estaCansado = false;
            cronometroPersecucion = tiempoMaximoPersecucion; // Reset energía
        }

        DetenerSonido();
    }

    void Detenerse()
    {
        rb.velocity = Vector3.zero;
        // Si no está persiguiendo, recupera energía poco a poco
        if (cronometroPersecucion < tiempoMaximoPersecucion)
            cronometroPersecucion += Time.fixedDeltaTime;

        DetenerSonido();
    }

    void DetenerSonido()
    {
        if (isPlayingSound && ghostAudio != null)
        {
            ghostAudio.Stop();
            isPlayingSound = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = col.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 dir = (col.transform.position - transform.position).normalized;
                playerRb.velocity = new Vector3(dir.x * 8f, 6f, 0);
            }
        }
    }
}