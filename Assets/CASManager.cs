using System;
using System.Collections;
using System.Collections.Generic;
using CAS.AdObject; // Make sure this is the correct namespace for BannerAdObject
using UnityEngine;

public class CASManager : MonoBehaviour
{
    public CASManager Instance { get; private set; }

    // Ensure BannerAdObject.Instance is a valid singleton instance
    BannerAdObject banner;
    InterstitialAdObject interstitialAd;
    RewardedAdObject rewarded;

    ReturnToPlayAdObject returnAdObject;

    private void Awake()
    {
        if (Instance == null || Instance != this)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        PublishingEvents.events.Lost.AddListener(ShowInterstitial);
        PublishingEvents.events.RewardedAd.AddListener(ShowRewarded);

        interstitialAd = GetComponent<InterstitialAdObject>();
        OnBannerAdLoaded();

        
        rewarded = GetComponent<RewardedAdObject>();
        rewarded.LoadAd();

        returnAdObject = GetComponent<ReturnToPlayAdObject>();
        returnAdObject.allowReturnToPlayAd = true;

        
        DontDestroyOnLoad(this);
    }

    void OnBannerAdLoaded()
    {
        banner = BannerAdObject.Instance;
        banner.gameObject.SetActive(true);
    }

    public void OnInterstitialLoaded()
    {
        // interstitialAd.Present();
    }

    public void ShowInterstitial()
    {
        interstitialAd.Present();
    }

    public void ShowRewarded()
    {
        rewarded.Present();
    }
}