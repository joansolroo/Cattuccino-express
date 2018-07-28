using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Popup))]
public class RecipeHelper : MonoBehaviour {

    static RecipeHelper instance;
    Popup popup;

    [SerializeField] Text drinkName;
    [SerializeField] Text recipe;

    [SerializeField] Button button;

    [SerializeField] Popup[] tutorial;
    // Use this for initialization
    void Awake () {
        instance = this;
	}
    private void Start()
    {
        popup = GetComponent<Popup>();
        popup.HideImmediately();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //popup.Hide();
           // HideRecipe();
        } 
    }

    public static bool isTutorial = false;
    public static bool beforeTutorial = true;
    int tutorialStep = 0;
    DrinkRecipe lastRecipe = null;
    public static void ShowRecipe(DrinkRecipe _recipe)
    {
        if (_recipe != null)
        {
            if (instance.lastRecipe == null)
            {
                beforeTutorial = false;
                isTutorial = true; 
                instance.tutorialStep = 0;
                instance.lastRecipe = _recipe;
                instance.NextHelp();
            }
            else
            {
                instance.popup.Show();
                instance.drinkName.text = _recipe.drinkName;
                string r = "Recipe:\n";
                for (int i = 0; i < _recipe.ingredients.Length; ++i)
                {
                    r += "" + (int)(_recipe.amounts[i] / 50) + "x " + _recipe.ingredients[i].ToString();
                    if (i < _recipe.ingredients.Length - 1) r += "\n";
                }
                instance.recipe.text = r;
               
                AudioBend.Pause();
                instance.lastRecipe = _recipe;
            }
        }
        
    }
    [SerializeField] MiniRecipe minirecipe;
    [SerializeField] MiniRecipe minirecipe2;
    public void HideRecipe()
    {
        if (isTutorial)
        {
            //Playlist.instance.NextSong();
            isTutorial = false;

        }
        if (lastRecipe != null)
        {
            MiniRecipe templateMR;
            MiniRecipe instanceMR; 
            if (MiniRecipe.minirecipes < 11)
            {
                templateMR = this.minirecipe;
            }
            else
            {
                templateMR = this.minirecipe2;
            }
            instanceMR = (MiniRecipe)Instantiate(templateMR);
        
            instanceMR.GetComponent<RectTransform>().parent = templateMR.GetComponent<RectTransform>().parent;
            instanceMR.transform.localPosition = templateMR.transform.localPosition;
            instanceMR.transform.localPosition = instanceMR.transform.localPosition + new Vector3(0, -5 * (MiniRecipe.minirecipes%12), 0);
            instanceMR.Show(lastRecipe);
        }
        AudioBend.Play();
        EventSystem.current.SetSelectedGameObject(null, new BaseEventData(EventSystem.current));
    }

    public void NextHelp()
    {
        if(tutorialStep == 0)
        {
            AudioBend.Pause();
        }
        if (tutorialStep < tutorial.Length)
        {
            tutorial[tutorialStep].Show();
            ++tutorialStep;
        }
        else if(lastRecipe!=null)
        {
            ShowRecipe(lastRecipe);
        }
       
    }
}
