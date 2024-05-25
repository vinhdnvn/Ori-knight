using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    // [SerializeField] private float timeCounter;
    [SerializeField] private TextMeshProUGUI timerText;

    // void Start()
    // {
    //     timeCounter = PlayerPrefs.GetFloat("timeCounter");
    // }

    // Update is called once per frame
    void Update()
    {
        float timeCounter = PlayerPrefs.GetFloat("timeCounter"); ;
        if (PlayerPrefs.GetInt("active") == 1)
        {
            timeCounter += Time.deltaTime;
            PlayerPrefs.SetFloat("timeCounter", timeCounter);
        }


        int minutes = Mathf.FloorToInt(timeCounter / 60f);
        int seconds = Mathf.FloorToInt(timeCounter % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
