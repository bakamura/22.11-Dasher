using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _debugUiText;

    private void Start() {
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdReadyEvent, AdLoadEvent);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdLoadFailedEvent, AdLoadFailEvent);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdOpenedEvent, AdShowEvent);
        IronSourceHandler.instance.SubscribeToIronSourceEvent(IronSourceHandler.IronSourceEvent.InterstitialAdShowFailedEvent, AdShowFailEvent);
        _debugUiText.text = "";
    }

    //public void LoadInterstitialAd() {
    //    IronSourceHandler.instance.InterstitialLoad();
    //}

    public void ShowInterstitialAd() {
        IronSourceHandler.instance.InterstitialShow();
    }

    private void AdLoadEvent(IronSourceAdInfo adInfo) { _debugUiText.text = "Ad Load Event"; }
    private void AdLoadFailEvent(IronSourceError adError) { _debugUiText.text = "Ad Load Fail Event"; }
    private void AdShowEvent(IronSourceAdInfo adInfo) { _debugUiText.text = "Ad Show Event"; }
    private void AdShowFailEvent(IronSourceError adError, IronSourceAdInfo adInfo) { _debugUiText.text = "Ad Show Fail Event"; }
}
