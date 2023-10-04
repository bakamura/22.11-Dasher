public class PlatformSwitchingAnimation : AnimationHandler {

    private const string SWITCH_ON = "PlatformSwitchOn";
    private const string SWITCH_OFF = "PlatformSwitchOff";

    public void SwitchAnimation(bool switchState) {
        ChangeAnimation(switchState ? SWITCH_ON : SWITCH_OFF);
    }

}
