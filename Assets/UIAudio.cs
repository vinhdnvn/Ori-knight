using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // sound on hover and soundOnClick
    public void SoundOnHover()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    public void SoundOnClick()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
