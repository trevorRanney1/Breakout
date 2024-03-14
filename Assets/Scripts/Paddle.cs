using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float paddleSpeed = 10f;
    float edge = 8.5f;

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector2.right * horizontal * Time.deltaTime * paddleSpeed);

        float height = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector2.up * height * Time.deltaTime * paddleSpeed);

        if (transform.position.x < -edge)
        {
            transform.position = new Vector2(-edge, transform.position.y);
        }
        if (transform.position.x > edge)
        {
            transform.position = new Vector2(edge, transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PowerUp"))
        {
            other.GetComponent<ExtraLife>().ApplyPowerUp();
            Destroy(other.gameObject);
        }
    }
}
