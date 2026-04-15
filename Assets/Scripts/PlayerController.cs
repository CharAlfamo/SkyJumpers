using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 50f;
    public float longJumpForce = 60f;

    public Animator animator;

    private Rigidbody rb;
    private bool isGrounded = true;

    private Vector3 checkpoint;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // checkpoint inicial
        checkpoint = transform.position;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        // Movimiento izquierda / derecha
        rb.velocity = new Vector3(move * speed, rb.velocity.y, 0);

        // Animación caminar / correr
        animator.SetFloat("Speed", Mathf.Abs(move));

        // Girar personaje
        if (move > 0)
            transform.rotation = Quaternion.Euler(0, 90, 0);
        else if (move < 0)
            transform.rotation = Quaternion.Euler(0, -90, 0);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            float jumpPower = jumpForce;

            if (Input.GetKey(KeyCode.LeftShift))
                jumpPower = longJumpForce;

            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            animator.SetBool("IsJumping", true);
            isGrounded = false;
        }

        // Caída al vacío
        if (transform.position.y < -10)
        {
            RegisterFall();
            Respawn();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        transform.position = checkpoint;
    }

    void RegisterFall()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RegisterFall();
        }
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpoint = position;
    }
}