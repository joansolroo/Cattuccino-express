using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioBend))]
[RequireComponent(typeof(AudioSource))]
public class Playlist : MonoBehaviour {

    //[SerializeField] AudioClip[] songs;
    [SerializeField] AudioSource source;
    [SerializeField] AudioSource buttonSource;

    //[SerializeField]int currentSong = 0;

    public static Playlist instance;
    AudioBend bend;
    [SerializeField] float bpm = 125;
    static float time = -1;
    public static float totalTime = 0;
    public static bool beat = false;
    public static int beats = 0;
    public float delay = 0.1f;

    public AudioSource[] channels;
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
        /*if (time == -1)
        {
            time = delay;
            source.clip = songs[currentSong];
            source.Play();
        }
        else */{
            if (RecipeHelper.beforeTutorial || RecipeHelper.isTutorial)
            {
                for(int c = 0; c< channels.Length; ++c)
                {
                    if(c == 0)
                    {
                        channels[c].volume = 1;
                    }
                    else
                    {
                        channels[c].volume = 0;
                    }
                }
            }
            else
            {
                float deltaTime = (Time.deltaTime) * bpm * bend.currentPitch / 60;
                totalTime += deltaTime;

                int lastTime = (int)time;

                time += deltaTime;

                int currentTime = (int)time;
                if (currentTime > lastTime)
                {
                    beat = true;
                    time -= Mathf.Floor(time);
                    beats++;
                }
                else
                {
                    beat = false;
                }

                if(beats < 4*8)
                {
                    for (int c = 0; c < channels.Length; ++c)
                    {
                        
                        if (c <3)
                        {
                            channels[c].volume = Mathf.MoveTowards(channels[c].volume,1,Time.deltaTime*bpm/60);
                        }
                        else
                        {
                            channels[c].volume = 0;
                        }
                    }
                    //channels[1].volume = Mathf.MoveTowards(channels[1].volume, 1, Time.deltaTime * bpm / 60);
                    channels[2].volume = bend.currentPitch;
                }
                else if (beats < 8*16)
                {
                    for (int c = 0; c < channels.Length; ++c)
                    {
                        if (c < 5)
                        {
                            channels[c].volume = Mathf.MoveTowards(channels[c].volume, 1, Time.deltaTime * bpm / 60);
                        }
                        else
                        {
                            channels[c].volume = 0;
                        }
                    }
                    channels[2].volume = bend.currentPitch;
                    channels[4].volume = channels[4].volume/2;
                }
                else if (beats > 8 * 16)
                {
                    for (int c = 0; c < channels.Length; ++c)
                    {
                        
                        {
                            channels[c].volume = Mathf.MoveTowards(channels[c].volume, 1, Time.deltaTime * bpm / 60);
                        }
                        
                    }
                    channels[2].volume = bend.currentPitch;
                    channels[5].volume = bend.currentPitch;
                }
                
                
                channels[0].volume = Mathf.MoveTowards(channels[0].volume, 1, Time.deltaTime * bpm / 60); //TODO unnefficient, fix
            }
        }
        if (source.time == 1)
        {
            //NextSong();
        }

    }
    /*
    public void NextSong()
    {
        buttonSource.Play();

        currentSong = (++currentSong) % songs.Length;
        source.clip = songs[currentSong];
        source.PlayDelayed(0.75f);
    }*/

    public void Play()
    {
        buttonSource.Play();

        //currentSong = (++currentSong) % songs.Length;
        //source.clip = songs[currentSong];
        StartCoroutine(DoPlay());
        
    }
    public void End()
    {
        buttonSource.Play();
        StartCoroutine(DoEnd());
    }
    IEnumerator DoEnd()
    {
        while (channels[0].pitch > 0)
        {
            foreach (AudioSource channel in channels)
            {
                channel.pitch = Mathf.MoveTowards(channel.pitch, 0, Time.unscaledDeltaTime/0.25f);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator DoPlay()
    {
        yield return new WaitForSeconds(0.75f);

        foreach (AudioSource channel in channels)
        {
            channel.Play();
            channel.volume = 0;
        }
        foreach (AudioSource channel in channels)
        {
            channel.pitch = 0;
        }
        while (channels[0].pitch <1)
        {
            foreach (AudioSource channel in channels)
            {
                channel.pitch = Mathf.MoveTowards(channel.pitch, 1, Time.unscaledDeltaTime / 0.25f);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
