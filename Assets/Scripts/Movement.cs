using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private KeyCode moveUp = KeyCode.W;
    [SerializeField] private KeyCode moveDown = KeyCode.S;

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