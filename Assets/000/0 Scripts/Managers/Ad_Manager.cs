using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ad_Manager : Singleton<Ad_Manager>
{
    //Reklam reklamSc;

    private void Start()
    {
        //reklamSc = FindObjectOfType<Reklam>();
    }

    public void Start_Ad()
    {
        Debug.Log("Reklam.");

        //if (Globals.levelIndex + 1 < 2)
        //    reklamSc.showInterstitialAd();
    }
}