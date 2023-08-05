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

    private void Start() {
        if (!SaveSystem.instance.settings.musicOn) ToggleMusic();
        if (!SaveSystem.instance.settings.sfxOn) ToggleSfx();
    }

    public void ToggleMusic() {
        _mixer.GetFloat("VolMusic", out float f);

        bool b = f == -80f;
        _mixer.SetFloat("VolMusic", b ? 0f : -80f);
        _musicBtnImage.sprite = b ? _musicBtnSprite[0] : _musicBtnSprite[1];
        SaveSystem.instance.ToggleAudio(SaveSystem.AudioType.Music, b);
    }

    public void ToggleSfx() {
        _mixer.GetFloat("VolSfx", out float f);

        bool b = f == -80f;
        _mixer.SetFloat("VolSfx", b ? 0 : -80f);
        _sfxBtnImage.sprite = b ? _sfxBtnSprite[0] : _sfxBtnSprite[1];
        SaveSystem.instance.ToggleAudio(SaveSystem.AudioType.Sfx, b);
    }

}
