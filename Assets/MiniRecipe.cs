using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class MiniRecipe : MonoBehaviour {

    [SerializeField] DrinkRecipe recipe;

    [SerializeField] Text recipeName;
    [SerializeField] Image[] ingredients;

    Color[] ingredientColors;
    [SerializeField] Color defaultColor = new Color(1,1,1,0.25f);
    public static int minirecipes = 0;

    public void Show(DrinkRecipe _recipe)
    {
        this.recipe = _recipe;
        recipeName.text = recipe.name;
        for (int i = 0; i < ingredients.Length ; ++i)
        {
            if (i < recipe.ingredients.Length)
            {
                ingredients[i].color = LiquidGenarate.liquidColor[recipe.ingredients[i]];
            }
            else
            {
                ingredients[i].color = defaultColor;
            }
        }
        
        minirecipes++;

        GetComponent<Popup>().Show();
    }
}
