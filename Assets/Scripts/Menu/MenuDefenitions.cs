using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum MenuType
{
    HORIZONTAL, 
    VERTICAL
}
public class MenuDefenition : MonoBehaviour
{
    public MenuType _menuType = MenuType.HORIZONTAL;
    public List<GameObject> _menuButtonObjects = new List<GameObject>();
    public AudioClip _menuMusic;
    public bool _continuePrevMusic = false;

    private List<ButtonDefenition> _menuButtonDefenitions = new List<ButtonDefenition>();
    private List<Button> _menuButtons = new List<Button>();
    private List<Animator> _menuAnimators = new List<Animator>();

    public void Start()
    {
        //Searches and grabs components
        for(int i = 0; i < _menuButtonObjects.Count; i++)
        {
            _menuButtonDefenitions.Add(_menuButtonObjects[i].GetComponent<ButtonDefenition>());
            _menuButtons.Add(_menuButtonObjects[1].GetComponent<Button>());

            Animator temp = null;
            _menuButtonObjects[i].TryGetComponent(out temp);

            _menuAnimators.Add(temp);
        }
    }

    public MenuType GetMenuType()
    {
        return _menuType;
    }

    public int GetButtonCount()
    {
        return _menuButtonObjects.Count;
    }

    public List<ButtonDefenition> GetButtonDefenitions()
    {
        return _menuButtonDefenitions;
    }

    public List<Button> GetButtons()
    {
        return _menuButtons;
    }

    public List<Animator> GetAnimations()
    {
        return _menuAnimators;
    }
}
