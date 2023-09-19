
public class PlatformBreakingAnimation : AnimationHandler {

    private const string BREAK = "PlatformBreak";
    private const string RESPAWN = "PlatformRespawn";
    private const string NULL = "Null";

    public void BreakAnimation() {
        ChangeAnimation(BREAK);
    }

    public void RespawnAnimation() {
        ChangeAnimation(RESPAWN);
    }

    public void NullAnimation() {
        ChangeAnimation(NULL);
    }

    public float BreakAnimationDuration() {
        return GetAnimationDuration(BREAK);
    }

}
