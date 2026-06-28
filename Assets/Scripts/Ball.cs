using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Ball : MonoBehaviour
{
    float speed;
    Rigidbody2D rb;
    Vector2 lastVelocity;
    public float speedMultiplier;
    bool isHostWon;
    int winScore;

    // Start is called before the first frame update
    void Start()
    {
        winScore = File.Exists(Application.dataPath + "\\settings.json") ? JsonUtility.FromJson<SettingsManager.Settings>(File.ReadAllText(Application.dataPath + "\\settings.json")).winScore : 100;
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
            GameController.instance.PlaySound(0);
        }

        //Make the ball bounce from wall... (You dk how it works...) now you know...
        if (collision.gameObject.tag == "CollisionBox")
        {
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * lastVelocity.magnitude;
            GameController.instance.PlaySound(1);
        }

        lastVelocity = rb.velocity;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.position.x > 0)
        {
            GameController.instance.score1.text = (int.Parse(GameController.instance.score1.text) + 1).ToString();
            if (int.Parse(GameController.instance.score1.text) == winScore)
            {
                GameController.instance.winScreenText.text = "Player 1 Won!";
                GameController.instance.winScreen.SetActive(true);
                GameController.instance.pauseButton.SetActive(false);
                Time.timeScale = 0;
            }
            isHostWon = true;
        }
        if (rb.position.x < 0)
        {
            GameController.instance.score2.text = (int.Parse(GameController.instance.score2.text) + 1).ToString();
            if (int.Parse(GameController.instance.score2.text) == winScore)
            {
                GameController.instance.winScreenText.text = "Player 2 Won!";
                GameController.instance.winScreen.SetActive(true);
                GameController.instance.pauseButton.SetActive(false);
                Time.timeScale = 0;
            }
            isHostWon = false;
        }
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
        GameController.instance.paddleSpeed = File.Exists(Application.dataPath + "\\settings.json") ? JsonUtility.FromJson<SettingsManager.Settings>(File.ReadAllText(Application.dataPath + "\\settings.json")).initialPaddleSpeed : 12;
        speed = GameController.instance.ballSpeed;
        GameController.instance.PlaySound(2);
    }
}
