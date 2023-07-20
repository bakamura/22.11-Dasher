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

    public void ToggleMusic() {
        _musicBtnImage.sprite = _musicBtnImage.sprite == _musicBtnSprite[0] ? _musicBtnSprite[1] : _musicBtnSprite[0];

        _mixer.GetFloat("VolMusic", out float f);
        _mixer.SetFloat("VolMusic", f == -80f ? 0 : -80f);
    }

    public void ToggleSfx() {
        _sfxBtnImage.sprite = _sfxBtnImage.sprite == _sfxBtnSprite[0] ? _sfxBtnSprite[1] : _sfxBtnSprite[0];

        _mixer.GetFloat("VolSfx", out float f);
        _mixer.SetFloat("VolSfx", f == -80f ? 0 : -80f);
    }

}
