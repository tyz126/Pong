using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public TMP_InputField volumeInputField;
    public TMP_InputField initialPaddleSpeed;
    public TMP_InputField initialBallSpeed;
    public TMP_InputField winScore;
    public Slider volumeSlider; 
    public int volume;
    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        Settings settings = File.Exists(Application.dataPath + "\\settings.json") ? JsonUtility.FromJson<Settings>(File.ReadAllText(Application.dataPath + "\\settings.json")) : null;
        if (settings == null)
        {
            return;
        }
        volumeInputField.text = settings.volumeLevel.ToString() + "%";
        volumeSlider.value = settings.volumeLevel;
        initialBallSpeed.text = settings.initialBallSpeed.ToString();
        initialPaddleSpeed.text = settings.initialPaddleSpeed.ToString();
        winScore.text = settings.winScore.ToString();
        volume = settings.volumeLevel;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Format string in input field 
    public void FormatStringVolume()
    {
        if (volumeInputField.text.Replace("%", "") == "")
        {
            volumeInputField.text = "0";
        }
        if (int.Parse(volumeInputField.text.Replace("%", "")) > 100)
        {
            volumeInputField.text = "100";
        }
        volumeInputField.text = volumeInputField.text.Split('%')[0] + '%';
    }

    // Set volume from slider to input field
    public void SetVolumeSlider()
    {
        volumeInputField.text = Mathf.RoundToInt(volumeSlider.value).ToString() + "%";
        volume = Mathf.RoundToInt(volumeSlider.value);
        mixer.SetFloat("Master", Mathf.Clamp(Mathf.Log10(volumeSlider.value / 100) * 20, -79.999f, 0));
    }

    public void SetVolumeInputField()
    {
        volumeSlider.value = float.Parse(volumeInputField.text.Split('%')[0]);
        volume = int.Parse(volumeInputField.text.Split('%')[0]);
    }

    public void WriteFile()
    {
        Settings settings = new Settings();
        settings.initialBallSpeed = float.Parse(initialBallSpeed.text);
        settings.initialPaddleSpeed = float.Parse(initialPaddleSpeed.text);
        settings.winScore = int.Parse(winScore.text);
        settings.volumeLevel = volume;
        File.WriteAllText(Application.dataPath + "\\settings.json", JsonUtility.ToJson(settings));

        SceneManager.LoadScene(0);
    }

    public void CancelButton()
    {
        SceneManager.LoadScene(0);
    }

    public class Settings
    {
        public int volumeLevel;
        public int winScore;
        public float initialPaddleSpeed;
        public float initialBallSpeed;
    }
}
