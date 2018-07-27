﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GlassSpawner : MonoBehaviour
{

    [SerializeField] Glass[] glasses;
    [SerializeField] int currentSelection;

    [SerializeField] Canvas labelCanvas;
    [SerializeField] ObjectFollowerUI labelPrefab;

    [SerializeField] int[] levels;
    [SerializeField] int[] drinksPerLevel;
    [SerializeField] float[] speeds;
    [SerializeField] int[] timeBetweenDrinks;
    [SerializeField] DrinkRecipe[] recipes;

    [SerializeField] UnityEngine.UI.Text text;

    AudioSource audiosource;
    int time;
    // Use this for initialization
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        time = -timeBetweenDrinks[level];

    }

    [SerializeField] int drinksServed = 0;
    [SerializeField] int level = 0;
    
    // Update is called once per frame
    void Update()
    {

        //timeCounter += Time.deltaTime;
        if (Playlist.beat)
        {
            time++;
        }
        if (level < levels.Length)
        {
            //if (timeCounter > timeBetweenDrinks[level])
            if(time>=timeBetweenDrinks[level])
            {
                time = 0;
                SpawnGlass();
                drinksServed++;
            }
            if (drinksServed > drinksPerLevel[level])
            {
                if (level < levels.Length)
                {
                    drinksServed = 0;
                    level++;
                    if (level < levels.Length)
                    {
                        time = -timeBetweenDrinks[level];
                    }
                }
                else
                {
                    Debug.Log("WIN!");
                }
            }

            if (level < levels.Length)
            {
                text.text = "HP: "; for (int hp = 0; hp < 5; ++hp) { text.text += CoffeeReceiver.HP > hp ? "I" : "-"; }
                text.text += " | LVL " + (level + 1);
                text.text += " | " + CoffeeReceiver.TOTALSCORE.ToString("00000") + " pts";
            }
            else
            {
                text.text = " WINNER! | " + CoffeeReceiver.TOTALSCORE.ToString("0000") + " pts";
            }
        }
    }

   // float timeCounter = 0;
    [SerializeField] float audioDelay = 1;
    public void SpawnGlass()
    {
            //timeCounter = 0;
            int recipeIndex = -1;
            if (recipeIndex == -1)
                recipeIndex = Random.Range(0, levels[level]);

            DrinkRecipe recipe = recipes[recipeIndex];

            Glass glass = Instantiate<Glass>(glasses[recipe.cupSize]);
            glass.recipe = recipe;

            glass.transform.position = this.transform.position;
            glass.GetComponent<move>().speed = speeds[level];
            ObjectFollowerUI ofui = Instantiate<ObjectFollowerUI>(labelPrefab);
            ofui.transform.parent = labelCanvas.transform;
            ofui.transform.position = labelPrefab.transform.position;
            ofui.text.text = recipe.drinkName.ToUpper();

            ofui.objectToFollow = glass.transform;

            glass.label = ofui.gameObject;
            audiosource.Play();
            audiosource.pitch = 1.2f - (0.2f * recipe.cupSize)+ Random.Range(-0.2f,0.2f);
            audiosource.time = audioDelay;
        
    }
}