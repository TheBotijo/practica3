using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : MonoBehaviour
{

    public AudioMixer audioMixer;
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    [SerializeField] 
    private TextMeshProUGUI musicSliderText; 
    [SerializeField] 
    private TextMeshProUGUI soundEffectsSliderText;

    public void OnMusicSliderValueChange(float value)
    { 

        musicVolume = value;

        musicSliderText.text = ((int)(value)).ToString();
        audioMixer.SetFloat("Music", value);
    }
    public void OnSound EffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        soundEffectsSliderText.text = ((int)(value)).ToString();
        audioMixer.SetFloat("Sound Effects", value);
    }
 }
