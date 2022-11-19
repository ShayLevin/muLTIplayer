using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumesaveController : MonoBehaviour
{
    [SerializeField] private Slider voolumeslider = null;
    [SerializeField] private Text voolumeTexxt = null;

    private void Start()
    {
        LoadValues();
    }

    public void volumeSlider(float volume)
    {
        voolumeTexxt.text = volume.ToString("0.0");

    }
    public  void savVolumeButton()
    {
        float volumeValue = voolumeslider.value;
        PlayerPrefs.SetFloat("VolumeValue" ,volumeValue );
        LoadValues();
    }

    public void LoadValues()
    {

        float volumeValue = PlayerPrefs.GetFloat("VolumeValue");
        voolumeslider.value = volumeValue;
        AudioListener.volume = volumeValue;

    }
}
