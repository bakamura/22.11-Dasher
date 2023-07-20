using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceInitializer : MonoBehaviour {

#if UNITY_ANDROID
    private static string APP_KEY = "1ae7669a5";
#elif UNITY_IOS
    private static string APP_KEY = "";
#endif

    private void Start() {
        IronSource.Agent.init(APP_KEY);
    }

    private void OnEnable() {
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
    }

    private void OnApplicationPause(bool isPaused) {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    private void SdkInitializationCompletedEvent() {
        IronSource.Agent.validateIntegration();
    }
}
