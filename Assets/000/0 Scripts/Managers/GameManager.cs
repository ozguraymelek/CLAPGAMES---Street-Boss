using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.AI;
//using UnityEditor.AI;
using UnityEngine;
using TMPro;
using TapticPlugin;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] CkyEvents cky;
    [SerializeField] TextMeshProUGUI levelTMP;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject succesPanel;
    [SerializeField] GameObject failPanel;
    [SerializeField] Button restartButton;


    [Header("Settings")]
    public GameSettings gameSettings;

    [Header("Levels")]
    [SerializeField] GameObject[] levels;

    private int levelIndex;

    [SerializeField] float timeScale = 1.0f;

    [Header("Building Activators")]
    [SerializeField] Activator hamburgerBuildingActivator;
    [SerializeField] Activator hotdogBuildingActivator;
    [SerializeField] Activator iceCreamBuildingActivator;
    [SerializeField] Activator chipsBuildingActivator;
    [SerializeField] Activator donutBuildingActivator;
    [SerializeField] Activator popcornBuildingActivator;


    private void Start()
    {
        SetTimeScale();

        PreparingPanels();

        GetSetLevelIndex();

        SaveLoadOperations();

        SetLevel();

        SubscribeEvents();

        TapticVibrate(ImpactFeedback.Heavy);
    }

    #region For Test and Taking SS

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        levelIndex++;
    //        Globals.levelIndex = levelIndex;
    //        PlayerPrefs.SetInt("level", levelIndex);
    //        SceneManager.LoadScene(0);
    //    }

    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        Time.timeScale = 0;
    //    }
    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        Time.timeScale = 1.5f;
    //    }
    //}

    #endregion

    #region Set At Start

    private void SetTimeScale()
    {
        Time.timeScale = timeScale;
    }

    void PreparingPanels()
    {
        startPanel.SetActive(true);
        succesPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    private void TapticVibrate(ImpactFeedback impactFeedback)
    {
        TapticManager.Impact(impactFeedback);
    }

    private void GetSetLevelIndex()
    {
        levelIndex = PlayerPrefs.GetInt("level");
        Globals.levelIndex = levelIndex;
        levelTMP.text = "Level " + (levelIndex + 1).ToString();
    }

    private void SubscribeEvents()
    {
        CkyEvents.OnSuccess += OnSuccess;
        CkyEvents.OnFail += OnFail;
    }

    private void SetLevel()
    {
        JoystickPlayer joystickPlayer = FindObjectOfType<JoystickPlayer>();
        joystickPlayer.speed = gameSettings.speed;
        joystickPlayer.rotationSpeed = gameSettings.rotationSpeed;
        //joystickPlayer.visDist = gameSettings.visDist;
        //joystickPlayer.visAngle = gameSettings.visAngle;

        CreateLevelMap();

        SetActivatorIndex();
    }

    private void SetActivatorIndex()
    {
        hamburgerBuildingActivator.GetSettings(gameSettings.hamburgerBuildingIndex, 0);
        hotdogBuildingActivator.GetSettings(gameSettings.hotdogbuildingIndex, 1);
        iceCreamBuildingActivator.GetSettings(gameSettings.iceCreambuildingIndex, 2);
        chipsBuildingActivator.GetSettings(gameSettings.chipsbuildingIndex, 3);
        donutBuildingActivator.GetSettings(gameSettings.donutbuildingIndex, 4);
        popcornBuildingActivator.GetSettings(gameSettings.popcornbuildingIndex, 5);
    }

    public void UpdateActivatorId(int activatorId, int activatorLevelIndex)
    {
        switch (activatorId)
        {
            case 0:
                gameSettings.hamburgerBuildingIndex = activatorLevelIndex;
                break;
            case 1:
                gameSettings.hotdogbuildingIndex = activatorLevelIndex;
                break;
            case 2:
                gameSettings.iceCreambuildingIndex = activatorLevelIndex;
                break;
            case 3:
                gameSettings.chipsbuildingIndex = activatorLevelIndex;
                break;
            case 4:
                gameSettings.donutbuildingIndex = activatorLevelIndex;
                break;
            case 5:
                gameSettings.popcornbuildingIndex = activatorLevelIndex;
                break;
        }

        SaveGame();
    }

    private void CreateLevelMap()
    {
        //levels[levelIndex % levels.Length].SetActive(true);
        //Instantiate(levels[levelIndex % levels.Length], Vector3.zero, Quaternion.identity);

        ////NavMeshBuilder.ClearAllNavMeshes();
        ////NavMeshBuilder.BuildNavMesh();
    }

    #endregion

    #region Save Load Operations

    private void SaveLoadOperations()
    {
        //if (levelIndex != 0)
        //    GameSaveManager.Instance.LoadGame();
        //if (levelIndex == 0)
        //    SaveGame();

        GameSaveManager.Instance.LoadGame();
    }

    private void SaveGame()
    {
        GameSaveManager.Instance.SaveGame(); // ******************************** SAVE ********************************
    }

    #endregion

    #region Success

    private void OnSuccess()
    {
        IncreaseLevel();
        SaveGame();
    }

    private void IncreaseLevel()
    {
        levelIndex++;
        Globals.levelIndex = levelIndex;
        PlayerPrefs.SetInt("level", levelIndex);
    }

    private IEnumerator SuccessPanelActivate()
    {
        yield return new WaitForSeconds(1.5f);

        succesPanel.SetActive(true);
    }

    #endregion

    #region Fail

    public void OnFail()
    {
        Debug.Log("LEVEL FAILED");
        Handheld.Vibrate();
        StartCoroutine(Delayed_ActiveFailPanel());
    }
    IEnumerator Delayed_ActiveFailPanel()
    {
        yield return new WaitForSeconds(1.5f);
        failPanel.SetActive(true);
    }

    #endregion

    #region Panels&Buttons
    public void OnStartPanelClicked()
    {
        Time.timeScale = timeScale;

        cky.OnStartButtonClicked();
        startPanel.SetActive(false);
    }

    public void OnSuccessPanelClicked()
    {
        ReloadCurrentScene();
    }

    public void OnFailPanelClicked()
    {
        ReloadCurrentScene();
    }

    public void OnRestartButtonClicked()
    {
        ReloadCurrentScene();
    }

    private void ReloadCurrentScene()
    {
        TapticVibrate(ImpactFeedback.Heavy);
        Ad_Manager.Instance.Start_Ad();
        SceneManager.LoadScene(0);
    }

    #endregion
}