using UnityEngine;

public class SfxHandler : Singleton<SfxHandler> {

    [Header("SFX List")]

    [SerializeField] private AudioClip _dashSfx;
    [SerializeField] private AudioClip _dashResetSfx;
    [SerializeField] private AudioClip _dashResetterSfx;
    [SerializeField] private AudioClip _uiUpSfx;
    [SerializeField] private AudioClip _uiDownSfx;
    [SerializeField] private AudioClip _victorySfx;

    [Header("Cache")]

    private AudioSource _as;

    protected override void Awake() {
        base.Awake();

        _as = GetComponent<AudioSource>();
    }

    private void Start() {
        PlayerDash.instance.onDash.AddListener(DashSfx);
        PlayerDash.instance.onDashReady.AddListener(DashResetSfx);
        Goal.onGoal.AddListener(VictorySFX);
    }

    // Public?
    public void PlaySfx(AudioClip sfx) {
        _as.PlayOneShot(sfx);
    }

    private void DashSfx(Vector2 v2) {
        PlaySfx(_dashSfx);
    }

    private void DashResetSfx() {
        PlaySfx(_dashResetSfx);
    }

    public void UiUpSfx() {
        PlaySfx(_uiUpSfx);
    }

    public void UiDownSfx() {
        PlaySfx(_uiDownSfx);
    }

    private void VictorySFX() {
        PlaySfx(_victorySfx);
    }

}
