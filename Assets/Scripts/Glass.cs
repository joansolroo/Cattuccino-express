using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour {

    [SerializeField] public LiquidContainer container;
    public GameObject label;
    public DrinkRecipe recipe;

    public bool taken = false;
    public bool landed = false;

    move move;
    static HashSet<DrinkRecipe> ServedDrinks = new HashSet<DrinkRecipe>();
    private void OnDestroy()
    {
        Destroy(label);
    }

    private void Start()
    {
        move = this.GetComponent<move>();
        move.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!landed)
        {
            Land();
        }
    }
    void Land()
    {
        landed = true;
        move.enabled = true;
        if (!ServedDrinks.Contains(recipe))
        {
            RecipeHelper.ShowRecipe(recipe);
            ServedDrinks.Add(recipe);
        }
    }
}
