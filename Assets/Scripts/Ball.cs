using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour, IPunObservable
{
    float speed;
    Rigidbody2D rb;
    PhotonView photonView;
    Vector3 lastVelocity;
    bool isMasterClientWon;
    public float speedMultiplier;

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
            if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.magnitude == 0)
            {
                rb.velocity = Quaternion.Euler(isMasterClientWon ? 45 : 315, 0, 0) * Vector2.one * speed;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.AddForce(rb.velocity.normalized * speedMultiplier);
        if (collision.gameObject.tag == "Paddle")
        {
            collision.gameObject.transform.InverseTransformPoint(collision.GetContact(collision.contacts.Length - 1).point);
            rb.velocity = Quaternion.AngleAxis(45, Vector3.back) * collision.contacts[0].normal * rb.velocity.magnitude;
        }

        //Make the ball bounce from wall... (You dk how it works...)
        if (collision.gameObject.tag == "CollisionBox")
        {
            var speedWhenAction = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            Debug.Log(collision.contacts[0].normal);
            rb.velocity = direction * speedWhenAction;
        }
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
            rb.position = Vector2.Lerp(rb.position, (Vector2)stream.ReceiveNext(), 0.9f);
            rb.velocity = (Vector2)stream.ReceiveNext();

            /*float lag = Mathf.Abs((float)(PhotonNetwork.time - info.timestamp));
            rb.position += rb.velocity * lag;*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.isMasterClient)
        {
            foreach (var player in PhotonNetwork.playerList)
            {
                if (player.IsMasterClient && rb.position.x > 0)
                {
                    isMasterClientWon = true;
                    player.AddScore(1);
                    Debug.Log(PhotonNetwork.player.GetScore());
                }
                if (!player.IsMasterClient && rb.position.x < 0)
                {
                    isMasterClientWon = false;
                    player.AddScore(1);
                    Debug.Log(PhotonNetwork.player.GetScore());
                }
            }
        }
        rb.velocity = Vector2.zero;
        rb.position = Vector2.zero;
        PhotonNetwork.RPC(photonView, "UpdateText", PhotonTargets.All, false);
    }

    [PunRPC]
    void UpdateText()
    {
        foreach (var player in PhotonNetwork.playerList)
        {
            if (player.IsMasterClient)
            {
                GameController.instance.score1.GetComponent<TextMeshProUGUI>().text = player.GetScore().ToString();
            }
            if (!player.IsMasterClient)
            {
                GameController.instance.score2.GetComponent<TextMeshProUGUI>().text = player.GetScore().ToString();
            }
        }
    }
}
