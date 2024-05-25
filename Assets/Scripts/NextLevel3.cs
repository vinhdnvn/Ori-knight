using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel3 : MonoBehaviour
{
    // Start is called before the first frame update private 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject != GlobalController.Instance.player)
            return;
        GlobalController.Instance.nextScene = "Level3";

        PlayerPrefs.SetString("Milestone", GlobalController.Instance.nextScene);
        SceneManager.LoadScene(GlobalController.Instance.nextScene);
    }
}
