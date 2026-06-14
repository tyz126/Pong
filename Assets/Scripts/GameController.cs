
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameController : MonoBehaviour
{
    public GameObject paddlePrefab;
    public static GameController instance;
    public GameObject ballPrefab;
    public TextMeshProUGUI score1;
    public TextMeshProUGUI score2;
    public float ballSpeed;
    public float paddleSpeed;
    public float angleMultiplier;
    public AudioClip[] audioClips;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        SettingsManager.Settings settings = File.Exists(Application.dataPath + "\\settings.json") ? JsonUtility.FromJson<SettingsManager.Settings>(File.ReadAllText(Application.dataPath + "\\settings.json")) : null;
        if (settings != null)
        {
            ballSpeed = settings.initialBallSpeed;
            paddleSpeed = settings.initialPaddleSpeed;
        }
        //Debug.Log(lll.transform.InverseTransformPoint(transform.position));
        //ball = Instantiate(ballPrefab);
        /*ball.GetComponent<Ball>().score1 = score1;
        ball.GetComponent<Ball>().score2 = score2;*/
        Instantiate(paddlePrefab, new Vector2(-8f, 0f), Quaternion.identity);
        Instantiate(ballPrefab, new Vector2(0f, 0f), Quaternion.identity);
        Instantiate(paddlePrefab, new Vector2(8f, 0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int i)
    {
        audioSource.clip = audioClips[i];
        audioSource.Play();
    }
}
