using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Reklam : MonoBehaviour {

	InterstitialAd interstitial;

    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;

        if (interstitial.IsLoaded() && timer <= 0)
        {
            interstitial.Show();
            RequestInterstitial();
            timer = 60.0f;
        }
    }

    void Start()
	{
        timer = 60.0f;
        RequestInterstitial();
    }

	private void RequestInterstitial()
	{
#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3255724213917516/5396514251";
#elif UNITY_IPHONE
		string adUnitId = "INSERT_IOS_INTERSTITIAL_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);

		}

		public void showInterstitialAd()
		{
		//Show Ad
		if (interstitial.IsLoaded())
		{
		interstitial.Show();
            RequestInterstitial();
        }

		}

		}
