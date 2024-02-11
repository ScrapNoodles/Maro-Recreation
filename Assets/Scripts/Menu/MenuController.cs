using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject _activeMenu;
    public AudioSource _backGroundAudio;

    public List<KeyCode> _increaseVert;
    public List<KeyCode> _decreaseVert;
    public List<KeyCode> _increaseHorizontal;
    public List<KeyCode> _decreaseHorizontal;
    public List<KeyCode> _ConfirmButtons;

    private MenuDefenition _activeMenuDefinition;
    private int _activeButton = 0;

    public void Start()
    {
        UpdateActiveMenuDefinition();
    }

    public void UpdateActiveMenuDefinition()
    {
        _activeMenuDefinition = _activeMenu.GetComponent<MenuDefenition>();

        if (_activeMenuDefinition._menuMusic != null)
        {
            _backGroundAudio.clip = _activeMenuDefinition._menuMusic;
            _backGroundAudio.Play();
        }
        else if (!_activeMenuDefinition._continuePrevMusic)
        {
            _backGroundAudio.Stop();
        }
    }

    public void Update()
    {
        switch (_activeMenuDefinition.GetMenuType())
        {
            case MenuType.HORIZONTAL:
                MenuInput(_increaseHorizontal, _decreaseHorizontal);
                break;

            case MenuType.VERTICAL:
                MenuInput(_increaseVert, _decreaseVert);
                break;
        }
    }

    private void MenuInput(List<KeyCode> increase, List<KeyCode> decrease)
    {
        int newActive = _activeButton;

        for (int i = 0; i < increase.Count; i++)
        {
            if (Input.GetKeyDown(increase[i]))
            {
                newActive = SwitchCurrentButton(1);
            }
        }

        for (int i = 0; i < decrease.Count; i++)
        {
            if (Input.GetKeyDown(decrease[i]))
            {
                newActive = SwitchCurrentButton(-1);
            }
        }

        for (int i = 0; i < _ConfirmButtons.Count; i++)
        {
            if (Input.GetKeyDown(_ConfirmButtons[i]))
            {
                ClickCurrentButton();
            }
        }

        _activeButton = newActive;
    }

    private int SwitchCurrentButton(int increment)
    {
        if (!_activeMenuDefinition.GetButtonDefenitions()[_activeButton].GetDisableControls())
        {
            int newActive = Utility.WrapAround(_activeMenuDefinition.GetButtonCount(), _activeButton, increment);

            _activeMenuDefinition.GetButtonDefenitions()[_activeButton].SwappedOff();

            Debug.Log(_activeButton);
            Debug.Log(newActive);
            _activeMenuDefinition.GetButtonDefenitions()[newActive].SwappedTo();

            return newActive;
        }
        else
        {
            return _activeButton;
        }
    }


    private void ClickCurrentButton()
    {
        if (!_activeMenuDefinition.GetButtonDefenitions()[_activeButton].GetDisableControls())
        {
            StartCoroutine(_activeMenuDefinition.GetButtonDefenitions()[_activeButton].ClickButton());
        }
    }


    public void setActiveMenu(GameObject activeMenu)
    {
        _activeMenu = activeMenu;

        UpdateActiveMenuDefinition();
    }
}
