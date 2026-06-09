using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SettingsManager : MonoBehaviour
{
    public TMP_InputField volumeInputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FormatString()
    {
        if (!volumeInputField.text.Contains("%") || volumeInputField.text.Count(c =>  c == '%') > 1)
        {
            volumeInputField.text = volumeInputField.text.Split('%')[0] + '%';
        }
    }
}
