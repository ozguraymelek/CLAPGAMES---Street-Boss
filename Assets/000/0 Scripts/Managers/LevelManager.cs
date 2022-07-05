using System.Collections;
using System.Collections.Generic;
using EZ_Pooling;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [Header("Scriptable Object References")]
    [Space]
    [SerializeField] private GameSettings gameSettings;

    [Header("Components")]
    [Space]
    public List<GameObject> levels;

    public List<GameObject> doors;

    [Header("Settings")]
    [Space]
    public int index;
    public Transform activeLevel;

    private void Start()
    {
        ActivateStartLevel(gameSettings);
    }
    public void ActivateStartLevel(GameSettings gameSettings)
    {
        if (gameSettings.hamburgerBuildingIndex == 0 && gameSettings.hotdogbuildingIndex == 0 &&
         gameSettings.iceCreambuildingIndex == 0 && gameSettings.donutbuildingIndex == 0 &&
         gameSettings.popcornbuildingIndex == 0 && gameSettings.chipsbuildingIndex == 0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 0);
        }
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 0 &&
            gameSettings.iceCreambuildingIndex == 0 && gameSettings.donutbuildingIndex == 0 &&
            gameSettings.popcornbuildingIndex == 0 && gameSettings.chipsbuildingIndex == 0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 0);
        }
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 0 && gameSettings.donutbuildingIndex == 0 &&
            gameSettings.popcornbuildingIndex == 0 && gameSettings.chipsbuildingIndex == 0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 1);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 0 &&
            gameSettings.popcornbuildingIndex == 0 && gameSettings.chipsbuildingIndex == 0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 2);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 1 &&
            gameSettings.popcornbuildingIndex == 0 && gameSettings.chipsbuildingIndex ==0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 3);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 1 &&
            gameSettings.popcornbuildingIndex == 1 && gameSettings.chipsbuildingIndex == 0)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 4);
        }
        else if (gameSettings.hamburgerBuildingIndex >= 1 && gameSettings.hotdogbuildingIndex >= 1 &&
            gameSettings.iceCreambuildingIndex >= 1 && gameSettings.donutbuildingIndex >= 1 &&
            gameSettings.popcornbuildingIndex >= 1 && gameSettings.chipsbuildingIndex >= 1)
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 5);
        }
        else
        {
            activeLevel = SpawnManager.Instance.SpawnLevels(Vector3.zero, Quaternion.identity, 0);
        }
    }

    public void RandomLevel()
    {
       
        if (gameSettings.hamburgerBuildingIndex > 1 && gameSettings.hotdogbuildingIndex > 1 &&
            gameSettings.iceCreambuildingIndex > 1 && gameSettings.donutbuildingIndex > 1 &&
            gameSettings.popcornbuildingIndex > 1 && gameSettings.chipsbuildingIndex > 1)
        {
            GameObject door = GameObject.FindGameObjectWithTag("Door");
            door.SetActive(true);
        }else
            return;
    }
}
