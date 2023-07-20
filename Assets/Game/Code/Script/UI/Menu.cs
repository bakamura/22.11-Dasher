using UnityEngine;

public class Menu : MonoBehaviour {

    [Header("Parameters")]

    [SerializeField] protected GameObject _currentMenu;

    private void Start() {
        OpenMenu(_currentMenu);
    }

    public void OpenMenu(GameObject menu) {
        if(_currentMenu) _currentMenu.SetActive(false);
        _currentMenu = menu;
        if (_currentMenu) _currentMenu.SetActive(true);
    }

}
