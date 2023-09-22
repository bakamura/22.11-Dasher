using System.Collections;
using UnityEngine;

public class MusicLoop : MonoBehaviour {

    [Header("Properties")]

    [SerializeField] private float _startTime;

    [Header("Cache")]

    private AudioSource _as;
    private WaitForSeconds _loopCheckWait;

    private void Awake() {
        _as = GetComponent<AudioSource>();
        StartCoroutine(TryLoop());
    }

    private IEnumerator TryLoop() {
        yield return _loopCheckWait;

        while (_as.isPlaying) yield return null;

        _as.time = _startTime;
        _as.Play();

        StartCoroutine(TryLoop());
    } 

}
