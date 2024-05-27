using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Audio Source")]
    private AudioSource audioSource;
    public AudioSource SFXSource;
    [Header("Audio Clip")]
    // all audio for player
    // public AudioClip attack;
    public AudioClip jump;
    // public AudioClip die;
    // public AudioClip hit;
    // public AudioClip win;
    // public AudioClip lose;

    // public AudioClip hurt;
    // public AudioClip heal;
    // public AudioClip wallJump;
    // public AudioClip dash;
    // public AudioClip walk;

    // public AudioClip fall;

    [Header("Volume")]
    public float volume = 1.0f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        return;

    }
}
