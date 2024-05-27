using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // audio source
    private AudioSource audioSource;
    [SerializeField] private AudioClip nextLevelSound;



    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        //    _audioSource = GetComponent<AudioSource>();

        Debug.Log("Next Level");
        // play audio
        audioSource.PlayOneShot(nextLevelSound);

        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;

        PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        SceneManager.LoadScene(GlobalController.Instance.nextScene);
    }
}
