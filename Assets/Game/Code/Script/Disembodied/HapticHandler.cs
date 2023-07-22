using UnityEngine;

public class HapticHandler : Singleton<HapticHandler> {

    private void Start() {
        PlayerDash.instance.onDash.AddListener(HapticFeedback);
    }

    private void HapticFeedback(Vector2 v2) {
#if UNITY_ANDROID
        Handheld.Vibrate();
#endif
    }

}
