using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float speedIncreasePerSecond = 0.5f;
    [SerializeField] private float speedIncreasePerImpact = 1f;
    [SerializeField] private float topLimit = 4f;
    [SerializeField] private float bottomLimit = -4f;
    [SerializeField] private float rightLimit = 9f;
    [SerializeField] private float leftLimit = -9f;
    [SerializeField] private float impactCooldown = 0.1f;

    private Vector2 direction;
    private float currentSpeed;
    private float lastImpactTime = -10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    void ResetBall()
    {
        transform.position = Vector3.zero;
        currentSpeed = initialSpeed;

        direction.x = (Random.Range(0, 2) == 0) ? -1 : 1;
        direction.y = (Random.Range(0, 2) == 0) ? -1 : 1;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direction.normalized * currentSpeed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        // Aumenta la velocidad de a poco con el tiempo, sin cambiar la dirección //
        currentSpeed += speedIncreasePerSecond * Time.fixedDeltaTime;
        rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;

        if (transform.position.y >= topLimit || transform.position.y <= bottomLimit)
        {
            Vector2 v = rb.linearVelocity;
            v.y *= -1;
            rb.linearVelocity = v;
        }

        if (transform.position.x >= rightLimit || transform.position.x <= leftLimit)
        {
            ResetBall();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // El cooldown evita contar el mismo pique varias veces //
        if (other.gameObject.CompareTag("Player") && Time.time - lastImpactTime > impactCooldown)
        {
            lastImpactTime = Time.time;
            currentSpeed += speedIncreasePerImpact;
            rb.linearVelocity = rb.linearVelocity.normalized * currentSpeed;
        }
    }
}