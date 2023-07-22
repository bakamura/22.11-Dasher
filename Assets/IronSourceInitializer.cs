using System;
using UnityEngine;

public class IronSourceInitializer : Singleton<IronSourceInitializer> {

#if UNITY_ANDROID
    private readonly static string APP_KEY = "1ae7669a5";
    private readonly static string INTERSTITIAL_PLACEMENT = "Interstitial_Android";
#elif UNITY_IOS
    private static string APP_KEY = "";
    private readonly static string INTERSTITIAL_PLACEMENT = "Interstitial_iOS";
#endif

    private void Start() {
        IronSource.Agent.init(APP_KEY);
        IronSource.Agent.shouldTrackNetworkState(true);
    }

    // Why OnEnable ? check later
    private void OnEnable() {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    private void OnApplicationPause(bool isPaused) {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void SdkInitializationCompletedEvent() {
        IronSource.Agent.validateIntegration();
    }

    public enum IronSourceEvent {
        // Banner Events
        BannerAdLoadedEvent,
        //Invoked when the banner loading process has failed.
        BannerAdLoadFailedEvent,
        // Invoked when end user clicks on the banner ad
        BannerAdClickedEvent,
        //Notifies the presentation of a full screen content following user click
        BannerAdScreenPresentedEvent,
        //Notifies the presented screen has been dismissed
        BannerAdScreenDismissedEvent,
        //Invoked when the user leaves the app
        BannerAdLeftApplicationEvent,

        // Interstitial Events

        // Invoked when the interstitial ad was loaded succesfully.
        InterstitialAdReadyEvent,
        // Invoked when the initialization process has failed.
        InterstitialAdLoadFailedEvent,
        // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
        InterstitialAdOpenedEvent,
        // Invoked when end user clicked on the interstitial ad
        InterstitialAdClickedEvent,
        // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
        // This callback is not supported by all networks, and we recommend using it only if  
        // it's supported by all networks you included in your build. 
        InterstitialAdShowSucceededEvent,
        // Invoked when the ad failed to show.
        InterstitialAdShowFailedEvent,
        // Invoked when the interstitial ad closed and the user went back to the application screen.
        InterstitialAdClosedEvent,

        // Rewarded Events

        // The Rewarded Video ad view has opened. Your activity will loose focus.
        RewardedAdOpenedEvent,
        // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
        RewardedAdClosedEvent,
        // Indicates that there’s an available ad.
        // The adInfo object includes information about the ad that was loaded successfully
        // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
        RewardedAdAvailableEvent,
        // Indicates that no ads are available to be displayed
        // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
        RewardedAdUnavailableEvent,
        // The rewarded video ad was failed to show.
        RewardedAdShowFailedEvent,
        // The user completed to watch the video, and should be rewarded.
        // The placement parameter will include the reward data.
        // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
        RewardedAdRewardedEvent,
        // Invoked when the video ad was clicked.
        // This callback is not supported by all networks, and we recommend using it only if
        // it’s supported by all networks you included in your build.
        RewardedAdClickedEvent,
    }

    #region SubscribeToIronSourceEvent

    public void SubscribeToIronSourceEvent(IronSourceEvent ironSourceEvent, Action<IronSourceAdInfo> method) {
        switch (ironSourceEvent) {
            case IronSourceEvent.BannerAdLoadedEvent:
                IronSourceBannerEvents.onAdLoadedEvent += method;
                break;
            case IronSourceEvent.BannerAdClickedEvent:
                IronSourceBannerEvents.onAdClickedEvent += method;
                break;
            case IronSourceEvent.BannerAdScreenPresentedEvent:
                IronSourceBannerEvents.onAdScreenPresentedEvent += method;
                break;
            case IronSourceEvent.BannerAdScreenDismissedEvent:
                IronSourceBannerEvents.onAdScreenDismissedEvent += method;
                break;
            case IronSourceEvent.BannerAdLeftApplicationEvent:
                IronSourceBannerEvents.onAdLeftApplicationEvent += method;
                break;
            case IronSourceEvent.InterstitialAdReadyEvent:
                IronSourceInterstitialEvents.onAdReadyEvent += method;
                break;
            case IronSourceEvent.InterstitialAdOpenedEvent:
                IronSourceInterstitialEvents.onAdOpenedEvent += method;
                break;
            case IronSourceEvent.InterstitialAdClickedEvent:
                IronSourceInterstitialEvents.onAdClickedEvent += method;
                break;
            case IronSourceEvent.InterstitialAdShowSucceededEvent:
                IronSourceInterstitialEvents.onAdShowSucceededEvent += method;
                break;
            case IronSourceEvent.InterstitialAdClosedEvent:
                IronSourceInterstitialEvents.onAdClosedEvent += method;
                break;
            case IronSourceEvent.RewardedAdOpenedEvent:
                IronSourceRewardedVideoEvents.onAdOpenedEvent += method;
                break;
            case IronSourceEvent.RewardedAdClosedEvent:
                IronSourceRewardedVideoEvents.onAdClosedEvent += method;
                break;
            case IronSourceEvent.RewardedAdAvailableEvent:
                IronSourceRewardedVideoEvents.onAdAvailableEvent += method;
                break;
            default:
                Debug.LogWarning("Trying to add method '" + method.ToString() + "' to the event '" + ironSourceEvent.ToString() + "' but method parameters are of wrong type!");
                break;
        }
    }

    public void SubscribeToIronSourceEvent(IronSourceEvent ironSourceEvent, Action<IronSourceError> method) {
        switch (ironSourceEvent) {
            case IronSourceEvent.BannerAdLoadFailedEvent:
                IronSourceBannerEvents.onAdLoadFailedEvent += method;
                break;
            case IronSourceEvent.InterstitialAdLoadFailedEvent:
                IronSourceInterstitialEvents.onAdLoadFailedEvent += method;
                break;
            default:
                Debug.LogWarning("Trying to add method '" + method.ToString() + "' to the event '" + ironSourceEvent.ToString() + "' but method parameters are of wrong type!");
                break;
        }
    }

    public void SubscribeToIronSourceEvent(IronSourceEvent ironSourceEvent, Action<IronSourceError, IronSourceAdInfo> method) {
        switch (ironSourceEvent) {
            case IronSourceEvent.InterstitialAdShowFailedEvent:
                IronSourceInterstitialEvents.onAdShowFailedEvent += method;
                break;
            case IronSourceEvent.RewardedAdShowFailedEvent:
                IronSourceRewardedVideoEvents.onAdShowFailedEvent += method;
                break;
            default:
                Debug.LogWarning("Trying to add method '" + method.ToString() + "' to the event '" + ironSourceEvent.ToString() + "' but method parameters are of wrong type!");
                break;
        }
    }

    public void SubscribeToIronSourceEvent(IronSourceEvent ironSourceEvent, Action method) {
        switch (ironSourceEvent) {
            case IronSourceEvent.RewardedAdUnavailableEvent:
                IronSourceRewardedVideoEvents.onAdUnavailableEvent += method;
                break;
            default:
                Debug.LogWarning("Trying to add method '" + method.ToString() + "' to the event '" + ironSourceEvent.ToString() + "' but method parameters are of wrong type!");
                break;
        }
    }

    public void SubscribeToIronSourceEvent(IronSourceEvent ironSourceEvent, Action<IronSourcePlacement, IronSourceAdInfo> method) {
        switch (ironSourceEvent) {
            case IronSourceEvent.RewardedAdRewardedEvent:
                IronSourceRewardedVideoEvents.onAdRewardedEvent += method;
                break;
            case IronSourceEvent.RewardedAdClickedEvent:
                IronSourceRewardedVideoEvents.onAdClickedEvent += method;
                break;
            default:
                Debug.LogWarning("Trying to add method '" + method.ToString() + "' to the event '" + ironSourceEvent.ToString() + "' but method parameters are of wrong type!");
                break;
        }
    }

    #endregion


    public void BannerLoad(bool isLoading) {
        if (isLoading) IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.TOP);
        else IronSource.Agent.destroyBanner();
    }

    public void BannerShow(bool isShowing) {
        if (isShowing) IronSource.Agent.displayBanner();
        else IronSource.Agent.hideBanner();
    }

    public bool InterstitialLoad() {
        if (!IronSource.Agent.isInterstitialReady()) {
            IronSource.Agent.loadInterstitial();
            return true;
        }
        Debug.LogWarning("Avoided loading aditional Interstitial Ad");
        return false;
    }

    public void InterstitialShow() {
        if (IronSource.Agent.isInterstitialReady() && IronSource.Agent.isInterstitialPlacementCapped(INTERSTITIAL_PLACEMENT)) {
            IronSource.Agent.showInterstitial(INTERSTITIAL_PLACEMENT);
        }
        else {
            // Log stuff
        }
    }

    public bool RewardadeShow() {
        if (IronSource.Agent.isRewardedVideoAvailable()) {
            IronSource.Agent.showRewardedVideo();
            return true;
        }
        // Log Stuff
        return false;
    }

}
