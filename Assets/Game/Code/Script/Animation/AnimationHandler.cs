using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AnimationHandler : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] protected Animation[] _animation;
    protected Dictionary<string, int> _animationDict = new Dictionary<string, int>();
    private WaitForSeconds[] _animationWait;
    protected int _currentAnimation = -1; // Starts negative for the comparison in ChangeAnimation
    private int _currentFrame;

    [Header("Cache")]

    private bool _useSr = true;
    protected SpriteRenderer _sr;
    protected Image _img;
    public SpriteRenderer SpriteRenderer { get { return _sr; } }
    private Coroutine _currentCoroutine;

    protected virtual void Awake() {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null) {
            _img = GetComponent<Image>();
            _useSr = false;
        }

        _animationWait = new WaitForSeconds[_animation.Length];
        for (int i = 0; i < _animation.Length; i++) {
            _animationDict.Add(_animation[i].name, i);
            _animationWait[i] = new WaitForSeconds(1 / _animation[i].speed);
        }

        _currentCoroutine = StartCoroutine(EmptyRoutine());
        ChangeAnimation(_animation[0].name);
    }

    protected void ChangeFrame() {
        if (++_currentFrame >= _animation[_currentAnimation].sprites.Length) {
            if (_animation[_currentAnimation].loop) _currentFrame = 0;
            else return;
        }
        if (_useSr) {
            _sr.enabled = _animation[_currentAnimation].sprites[_currentFrame] != null;
            _sr.sprite = _animation[_currentAnimation].sprites[_currentFrame];
        }
        else {
            _img.sprite = _animation[_currentAnimation].sprites[_currentFrame];
            _img.enabled = _animation[_currentAnimation].sprites[_currentFrame] != null;
            }
    }

    protected void ChangeAnimation(string animationName) {
        int previousAnimation = _currentAnimation;
        if (_animationDict.TryGetValue(animationName, out _currentAnimation)) {
            if (previousAnimation == _currentAnimation) return;
            _currentFrame = -1; // Because frame is incremented before aplying
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(Animation());
        }
        else Debug.LogWarning("Animation with name '" + animationName + "' does not exist");
    }

    protected IEnumerator Animation() {
        while (true) {
            ChangeFrame();
            yield return _animationWait[_currentAnimation];
        }
    }

    public float GetAnimationDuration(string animationName) {
        if (_animationDict.TryGetValue(animationName, out int animationIndex)) {
            return _animation[animationIndex].sprites.Length / _animation[animationIndex].speed;
        }
        else {
            Debug.LogWarning("Animation with name '" + animationName + "' does not exist");
            return 0;
        }
    }

    // Used to avoid checking if Coroutine is null every frame
    private IEnumerator EmptyRoutine() {
        yield return null;
    }
}