using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour, IPunObservable
{
    float speed;
    Rigidbody2D rb;
    PhotonView photonView;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    Vector3 lastVelocity;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        speed = GameController.instance.ballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
        if (photonView.isMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = Quaternion.Euler(Random.Range(0, 2) == 0 ? 45 : -45, 0, 0) * Vector2.one * speed;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Paddle")
        {
            collision.gameObject.transform.InverseTransformPoint(collision.GetContact(collision.contacts.Length - 1).point);
            Debug.Log(rb.velocity);
            Debug.Log(rb.velocity.magnitude);
        }

        //Make the ball bounce from wall... (You dk how it works...)
        //if (collision.gameObject.tag == "CollisionBox")
        //{
            var speedWhenAction = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speedWhenAction;
        //}
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }
        else
        {
            rb.position = (Vector2)stream.ReceiveNext();
            rb.velocity = (Vector2)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
            rb.position += rb.velocity * lag;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.position.x > 0)
        {
            score1.text = (System.Convert.ToInt32(score1.text) + 1).ToString();
            transform.position = Vector2.zero;
            rb.velocity = Quaternion.Euler(45, 0, 0) * Vector2.one * speed;
        }
        if (collision.gameObject.transform.position.x < 0)
        {
            score2.text = (System.Convert.ToInt32(score2.text) + 1).ToString();
            transform.position = Vector2.zero;
            rb.velocity = Quaternion.Euler(-45, 0, 0) * Vector2.one * speed;
        }
    }*/
}
