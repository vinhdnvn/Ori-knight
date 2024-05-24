using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject inventoryUI;

    bool openInventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            Time.timeScale = pauseMenu.activeSelf ? 0 : 1;


        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            openInventory = !openInventory;
            inventoryUI.SetActive(openInventory);

        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            openInventory = !openInventory;
            inventoryUI.SetActive(openInventory);

        }
    }

    public void loadMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }
}
