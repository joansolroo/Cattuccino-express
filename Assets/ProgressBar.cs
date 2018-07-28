using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ProgressBar : MonoBehaviour {

    [SerializeField] int value = 0;
    [SerializeField] int MaxValue = 25;
    [SerializeField] float speed = 3;
    [SerializeField] Text progress;

    [SerializeField] GameObject game;
    [SerializeField] GameObject intro;

    [SerializeField] GameObject[] info;
    [SerializeField] GameObject screen;

    [SerializeField] AudioSource introSong;
    // Use this for initialization
    void Start() {
        
            foreach (GameObject g in info) g.SetActive(false);
    }

    float t = 0;
    bool showing = true;
    // Update is called once per frame
    void Update() {
        if (showing)
        {
            t += Time.deltaTime * speed;
            value = (int)t;
            string text = "";
            for (int i = 0; i < MaxValue; ++i)
            {
                if (i < value) text += "|"; else text += ".";
            }

            introSong.volume = ((float)value) / MaxValue;
            progress.text = text;

            if (value > 7)
            {
                foreach (GameObject g in info) g.SetActive(true);
            }
            if (Input.anyKey)
            {

                StartCoroutine(LaunchGame());
                showing = false;
            }
        }
	}
    IEnumerator LaunchGame()
    {
        introSong.volume = 1;
        screen.SetActive(false);
        introSong.Pause();
        Playlist.instance.Play();
        yield return new WaitForSeconds(2f);
        game.SetActive(true);
        intro.SetActive(false);
    }
}
