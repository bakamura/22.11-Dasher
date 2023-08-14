using UnityEngine;

public class HapticHandler : Singleton<HapticHandler> {

#if UNITY_ANDROID
    //private int _hapticFeedbackConstantsKey;
    private AndroidJavaObject _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); // Needs to be public static ?
    private AndroidJavaObject _currentActivity;
    private AndroidJavaObject _vibrator;
    private float _vibrationTime = 200; // in Milliseconds
#endif

    protected override void Awake() {
        base.Awake();

        if (IsAndroid()) {
            //_hapticFeedbackConstantsKey = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
            //_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");

            //_unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "Vibrator");
        }
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(HapticFeedback);
    }

    private void HapticFeedback(Vector2 v2) {
        if (IsAndroid()) {
            //_unityPlayer.Call<bool>("performHapticFeedback", _hapticFeedbackConstantsKey);

            _vibrator.Call("vibrate", _vibrationTime);
        }
        else {
            Handheld.Vibrate();
        }
    }

    private bool IsAndroid() {
#if UNITY_ANDROID && !UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }
}
