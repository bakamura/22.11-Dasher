using UnityEngine;

public class HapticHandler : Singleton<HapticHandler> {

//#if UNITY_ANDROID && !UNITY_EDITOR
//    private AndroidJavaObject _unityPlayer; // Needs to be public static ?
//    private AndroidJavaObject _currentActivity;
//    private AndroidJavaObject _vibrator;
//#endif
    // private float _vibrationTime = 200; // in Milliseconds

    protected override void Awake() {
        base.Awake();

//#if UNITY_ANDROID && !UNITY_EDITOR
//            Debug.Log("Playing in Android Phone");
//            _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
//            _vibrator = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
//        }
//#endif
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(HapticFeedback);
    }

    private void HapticFeedback(Vector2 v2) {
        Handheld.Vibrate();
//#if UNITY_ANDROID && !UNITY_EDITOR
//        _vibrator.Call("vibrate", _vibrationTime);
//#endif
    }

}
