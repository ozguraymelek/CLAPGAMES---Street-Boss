using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rewards : MonoBehaviour
{

    public AudioSource ses;
    public GameObject EarnedMenu;

    public GameObject Day1Tick;
    public GameObject Day2Tick;
    public GameObject Day3Tick;
    public GameObject Day4Tick;

    public GameObject Day2Locked;
    public GameObject Day3Locked;
    public GameObject Day4Locked;

    void Awake()
    {

        if (PlayerPrefs.GetInt("Day1") == 1)
        {
            Day1Tick.SetActive(true);
            Day2Locked.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Day2") == 1)
        {
            Day2Tick.SetActive(true);
            Day3Locked.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Day3") == 1)
        {
            Day3Tick.SetActive(true);
            Day4Locked.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Day4") == 1)
        {
            Day4Tick.SetActive(true);
            Day4Locked.SetActive(false);
        }

    }

    public void Day1()
    {
        ses.Play();
        EarnedMenu.SetActive(true);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 3500);
        PlayerPrefs.SetInt("Day1", 1);
        Day1Tick.SetActive(true);

    }

    public void Day2()
    {
        ses.Play();
        EarnedMenu.SetActive(true);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 7500);
        PlayerPrefs.SetInt("Day2", 1);
        Day2Tick.SetActive(true);
        Day2Locked.SetActive(false);

    }

    public void Day3()
    {
        ses.Play();
        EarnedMenu.SetActive(true);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 15000);
        PlayerPrefs.SetInt("Day3", 1);
        Day3Tick.SetActive(true);
        Day3Locked.SetActive(false);

    }

    public void Day4()
    {
        ses.Play();
        EarnedMenu.SetActive(true);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 30000);
        PlayerPrefs.SetInt("Day4", 1);
        Day4Tick.SetActive(true);
        Day4Locked.SetActive(false);

    }

}
