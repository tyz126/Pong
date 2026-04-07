
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject score;
    public GameObject canvas;
    public GameObject paddlePrefab;
    public static GameController instance;
    public GameObject ballPrefab;
    public GameObject scorePrefab;
    public float ballSpeed;
    public float paddleSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        //Debug.Log(lll.transform.InverseTransformPoint(transform.position));
        //ball = Instantiate(ballPrefab);
        /*ball.GetComponent<Ball>().score1 = score1;
        ball.GetComponent<Ball>().score2 = score2;*/
        if (PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Instantiate(paddlePrefab.name, new Vector2(-8f, 0f), Quaternion.identity, 0);
            score = PhotonNetwork.Instantiate(scorePrefab.name, new Vector2(-500, 460), Quaternion.identity, 0);
            score.transform.SetParent(canvas.transform);
            PhotonNetwork.Instantiate(ballPrefab.name, new Vector2(0f, 0f), Quaternion.identity, 0);
        }
        else
        {
            GameObject score = PhotonNetwork.Instantiate(scorePrefab.name, new Vector2(500, 460), Quaternion.identity, 0);
            score.transform.SetParent(canvas.transform);
            PhotonNetwork.Instantiate(paddlePrefab.name, new Vector2(8f, 0f), Quaternion.identity, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
