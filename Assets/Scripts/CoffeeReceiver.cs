using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CoffeeReceiver : MonoBehaviour
{
    [SerializeField] AudioSource soundAccept;
    [SerializeField] AudioSource soundReject;

    [SerializeField] Popup info;
    Glass _glass;

    [SerializeField] Image[] starsOn;
    [SerializeField] Image[] starsOff;

    public static int TOTALSCORE = 0;
    public static int HP = 5;

    public static int lastScore = -1;

    
    private void Start()
    {
        info.HideImmediately();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent == null)
            return;

        Glass glass = collision.transform.parent.GetComponent<Rigidbody2D>().GetComponent<Glass>();
        
        if (glass != null && !glass.taken)
        {
            glass.taken = true;
            _glass = glass;
            Debug.Log(this.name + " touching " + glass.gameObject.name);

            glass.container.RecomputeIngredients();
            var ingredients = glass.container.ingredients;
            var amounts = glass.container.amounts;
            int total = 0;
            foreach (int a in amounts)
            {
                total += a;
            }
            /* 
             text.text = "";
             for (int idx = 0; idx < ingredients.Length; ++idx)
             {
                 text.text += ingredients[idx].ToString() + ": " + ((float)amounts[idx]).ToString("0.0")+"\n";
             }*/

            int score = glass.recipe.ScoredRecipe(ingredients,amounts);
            lastScore = score;
            if (score>2)
            {
                soundAccept.Play();
                if(HP<5)HP++;
            }
            else
            {
                soundReject.Play();
                if (HP > 0)
                {
                    HP--;
                }
                else
                {
                    Debug.Log("LOST!");
                }
            }
            for(int idx = 0; idx < 5; ++idx)
            {
                bool reached = idx < score;
                starsOn[idx].enabled = reached;
                starsOff[idx].enabled = !reached;
            }
            //for (int s = 0; s < score; ++s) text.text += "|";
            //for( int s = score; s < 5;++s) text.text += "★";

            glass.GetComponent<move>().enabled = false;
            StartCoroutine(DestroyDrink(glass.gameObject, 2, 0.02f));
            TOTALSCORE += score * 100;
            info.Show();
        }
    }

    IEnumerator DestroyDrink(GameObject drink, float waitTime, float fadeTime)
    {
        yield return new WaitForSeconds(waitTime);
        float t = fadeTime;
        while (t > 0)
        {
            drink.transform.localScale = drink.transform.localScale * 0.75f;
            yield return new WaitForEndOfFrame();
            t -= Time.deltaTime;
        }
        Destroy(drink);
        info.Hide();
    }

    
}
