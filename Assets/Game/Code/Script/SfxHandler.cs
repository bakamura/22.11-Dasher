using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxHandler : MonoBehaviour {

    [Header("SFX List")]

    [SerializeField] private AudioClip _dashSfx;
    [SerializeField] private AudioClip _uiClickSfx;
    [SerializeField] private AudioClip _victorySfx;

    [Header("Cache")]

    private AudioSource _as;

    private void Awake() {
        _as = GetComponent<AudioSource>();
    }

    private void Start() {
        FindObjectOfType<PlayerDash>().onDash.AddListener(DashSfx);
        Goal.onGoal.AddListener(VictorySFX);
    }

    public void PlaySfx(AudioClip sfx) {
        //_as.clip = sfx;
        _as.PlayOneShot(sfx);
    }

    private void DashSfx(Vector2 v2) {
        PlaySfx(_dashSfx);
    }

    public void UiClickSfx() {
        PlaySfx(_uiClickSfx);
    }

    private void VictorySFX() {
        PlaySfx(_victorySfx);
    }

}
