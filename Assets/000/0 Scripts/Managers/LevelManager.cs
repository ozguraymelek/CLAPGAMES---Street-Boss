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
    public List<GameObject> level1Prefabs;
    public List<GameObject> level2Prefabs;
    public List<GameObject> level3Prefabs;
    public List<GameObject> previousLevelPrefabs;

    [Header("Settings")]
    [Space]
    public int index;
    public GameObject activeLevel;

    private void Start()
    {
        ActivateNextLevel(gameSettings);
    }
    public void ActivateNextLevel(GameSettings gameSettings)
    {
        //activeLevel.SetActive(false);

        //switch (gameSettings.hamburgerBuildingIndex)
        //{
        //    case 0:
        //        int ind0 = Random.Range(0, level1Prefabs.Count);
        //        activeLevel = level1Prefabs[ind0];
        //        break;
        //    case 1:
        //        int ind1 = Random.Range(0, level1Prefabs.Count);
        //        activeLevel = level1Prefabs[ind1];
        //        break;
        //    case 2:
        //        int ind2 = Random.Range(0, level2Prefabs.Count);
        //        activeLevel = level2Prefabs[ind2];
        //        break;
        //    case 3:
        //        int ind3 = Random.Range(0, level3Prefabs.Count);
        //        activeLevel = level3Prefabs[ind3];
        //        break;
        //}
        //activeLevel.SetActive(true);
        int rand = Random.Range(0, level1Prefabs.Count);
        level1Prefabs[rand].SetActive(true);
        level1Prefabs.RemoveAt(rand);
    }
    public void ActivateNextInstanceLevel()
    {

    }
}
