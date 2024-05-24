using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


[System.Serializable]

public class SaveData
{
    public static SaveData Instance;
    public HashSet<string> sceneName;

    public void Initialize()
    {
        if (sceneName == null)
        {
            sceneName = new HashSet<string>();
        }
    }
}
