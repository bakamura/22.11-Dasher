using UnityEngine;

using RDG;

public class HapticHandler : Singleton<HapticHandler> {

    [Header("Parameters")]

    [SerializeField] private long _hapticDurationMilsec;
    [SerializeField] private byte _hapticAmplitude;

    private void HapticFeedback(Vector2 v2) {
        Vibration.Vibrate(_hapticDurationMilsec, _hapticAmplitude, true);
    }

    public void HapticToggle(bool on) {
        if (on) PlayerDash.instance.onDash.AddListener(HapticFeedback);
        else PlayerDash.instance.onDash.RemoveListener(HapticFeedback);
    }

}
