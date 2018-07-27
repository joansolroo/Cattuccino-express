using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(Popup))]
public class RecipeHelper : MonoBehaviour {

    static RecipeHelper instance;
    Popup popup;

    [SerializeField] Text drinkName;
    [SerializeField] Text recipe;

	// Use this for initialization
	void Awake () {
        instance = this;
	}
    private void Start()
    {
        popup = GetComponent<Popup>();
        popup.HideImmediately();
    }

    public static void ShowRecipe(DrinkRecipe _recipe)
    {
        instance.popup.Show();
        instance.drinkName.text = _recipe.drinkName;
        string r = "Recipe:\n";
        for (int i = 0; i < _recipe.ingredients.Length; ++i)
        {
            r+= ""+ (int)(_recipe.amounts[i]/50) +"x " +_recipe.ingredients[i].ToString();
            if (i < _recipe.ingredients.Length - 1) r += "\n";
        }
        instance.recipe.text = r;
    }
}
