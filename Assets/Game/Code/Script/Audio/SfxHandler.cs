using UnityEngine;

public class SfxHandler : Singleton<SfxHandler> {

    [Header("Player SFX List")]

    [SerializeField] private AudioClip _dashSfx;
    [SerializeField] private AudioClip _landSfx;
    [SerializeField] private AudioClip _dashResetSfx;

    [Header("UI SFX List")]

    [SerializeField] private AudioClip _uiUpSfx;
    [SerializeField] private AudioClip _uiDownSfx;
    [SerializeField] private AudioClip _victorySfx;
    
    [Header("Level Mechanic SFX List")]

    [SerializeField] private AudioClip _dashResetterSfx;
    [SerializeField] private AudioClip _platformBreakingStartSfx;
    [SerializeField] private AudioClip _platformBreakingSfx;
    [SerializeField] private AudioClip _platformSwitchingSfx;
    [SerializeField] private AudioClip _holeteleportSfx;

    [Header("Cache")]

    private AudioSource _as;

    protected override void Awake() {
        base.Awake();

        _as = GetComponent<AudioSource>();
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(DashSfx);
        PlayerDash.instance.onLand.AddListener(LandSfx);
        PlayerDash.instance.onDashReady.AddListener(DashResetSfx);

        Goal.onGoal.AddListener(VictorySFX);

        DashResetter.onResetDash.AddListener(DashResetterSfx);
        PlatformBreaking.onBreakingStart.AddListener(PlatformBreakingStartSfx);
        PlatformBreaking.onBreaking.AddListener(PlatformBreakingSfx);
        PlatformSwitching.onSwitch.AddListener(PlatformSwitchingSfx);
        HoleTeleport.onTeleport.AddListener(HoleTeleportSfx);
    }

    // Public?
    public void PlaySfx(AudioClip sfx) {
        _as.PlayOneShot(sfx);
    }

    // Player

    private void DashSfx(Vector2 v2) {
        PlaySfx(_dashSfx);
    }

    private void LandSfx() {
        PlaySfx(_landSfx);
    }

    private void DashResetSfx() {
        PlaySfx(_dashResetSfx);
    }

    // UI

    public void UiUpSfx() {
        PlaySfx(_uiUpSfx);
    }

    public void UiDownSfx() {
        PlaySfx(_uiDownSfx);
    }

    private void VictorySFX() {
        PlaySfx(_victorySfx);
    }

    // Level Mechanic

    private void DashResetterSfx() {
        PlaySfx(_dashResetterSfx);
    }

    private void PlatformBreakingStartSfx() {
        PlaySfx(_platformBreakingStartSfx);
    }

    private void PlatformBreakingSfx() {
        PlaySfx(_platformBreakingSfx);
    }

    private void PlatformSwitchingSfx() {
        PlaySfx(_platformSwitchingSfx);
    }

    private void HoleTeleportSfx() {
        PlaySfx(_holeteleportSfx);
    }

}
