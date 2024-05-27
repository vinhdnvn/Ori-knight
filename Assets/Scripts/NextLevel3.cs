using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel3 : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip nextLevelSound;
    // Start is called before the first frame update private 

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.PlayOneShot(nextLevelSound);
        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;
        GlobalController.Instance.nextScene = "Level3";

        PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        SceneManager.LoadScene(GlobalController.Instance.nextScene);
    }
}
