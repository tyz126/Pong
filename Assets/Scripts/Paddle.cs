using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 4.2)
            {
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -4.2)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }*/
        if (Input.GetKey(KeyCode.W) && rb.position.x <= 0)
        {
            rb.velocity = Vector2.up * GameController.instance.paddleSpeed;
        }
        else if (Input.GetKey(KeyCode.S) && rb.position.x <= 0)
        {
             rb.velocity = Vector2.down * GameController.instance.paddleSpeed;
        }
        else if (Input.GetKey(KeyCode.UpArrow) && rb.position.x >= 0)
        {
            rb.velocity = Vector2.up * GameController.instance.paddleSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && rb.position.x >= 0)
        {
            rb.velocity = Vector2.down * GameController.instance.paddleSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }    
    }
}
