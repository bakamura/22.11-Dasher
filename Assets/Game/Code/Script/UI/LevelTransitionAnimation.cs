using UnityEngine;

public class LevelTransitionAnimation : AnimationHandler {

    [Header("Cache")]

    public const string TRANSITION_START = "TransitionStart";
    public const string TRANSITION_END = "TransitionEnd";

    public void TransitionStartAnimation() {
        ChangeAnimation(TRANSITION_START);
    }

    public void TransitionEndAnimation() {
        ChangeAnimation(TRANSITION_END);
    }

}
