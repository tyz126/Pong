using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Vector2 lastVelocity;
    public float speedMultiplier;
    bool isHostWon;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = GameController.instance.ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.magnitude == 0)
            {
                rb.velocity = Quaternion.Euler(0, 0, isHostWon ? 45 : -45) * Vector2.up * speed;
                lastVelocity = rb.velocity;
            }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //rb.AddForce(rb.velocity.normalized * speedMultiplier);
        if (collision.gameObject.tag == "Paddle")
        {
            float angle = 180 * collision.gameObject.transform.InverseTransformPoint(collision.GetContact(collision.contacts.Length - 1).point).y * GameController.instance.angleMultiplier;
            collision.gameObject.transform.InverseTransformPoint(collision.GetContact(collision.contacts.Length - 1).point);
            rb.velocity = Quaternion.Euler(0, 0, rb.position.x > 0 ? -angle : angle) * collision.contacts[0].normal * speed;
            GameController.instance.paddleSpeed += 1;
            speed += speedMultiplier;
        }

        //Make the ball bounce from wall... (You dk how it works...)
        if (collision.gameObject.tag == "CollisionBox")
        {
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * lastVelocity.magnitude;
        }

        lastVelocity = rb.velocity;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.position.x > 0)
        {
            GameController.instance.score1.text = (int.Parse(GameController.instance.score1.text) + 1).ToString();
            isHostWon = true;
        }
        if (rb.position.x < 0)
        {
            GameController.instance.score2.text = (int.Parse(GameController.instance.score2.text) + 1).ToString();
            isHostWon = false;
        }
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
        GameController.instance.paddleSpeed = 12;
        speed = GameController.instance.ballSpeed;
    }
}
