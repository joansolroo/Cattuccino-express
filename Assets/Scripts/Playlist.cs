using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioBend))]
[RequireComponent(typeof(AudioSource))]
public class Playlist : MonoBehaviour {

    [SerializeField] AudioClip[] songs;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource buttonSource;

    int currentSong = 0;

    public static Playlist instance;
    AudioBend bend;
    [SerializeField] float bpm = 125;
    static float time = -1;
    public static float totalTime = 0;
    public static bool beat = false;
    public static int beats = 0;
    public float delay = 0.1f;
    // Use this for initialization
    void Start () {
        instance = this;
        source = GetComponent<AudioSource>();
        bend = GetComponent<AudioBend>();
        
        //time = Time.time;
        //time = +delay;
    }
	
	// Update is called once per frame
	void Update () {
        if (time == -1)
        {
            time = delay;
            source.clip = songs[currentSong];
            source.Play();
        }
        else {
            float deltaTime = (Time.deltaTime) * bpm * bend.currentPitch / 60;
            totalTime += deltaTime;

            int lastTime = (int)time;
            
            time += deltaTime;
            
            int currentTime = (int)time;
            if (currentTime > lastTime)
            {
                beat = true;
                time -= currentTime;
                beats++;
            }
            else
            {
                beat = false;
            }
        }
        if (source.time == 1)
        {
            NextSong();
        }

    }
    public void NextSong()
    {
        buttonSource.Play();

        currentSong = (++currentSong) % songs.Length;
        source.clip = songs[currentSong];
        source.Play();
    }
}
