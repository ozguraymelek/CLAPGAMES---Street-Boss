using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class Activator : MonoBehaviour, IInteractable
{
    private float _stayedTime;
    [SerializeField] List<GameObject> _levels = new List<GameObject>();
    [SerializeField] int[] prices = { 5, 10, 15, 20 };
    [SerializeField] private int[] pricesToText;
    public int levelIndex = 0;
    //protected int levelBound = 0;
    public int activatorId;
    public int collectedMoney;
    [SerializeField] TextMeshProUGUI remainingMoneyTMP;
    // public bool unlocked = true;
    [SerializeField] private bool upgraded = false;

    [Header("Scriptable Objects Reference")]
    [SerializeField] private PlayerSettings playerSettings;

    [Header("Settings")]
    public int spendedDeck;

    public void GetSettings(int _levelIndex, int _activatorId)
    {
        this.levelIndex = _levelIndex;
        //this.levelBound = _levelBound;
        this.activatorId = _activatorId;

        //if (levelBound == levelIndex)
        //{
        //    GetComponent<BoxCollider>().enabled = false;
        //    transform.GetChild(0).gameObject.SetActive(false);
        //}
    }

    private void UpdateScriptable()
    {
        GameManager.Instance.UpdateActivatorId(this.activatorId, levelIndex + 1);
    }

    private void Start()
    {
        StartCoroutine(WaitForGameManagerSet());
    }

    IEnumerator WaitForGameManagerSet()
    {
        yield return null;

        if (levelIndex != 0)
        {
            _levels[0].SetActive(false);

            var limit = levelIndex;
            levelIndex = 0;

            for (int i = 0; i < limit; i++)
            {
                Upgrade();

                if (Improveable() == false)
                {
                    WhenReachedMaxLevel();
                }
            }
        }
        else
        {
            _levels[0].SetActive(true);
            UpdateRemainingMoney();
        }
    }

    private void UpdateRemainingMoney()
    {
        Color textColor;
        if (levelIndex != prices.Length)
            remainingMoneyTMP.text = (pricesToText[levelIndex] - (spendedDeck*2)).ToString();
        else
        {
            textColor = new Color(0b_1, 0b0, 0b0, 0b1);
            remainingMoneyTMP.text = "MAX";
            remainingMoneyTMP.color = textColor;
        }
    }

    void IInteractable.OnEnter()
    {

    }

    void IInteractable.OnExit()
    {
        Exit();
    }

    private void Exit()
    {
        _stayedTime = 0;
        upgraded = false;
    }

    void IInteractable.OnStay()
    {
        if (Improveable() == false)
            return;

        if (upgraded == true)
            return;

        if (Input.GetMouseButton(0))
            return;

        if (StackManager.Instance.ReturnMoneyCount() == 0)
            return;
        //if (Globals.tutorialFinished == false)
        //    return;

        _stayedTime += Time.deltaTime;

        if (_stayedTime < Globals.timeToActivate)
            return;

        if (playerSettings.takedDeck <= 0) return;

        
        StackManager.Instance.RemoveObjectMoney(transform);

        _stayedTime = 0;

        //UI_Manager.Instance.DecreasePlayerMoney();

        

        spendedDeck++;
        if (spendedDeck >= prices[levelIndex])
        {
            StartCoroutine(DelayedUpgrade());

            UpdateScriptable();
        }

        UpdateRemainingMoney();
    }

    IEnumerator DelayedUpgrade()
    {
        upgraded = true;

        spendedDeck = 0;
        // playerSettings.takedDeck -= StackManager.Instance.spendedDeck;
        yield return new WaitForSeconds(0.8f);

        Upgrade();

        if (Improveable() == false)
        {
            WhenReachedMaxLevel();
        }
    }

    private void Upgrade()
    {
        //ForTutorial0();

        if (levelIndex == 0)
            _levels[0].SetActive(false);
        else
            _levels[levelIndex].SetActive(false);

        levelIndex++;
        //_levels[levelIndex].transform.localScale = Vector3.one;
        _levels[levelIndex].SetActive(true);
        
        SoundManager.Instance.StandUpgradeSound(_levels[levelIndex].transform.position);
        
        UpdateRemainingMoney();
        //EffectManager.Instance.ShieldBlast(transform.position);
        //SoundManager.Instance.UpgradeBuilding(transform.position);
    }

    private bool Improveable()
    {
        if (levelIndex < prices.Length - 1 /*&& levelIndex < levelBound*/)
            return true;

        return false;
    }

    private void WhenReachedMaxLevel()
    {
        //Debug.Log("REACHED MAX LEVEL");
        remainingMoneyTMP.text = "Max";

        if (/*levelBound == levelIndex &&*/ Improveable() == false)
        {
            gameObject.SetActive(false);
        }
    }

    /////////////////////////////
    ///
    //bool tut1 = false;
    //protected void ForTutorial0()
    //{
    //    Debug.Log("tut0 Activator");
    //    if (tut1 == true) return;
    //    tut1 = true;

    //    FindObjectOfType<Tutorial0>().InvokeTutorial0();
    //}
}