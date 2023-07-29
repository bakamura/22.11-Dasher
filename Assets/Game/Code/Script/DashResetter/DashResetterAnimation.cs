using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashResetterAnimation : AnimationHandler {

    [Header("Cache")]

    private const string RESETTER_SPAWN = "ResetterSpawn";
    private const string RESETTER_POP = "ResetterPop";

    public void SpawnAnimation() {
        ChangeAnimation(RESETTER_SPAWN);
    }

    public void BreakAnimation() {
        ChangeAnimation(RESETTER_POP);
    }

}
