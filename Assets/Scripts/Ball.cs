using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float velocidadInicial = 5f;
    [SerializeField] private float incrementoPorSegundo = 0.5f;
    [SerializeField] private float incrementoPorImpacto = 1f;
    [SerializeField] private float limiteSuperior = 4f;
    [SerializeField] private float limiteInferior = -4f;

    [SerializeField] private float  cooldownImpacto = -0.1f;

    private float  ultimoImpacto = -10f;

    private Vector2 direccion;
    private float velocidadActual;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetearPelota();
    }

    void ResetearPelota()
    {
        transform.position = Vector3.zero;
        velocidadActual = velocidadInicial;

        direccion.x = (Random.Range(0, 2) == 0) ? -1 : 1;
        direccion.y = (Random.Range(0, 2) == 0) ? -1 : 1;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(direccion.normalized * velocidadActual, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        // Aumenta la velocidad de a poco con el tiempo, sin cambiar la dirección //
        velocidadActual += incrementoPorSegundo * Time.fixedDeltaTime;
        rb.linearVelocity = rb.linearVelocity.normalized * velocidadActual;

        if (transform.position.y >= limiteSuperior || transform.position.y <= limiteInferior)
        {
            Vector2 v = rb.linearVelocity;
            v.y *= -1;
            rb.linearVelocity = v;
        }

        if (transform.position.x >= 9 || transform.position.x <= -9)
        {
            ResetearPelota();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && Time.time - ultimoImpacto > cooldownImpacto)
        {
            ultimoImpacto = Time.time;
            velocidadActual += incrementoPorImpacto;
            rb.linearVelocity = rb.linearVelocity.normalized * velocidadActual;
        }

    }
}