using JetBrains.Rider.Unity.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float ballSpeed = 150f;
    [SerializeField] Transform playerPaddle;

    bool inPlay = false;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    private void Start()
    {
        rb.AddForce(Vector2.up * ballSpeed);
    }

    private void Update()
    {
        if (!inPlay)
        {
            transform.position = playerPaddle.position;
        }

        if (Input.GetButtonDown("Jump") && !inPlay)
        {
            inPlay = true;
            rb.AddForce(Vector2.up * ballSpeed);
        }
    }

    public void ResetBall()
    {
        transform.position = playerPaddle.position;
        inPlay = false;
        rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("The Void")){
            ResetBall();
            GameManager.instance.UpdateLiveNum();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Brick"))
        {
            other.gameObject.GetComponent<BrickParent>().TakeDamage(1);
        }
    }
}
