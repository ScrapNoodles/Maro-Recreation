using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class NumberDisplayDefenition : MonoBehaviour
{
    public string _numericValue = "";
    public string _oldNumericValue = "";
    public bool _enableConversion = false;
    private bool _converted = false;

    public int _numDigits = 1;
    public GameObject _defaultObj;
    public Sprite _noNumberImage;

    public int _spacePadding = 5;
    private int _lastValuePadding = 5;

    public ScriptableNumberFont _numberSprites;

    public void CreateNewDigits()
    {
        _converted = false;

        for (int i = transform.childCount; i < _numDigits; i++) 
        {
            //Add digits as children of the number display object
            GameObject temp = Instantiate(_defaultObj, transform);
            temp.GetComponent<Image>().sprite = _noNumberImage;

        }//end for

        UpdateSpacing();

    }//end CreateNewDigits

    public void UpdateSpacing()
    {
        for(int i = 0; i < _numDigits; i++)
        {
            GameObject temp = transform.GetChild(i).gameObject;
            RectTransform pos = temp.GetComponent<RectTransform>();
            pos.localPosition = new Vector3(i*(pos.sizeDelta.x + _spacePadding), 0 , pos.localPosition.z);

        }//end for
    }//end UpdateSpacing

    public void DeleteOldDigits()
    {
        _converted = false;

        for (int i = transform.childCount - 1; i>=_numDigits; i--)
        {
            //remove the children from bottom
            DestroyImmediate(transform.GetChild(i).gameObject.GetComponent<Image>());
            DestroyImmediate(transform.GetChild(i).gameObject);

        }//end for

    }//end DeleteOldDigits

    public void ConvertToSprites()
    {
        string splitVal = _numericValue;
        if (splitVal.Length > _numDigits)
        {
            Debug.LogWarning("your number is longer than digits, increasing digits");
            _numDigits = splitVal.Length;
            CreateNewDigits();
        }//end if
        else if (splitVal.Length < _numDigits)
        {
            Debug.LogWarning("your number is shorter than digits, decreasing digits");
            DeleteOldDigits();
        }//end else if

        for (int i = 0; i < _numDigits; i++)
        {
            //update digits to relfect numbers
            transform.GetChild(i).GetComponent<Image>().sprite = _numberSprites._numberSprites[int.Parse(splitVal[i].ToString())];
        }//end ofr

        _converted = true;

    }//end ConvertToSprites

    public void LockVariables()
    {
        if (_enableConversion)
        {
            _numDigits = _numericValue.Length;
        }//end if
    }//end LockVariables

    private void Update()
    {
        if (!_converted && _enableConversion)
        {
            ConvertToSprites();
        }//end if

        LockVariables();

        if(gameObject.transform.childCount < _numDigits)
        {
            CreateNewDigits();
        }//end if

        else if (gameObject.transform.childCount > _numDigits)
        {
            DeleteOldDigits();
        }//end elseif

        if(_numericValue != _oldNumericValue)
        {
            _converted = false;
        }//end if

        if(_spacePadding != _lastValuePadding)
        {
            UpdateSpacing();
        }//end if 

        _oldNumericValue = _numericValue;
        _lastValuePadding = _spacePadding;

    }//end Update

}//end class
