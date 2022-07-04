using System.Collections;
using System.Collections.Generic;
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
    public GameObject activeLevel;

    private void Start()
    {
        ActivateStartLevel(gameSettings);
    }
    public void ActivateStartLevel(GameSettings gameSettings)
    {
        if (gameSettings.hamburgerBuildingIndex == 0 || gameSettings.hamburgerBuildingIndex == 1)
        {
            activeLevel = levels[0];
            activeLevel.SetActive(true);   
        }

        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1)
        {
            activeLevel = levels[1];
            activeLevel.SetActive(true);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1)
        {
            activeLevel = levels[2];
            activeLevel.SetActive(true);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 1)
        {
            activeLevel = levels[3];
            activeLevel.SetActive(true);
        }
        
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 1 &&
            gameSettings.popcornbuildingIndex == 1)
        {
            activeLevel = levels[4];
            activeLevel.SetActive(true);
        }
        else if (gameSettings.hamburgerBuildingIndex == 1 && gameSettings.hotdogbuildingIndex == 1 &&
            gameSettings.iceCreambuildingIndex == 1 && gameSettings.donutbuildingIndex == 1 &&
            gameSettings.popcornbuildingIndex == 1 && gameSettings.chipsbuildingIndex == 1)
        {
            activeLevel = levels[5];
            activeLevel.SetActive(true);
        }
        
    }

    public void RandomLevel()
    {
       
        if (gameSettings.hamburgerBuildingIndex > 1 && gameSettings.hotdogbuildingIndex > 1 &&
            gameSettings.iceCreambuildingIndex > 1 && gameSettings.donutbuildingIndex > 1 &&
            gameSettings.popcornbuildingIndex > 1 && gameSettings.chipsbuildingIndex > 1)
        { 
            activeLevel.SetActive(false);
            int rand = UnityEngine.Random.Range(0, levels.Count - 1);
            activeLevel = levels[rand];
            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
    }
}
