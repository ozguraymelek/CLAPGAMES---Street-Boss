using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class BannerAdsNew : MonoBehaviour {

	private BannerView bannerView;

	void Start()
	{

	}

	public void Awake()
	{
        
            this.RequestBanner();
        
	}

	private void RequestBanner()
	{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-3255724213917516/5496037863";
		#elif UNITY_IPHONE
		string adUnitId = "";
		#else
		string adUnitId = "unexpected_platform";
		#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        this.bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);
		}

		public void hideAd(){
		bannerView.Hide ();
		}

		public void ShowAd(){
		bannerView.Show ();
		}

		public void YoketAd(){
		bannerView.Hide ();
		bannerView.Destroy ();
		}
}
