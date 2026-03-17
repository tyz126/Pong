using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float speed;
    PhotonView photonView;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        speed = GameController.instance.paddleSpeed;
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
        if (photonView.isMine)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = Vector2.up * speed;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                rb.velocity = Vector2.down * speed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }
}
