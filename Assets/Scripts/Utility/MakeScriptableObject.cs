using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class MakeScriptableObject
{
    [MenuItem("Assets/Create/ScriptableObjects/NumberFont")]

    public static void CreateMyAsset()
    {
        ScriptableNumberFont asset = ScriptableObject.CreateInstance<ScriptableNumberFont>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/NumberFonts/NewNumberFont.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }//end CreateMyAsset
}//end class
#endif