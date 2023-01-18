using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundsSlider;
    public void ChangeMusicVolume()
    {
        GetComponent<AudioSource>().volume = musicSlider.value;
    }
    public void ChangeSoundsVolume()
    {
        GetComponent<AudioSource>().volume = soundsSlider.value;
    }
}
