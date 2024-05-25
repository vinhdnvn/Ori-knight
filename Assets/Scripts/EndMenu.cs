using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMenu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI leaderboardText;

    public class Data
    {
        public List<int> leaderboard;
    }



    // Start is called before the first frame update
    void Start()
    {
        // stop timer
        PlayerPrefs.SetInt("active", 0);

        // show timer
        float timeCounter = PlayerPrefs.GetFloat("timeCounter");

        int minutes = Mathf.FloorToInt(timeCounter / 60f);
        int seconds = Mathf.FloorToInt(timeCounter % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        string path = Application.persistentDataPath + "/gameData.json";
        Data gameData = LoadData(path);
        if (gameData == null)
        {
            gameData = new Data
            {
                leaderboard = new List<int>()
            };
        }

        gameData.leaderboard.Add((int)timeCounter);
        gameData.leaderboard.Sort();
        gameData.leaderboard.Reverse();


        string leaderboardString = "";
        for (int i = 0; i < Mathf.Min(gameData.leaderboard.Count, 5); i++)
        {
            int m = Mathf.FloorToInt(gameData.leaderboard[i] / 60f);
            int s = Mathf.FloorToInt(gameData.leaderboard[i] % 60f);
            leaderboardString += (i + 1) + string.Format(". {0}m {1}s\n", m, s);
        }
        if (gameData.leaderboard.Count == 0)
        {
            leaderboardString = "No scores yet!";
        }

        leaderboardText.text = leaderboardString;

        SaveData(path, gameData);
    }

    public Data LoadData(string path)
    {
        // string path = Application.persistentDataPath + "/gameData.json";

        if (System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            Data gameData = JsonUtility.FromJson<Data>(json);

            return gameData;
        }
        else
        {
            Debug.Log("file not found");
            return null;
        }
    }

    public void SaveData(string path, Data gameData)
    {
        // string path = Application.persistentDataPath + "/gameData.json";

        string json = JsonUtility.ToJson(gameData);

        System.IO.File.WriteAllText(path, json);
    }

}
