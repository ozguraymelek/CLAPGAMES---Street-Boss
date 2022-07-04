using System;
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
    [SerializeField] internal GameObject panelGameInfoCanvas;

    [Header("Food Info Components")] [Space] 
    
    [SerializeField] internal TMP_Text hamburgerCountInfo;
    [SerializeField] internal TMP_Text hotdogCountInfo;
    [SerializeField] internal TMP_Text icecreamCountInfo;
    [SerializeField] internal TMP_Text donutCountInfo;
    [SerializeField] internal TMP_Text popcornCountInfo;
    [SerializeField] internal TMP_Text chipCountInfo;
    
    [Header("Food Info Settings")][Space]
    [SerializeField] internal int hamburgerCount;
    [SerializeField] internal int hotdogCount;
    [SerializeField] internal int icecreamCount;
    [SerializeField] internal int donutCount;
    [SerializeField] internal int popcornCount;
    [SerializeField] internal int chipCount;

    private void Start()
    {
        tmp_PlayerMoney.text = playerSettings.playerMoney.ToString();
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

    #region Food Count Info to UI

    public void IncreaseFoodCountToUI(Food _food)
    {
        if (_food.activeFood == _food.hamburgerTypes[0] ||_food.activeFood == _food.hamburgerTypes[1] || 
                _food.activeFood == _food.hamburgerTypes[2])
            {
                hamburgerCount++;
                hamburgerCountInfo.text = hamburgerCount.ToString();
            }
            
            if (_food.activeFood == _food.hotDogTypes[0] || _food.activeFood == _food.hotDogTypes[1] || 
                _food.activeFood == _food.hotDogTypes[2])
            {
                hotdogCount++;
                hotdogCountInfo.text = hotdogCount.ToString();
            }

            if (_food.activeFood == _food.iceCreamTypes[0] || _food.activeFood == _food.iceCreamTypes[1] ||
                _food.activeFood == _food.iceCreamTypes[2])
            {
                icecreamCount++;
                icecreamCountInfo.text = icecreamCount.ToString();
            }
            if (_food.activeFood == _food.donutTypes[0] || _food.activeFood == _food.donutTypes[1] ||
                _food.activeFood == _food.donutTypes[2])
            {
                donutCount++;
                donutCountInfo.text = donutCount.ToString();
            }
            if (_food.activeFood == _food.popcornTypes[0] || _food.activeFood == _food.popcornTypes[1] ||
                 _food.activeFood == _food.popcornTypes[2])
            {
                popcornCount++;
                popcornCountInfo.text = popcornCount.ToString();
            }
            if (_food.activeFood == _food.chipsTypes[0] || _food.activeFood == _food.chipsTypes[1] ||
                 _food.activeFood == _food.chipsTypes[2])
            {
                chipCount++;
                popcornCountInfo.text = chipCount.ToString();
            }
    }
    
    public void DecreaseFoodCountToUI(Food _food)
    {
        if (_food.activeFood == _food.hamburgerTypes[0] ||_food.activeFood == _food.hamburgerTypes[1] || 
            _food.activeFood == _food.hamburgerTypes[2])
        {
            hamburgerCount--;
            hamburgerCountInfo.text = hamburgerCount.ToString();

            if (hamburgerCount <= 0)
            {
                hamburgerCount = 0;
                hamburgerCountInfo.text = hamburgerCount.ToString();
            }
        }
            
        if (_food.activeFood == _food.hotDogTypes[0] || _food.activeFood == _food.hotDogTypes[1] || 
            _food.activeFood == _food.hotDogTypes[2])
        {
            hotdogCount--;
            hotdogCountInfo.text = hotdogCount.ToString();
            
            if (hotdogCount <= 0)
            {
                hotdogCount = 0;
                hotdogCountInfo.text = hotdogCount.ToString();
            }
        }

        if (_food.activeFood == _food.iceCreamTypes[0] || _food.activeFood == _food.iceCreamTypes[1] ||
            _food.activeFood == _food.iceCreamTypes[2])
        {
            icecreamCount--;
            icecreamCountInfo.text = icecreamCount.ToString();

            if (icecreamCount <= 0)
            {
                icecreamCount = 0;
                icecreamCountInfo.text = icecreamCount.ToString();
            }
        }
        if (_food.activeFood == _food.donutTypes[0] || _food.activeFood == _food.donutTypes[1] ||
            _food.activeFood == _food.donutTypes[2])
        {
            donutCount--;
            donutCountInfo.text = donutCount.ToString();

            if (donutCount <= 0)
            {
                donutCount = 0;
                donutCountInfo.text = donutCount.ToString();
            }
        }
        if (_food.activeFood == _food.popcornTypes[0] || _food.activeFood == _food.popcornTypes[1] ||
            _food.activeFood == _food.popcornTypes[2])
        {
            popcornCount--;
            popcornCountInfo.text = popcornCount.ToString();

            if (popcornCount <= 0)
            {
                popcornCount = 0;
                popcornCountInfo.text = popcornCount.ToString();
            }
        }
        if (_food.activeFood == _food.chipsTypes[0] || _food.activeFood == _food.chipsTypes[1] ||
            _food.activeFood == _food.chipsTypes[2])
        {
            chipCount--;
            popcornCountInfo.text = chipCount.ToString();

            if (chipCount <= 0)
            {
                chipCount = 0;
                chipCountInfo.text = chipCount.ToString();
            }
        }
    }

    #endregion
}
