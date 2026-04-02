using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeUi : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    void Start()
    {
        volumeSlider = GetComponent<Slider>();

        volumeSlider.value = audioSource.volume;

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
