using UnityEngine;
using System.Collections;
using UnityEditor;

public class MakeScriptableObject
{
    [MenuItem("Assets/Create/DrinkRecipe")]
    public static void CreateMyAsset()
    {
        DrinkRecipe asset = ScriptableObject.CreateInstance<DrinkRecipe>();

        AssetDatabase.CreateAsset(asset, "Assets/Recipes/NewRecipe.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}