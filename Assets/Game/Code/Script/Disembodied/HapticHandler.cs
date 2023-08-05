using UnityEngine;

public class HapticHandler : Singleton<HapticHandler> {

#if UNITY_ANDROID
        private int _hapticFeedbackConstantsKey;
        private AndroidJavaObject _unityPlayer;
#endif

    protected override void Awake() {
        base.Awake();

#if UNITY_ANDROID
        _hapticFeedbackConstantsKey = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
        _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
#endif  
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(HapticFeedback);
    }

    private void HapticFeedback(Vector2 v2) {
#if UNITY_ANDROID
        //Handheld.Vibrate();
        _unityPlayer.Call<bool>("performHapticFeedback", _hapticFeedbackConstantsKey);
#endif
    }

}
