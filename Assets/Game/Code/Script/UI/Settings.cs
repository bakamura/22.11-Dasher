using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    [Header("Audio")]

    [SerializeField] private AudioMixer _mixer;

    [Header("Button Image")]

    [SerializeField] private Image _musicBtnImage; 
    [SerializeField] private Sprite[] _musicBtnSprite = new Sprite[2];

    [Space(16)]

    [SerializeField] private Image _sfxBtnImage;
    [SerializeField] private Sprite[] _sfxBtnSprite = new Sprite[2];

    [Space(16)]

    [SerializeField] private Image _hapticBtnImage;
    [SerializeField] private Sprite[] _hapticBtnSprite = new Sprite[2];
    private bool _hapticOn = false;

    private void Start() {
        if (!SaveSystem.instance.settings.musicOn) ToggleMusic();
        if (!SaveSystem.instance.settings.sfxOn) ToggleSfx();
        if (SaveSystem.instance.settings.hapticOn) ToggleHaptic();
    }

    public void ToggleMusic() {
        _mixer.GetFloat("VolMusic", out float f);

        bool on = f == -80f;
        _mixer.SetFloat("VolMusic", on ? 0f : -80f);
        _musicBtnImage.sprite = _musicBtnSprite[on ? 0 : 1];
        SaveSystem.instance.ToggleAudio(SaveSystem.AudioType.Music, on);
    }

    public void ToggleSfx() {
        _mixer.GetFloat("VolSfx", out float f);

        bool on = f == -80f;
        _mixer.SetFloat("VolSfx", on ? 0 : -80f);
        _sfxBtnImage.sprite = _sfxBtnSprite[on ? 0 : 1];
        SaveSystem.instance.ToggleAudio(SaveSystem.AudioType.Sfx, on);
    }

    public void ToggleHaptic() {
        _hapticOn = !_hapticOn;

        HapticHandler.instance.HapticToggle(_hapticOn);
        _hapticBtnImage.sprite = _hapticBtnSprite[_hapticOn ? 0 : 1];
        SaveSystem.instance.ToggleHaptic(_hapticOn);
    }
}
