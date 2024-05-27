using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnIntroduction : MonoBehaviour
{
    private string[] scripts = {
        "Hello, welcome to the game!",
        "This is a simple game where you have to collect the coins.",
        "You can move the player using the arrow keys or the WASD keys. You can jump by pressing the space bar or the A button on the joystick.",
        "Have fun!"
    };
    private bool isScriptFinished = false;
    private int currentScriptIndex = 0;

    [SerializeField] TextMeshProUGUI textUI;

    // Start is called before the first frame update
    void Start()
    {
        textUI.text = scripts[currentScriptIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (isScriptFinished)
        {
            return;
        }
        GlobalController.Instance.player.GetComponent<PlayerController>().setInputEnabled(false);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            currentScriptIndex++;
            if (currentScriptIndex < scripts.Length)
            {
                textUI.text = scripts[currentScriptIndex];
            }
            else
            {
                GlobalController.Instance.player.GetComponent<PlayerController>().setInputEnabled(true);
                isScriptFinished = true;
                gameObject.SetActive(false);
            }
        }
    }
}
