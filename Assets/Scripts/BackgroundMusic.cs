using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSource;
    public static BackgroundMusic instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(transform.gameObject);
        }
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
        StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void StartMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
