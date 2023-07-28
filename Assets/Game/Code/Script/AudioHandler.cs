using System.Collections.Generic;
using UnityEngine;

public abstract class AudioHandler : MonoBehaviour {

    [SerializeField] private AudioClip[] _SFXs;
    private Dictionary<string, int> _SFXDict = new Dictionary<string, int>();

    [Header("Cache")]

    private AudioSource _as;
    private int _sfxIDCache;

    protected virtual void Awake() {
        _as = GetComponent<AudioSource>();

        for (int i = 0; i < _SFXs.Length; i++) _SFXDict.Add(_SFXs[i].name, i);
    }

    protected void PlaySound(string name) {
        if (_SFXDict.TryGetValue(name, out _sfxIDCache)) {
            _as.clip = _SFXs[_sfxIDCache];
            _as.Play();
        }
        else Debug.Log("Could not find sound with name " + name);
    }
}
