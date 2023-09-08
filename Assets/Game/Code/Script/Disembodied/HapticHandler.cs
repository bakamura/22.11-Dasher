using UnityEngine;

public class HapticHandler : Singleton<HapticHandler> {

#if UNITY_ANDROID
    private AndroidJavaObject _unityPlayer; // Needs to be public static ?
    private AndroidJavaObject _currentActivity;
    private AndroidJavaObject _vibrator;
#endif
    private float _vibrationTime = 200; // in Milliseconds

    protected override void Awake() {
        base.Awake();

        if (IsAndroid()) {
            Debug.Log("Playing in Android Phone");
            _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
        }
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(HapticFeedback);
    }

    private void HapticFeedback(Vector2 v2) {
        if (IsAndroid()) {
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
