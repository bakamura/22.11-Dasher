using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSwitching : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] private bool _switchState;
    [SerializeField] private Sprite[] _spriteSwitch;

    private void Switch() {
        _switchState = !_switchState;

    }

#if UNITY_EDITOR
    private bool _switchStatePrevious;
    private void OnValidate() {
        if (_switchStatePrevious != _switchState) {
            _switchStatePrevious = _switchState;
            Switch();
        }
    }
#endif
    }
