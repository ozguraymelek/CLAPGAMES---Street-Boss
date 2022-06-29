using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class UI_Manager : Singleton<UI_Manager>
{
    [Header("Scriptable Objects References")]
    [Space]
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private GameSettings gameSettings;
    
    [Header("Components")]
    [Space]
    [SerializeField] internal TMP_Text tmp_PlayerMoney;
    [SerializeField] internal GameObject activeIcon;

    [Header("Stand Components")] [Space] 
    
    [SerializeField] internal Text hamburgerStandLevel;
    [SerializeField] internal Text hotdogStandLevel;
    [SerializeField] internal Text icecreamStandLevel;
    [SerializeField] internal Text donutStandLevel;
    [SerializeField] internal Text popcornStandLevel;
    [SerializeField] internal Text chipStandLevel;

    private void Start()
    {
        tmp_PlayerMoney.text = playerSettings.playerMoney.ToString();
        SetStandsLevelToUI();
    }

    #region Player Info
    public void IncreasePlayerMoney()
    {
        playerSettings.playerMoney += 10;
        tmp_PlayerMoney.text = playerSettings.playerMoney.ToString();
    }        
    public void DecreasePlayerMoney()
    {
        playerSettings.playerMoney -= 10;
        tmp_PlayerMoney.text = playerSettings.playerMoney.ToString();

        if (playerSettings.playerMoney <= 0)
        {
            playerSettings.playerMoney = 0;
            tmp_PlayerMoney.text = playerSettings.playerMoney.ToString();
        }
    }
    #endregion

    #region Customer Info
    public void ActivateHappyIcon(Customer customer)
    {
        activeIcon = customer.happyIcon;
        activeIcon.SetActive(true);

        ProcessUIScale(customer);
    }
    public void ActivateAngryIcon(Customer customer)
    {
        activeIcon = customer.angryIcon;
        activeIcon.SetActive(true); 
        ProcessUIScale(customer);
    }
    void ProcessUIScale(Customer customer)
    {
        customer.iconsHolder[0].transform.DOScale(1, .5f).OnComplete(() =>
        {
            customer.iconsHolder[1].transform.DOScale(1, .6f).OnComplete(() =>
            {
                customer.iconsHolder[2].transform.DOScale(1, .7f).OnComplete(() =>
                {
                    activeIcon.transform.DOScale(1, .8f);
                }
                );
            }
            );
        }
        );
    }
    #endregion

    #region Stands Level Info to UI

    public void SetStandsLevelToUI()
    {
        hamburgerStandLevel.text = gameSettings.hamburgerBuildingIndex.ToString();
        hotdogStandLevel.text = gameSettings.hotdogbuildingIndex.ToString();
        icecreamStandLevel.text = gameSettings.iceCreambuildingIndex.ToString();
        donutStandLevel.text = gameSettings.donutbuildingIndex.ToString();
        popcornStandLevel.text = gameSettings.popcornbuildingIndex.ToString();
        chipStandLevel.text = gameSettings.chipsbuildingIndex.ToString();

        SetTextColor();
    }
    
    private void SetTextColor()
    {
        if (gameSettings.hamburgerBuildingIndex != 0)
            hamburgerStandLevel.color = Color.green;
        else
            hamburgerStandLevel.color = Color.red;
        
        if (gameSettings.hotdogbuildingIndex != 0)
            hotdogStandLevel.color = Color.green;
        else
            hotdogStandLevel.color = Color.red;
        
        if (gameSettings.iceCreambuildingIndex != 0)
            icecreamStandLevel.color = Color.green;
        else
            icecreamStandLevel.color = Color.red;
        
        if (gameSettings.donutbuildingIndex != 0)
            donutStandLevel.color = Color.green;
        else
            donutStandLevel.color = Color.red;
        
        if (gameSettings.popcornbuildingIndex != 0)
            popcornStandLevel.color = Color.green;
        else
            popcornStandLevel.color = Color.red;
        
        if (gameSettings.chipsbuildingIndex != 0)
            chipStandLevel.color = Color.green;
        else
            chipStandLevel.color = Color.red;
    }
    #endregion
}
