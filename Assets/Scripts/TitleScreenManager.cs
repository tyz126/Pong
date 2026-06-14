using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.Settings settings = File.Exists(Application.dataPath + "\\settings.json") ? JsonUtility.FromJson<SettingsManager.Settings>(File.ReadAllText(Application.dataPath + "\\settings.json")) : null;
        if (settings == null)
        {
            return;
        }
        mixer.SetFloat("Master", Mathf.Clamp(Mathf.Log10(settings.volumeLevel / 100f) * 20, -79.999f, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeScene(int index)
    {
        if (index == 1)
        {
            BackgroundMusic.instance.StopMusic();
        }
        SceneManager.LoadScene(index);
    }
}
