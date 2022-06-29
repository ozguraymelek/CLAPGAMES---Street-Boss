using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Reflection;

[System.Serializable]
public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;
    public GameSettings gameSettings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);

        //SaveGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadGame();
        }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "game_save");
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/game_data");
        }

        Debug.Log("Game Saved");
        var json = JsonUtility.ToJson(gameSettings);
        File.WriteAllText(Application.persistentDataPath + "/game_save/game_data/game_save.txt", json);
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/game_data"))
        {
            SaveGame();
        }
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(Application.persistentDataPath + "/game_save/game_data/game_save.txt"))
        {
            var file = File.ReadAllText(Application.persistentDataPath + "/game_save/game_data/game_save.txt");
            JsonUtility.FromJsonOverwrite((string)file, gameSettings);
        }

        //Debug.Log("Game Loaded");
    }

}