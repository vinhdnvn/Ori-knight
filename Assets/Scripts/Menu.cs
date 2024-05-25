using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void clickStartButton()
    {
        Debug.Log("New Game start !");
        PlayerPrefs.SetString("Milestone", "Spawn");
        PlayerPrefs.SetInt("UnlockRedSlash", 0);
        PlayerPrefs.SetInt("UnlockPowerForBoss", 0);

        Debug.Log(PlayerPrefs.GetString("Milestone"));
        clickLoadButton();
    }

    public void clickLoadButton()
    {

        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
        Debug.Log(PlayerPrefs.GetString("Milestone"));
        Debug.Log(PlayerPrefs.GetInt("UnlockRedSlash"));
        Debug.Log(PlayerPrefs.GetInt("UnlockPowerForBoss"));
    }

    public void clickQuitButton()
    {
        Application.Quit();
    }

    public void saveInventory()
    {


    }
}
