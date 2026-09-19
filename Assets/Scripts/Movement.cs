using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;

    // moveSpeed ya no es velocidad por segundo, sino la magnitud de la fuerza aplicada cada frame de física.
    // Se mantiene ese nombre (en vez de moveForce) porque MenuManager.cs lo usa para el slider de "velocidad" en Opciones. //
    [SerializeField] public float moveSpeed = 4f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(moveUp))
        {
            rb.AddForce(Vector2.up * moveSpeed);
        }

        if (Input.GetKey(moveDown))
        {
            rb.AddForce(Vector2.down * moveSpeed);
        }
    }
}