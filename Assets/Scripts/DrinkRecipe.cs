using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkRecipe : ScriptableObject {

    [SerializeField] public string drinkName;
    [SerializeField] public int cupSize;
    [SerializeField] public int totalAmount;


    [SerializeField] public LiquidParticle.LiquidType[] ingredients;
    [SerializeField] public int[] amounts;

    public bool ContainsIngredient(LiquidParticle.LiquidType ingredient)
    {
        return ContainsIngredient(ingredients, ingredient);
    }
    public static bool ContainsIngredient(LiquidParticle.LiquidType[] ingredients, LiquidParticle.LiquidType ingredient)
    {
        foreach (var ingredient2 in ingredients)
        {
            if (ingredient == ingredient2) return true;
        }
        return false;
    }

    public static int FindIngredient(LiquidParticle.LiquidType[] ingredients, LiquidParticle.LiquidType ingredient)
    {
        int idx = 0;
        foreach (var ingredient2 in ingredients)
        {
            if (ingredient == ingredient2) return idx;
            idx++;
        }
        return -1;
    }

    public bool SameIngredients(LiquidParticle.LiquidType[] ingredients2)
    {
        return SameIngredients(ingredients, ingredients2);
    }
    public int ExpectedAmount(LiquidParticle.LiquidType ingredient)
    {
        int idx1 = FindIngredient(ingredients, ingredient);
        if(idx1 == -1)
        {
            return 0;
        }
        else
        {
            return amounts[idx1];
        }
    }

    public int ScoredRecipe(LiquidParticle.LiquidType[] ingredients2, int[] amounts2)
    {
        if (ingredients2.Length == 0)
            return 0;

        bool succeed = ingredients.Length == ingredients2.Length;
        // The minimum amount of ingredients is present
        int score = 1;
        for (int idx1 = 0; succeed && idx1 < ingredients.Length; ++idx1)
        {
            succeed = ContainsIngredient(ingredients2, ingredients[idx1]);
        }
        if (succeed) score+=2;

        // The proportions are right
        int amount = 0;
        float hackyRatio = 1.5f;
        for (int idx2 = 0; succeed && idx2 < ingredients2.Length; ++idx2)
        {
            int idx1 = FindIngredient(ingredients, ingredients2[idx2]);
            if (idx1 == -1)
            {
                succeed = amounts2[idx2] < 10;
            }
            else
            {
                //succeed = true;
                succeed = amounts2[idx2]* hackyRatio > this.amounts[idx1] * 0.3f && amounts2[idx2]* hackyRatio < this.amounts[idx1]*1.7f;
            }
            amount += amounts2[idx2];
        }
        if (succeed)
        {
            score++;
        }
        if(amount* hackyRatio > this.totalAmount*0.5f && amount* hackyRatio < this.totalAmount * 1.5f)
        {
            score++;
        }

        return score;
    }

    public static bool SameIngredients(LiquidParticle.LiquidType[] ingredients1, LiquidParticle.LiquidType[] ingredients2)
    {
        bool succeed = ingredients1.Length==ingredients2.Length;
        for (int idx1 = 0; succeed && idx1 < ingredients1.Length; ++idx1)
        {
            succeed =  ContainsIngredient(ingredients2, ingredients1[idx1]);
        }
        for (int idx2 = 0; succeed && idx2 < ingredients2.Length; ++idx2)
        {
            succeed = ContainsIngredient(ingredients1, ingredients2[idx2]);
        }
        return succeed;
    }
}
    
