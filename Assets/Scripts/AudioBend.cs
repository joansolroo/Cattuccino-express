using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioBend : MonoBehaviour {

    AudioSource source;
    [Range(0, 2)] public float targetPitch = 1;
   
    [Range(0, 10)] public float speed = 1;
    [Range(0, 1)] public float noise = 1;

    [Range(0, 2)] public float currentPitch = 1;
    float unbendPitch;

    public static AudioBend instance;
    private void Awake()
    {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        unbendPitch = source.pitch;
        currentPitch = source.pitch;
    }

    bool paused = false;
    public bool applyPitch = false;
	// Update is called once per frame
	void Update () {
        unbendPitch = Mathf.MoveTowards(unbendPitch, targetPitch, Time.unscaledDeltaTime * speed * Mathf.Abs(unbendPitch - targetPitch));
        currentPitch = Mathf.MoveTowards(currentPitch, unbendPitch, 1);
        currentPitch += Random.Range(-noise, noise)*Time.unscaledDeltaTime;
        currentPitch = Mathf.Min(1.5f, Mathf.Max(0.2f, currentPitch));

        if(applyPitch)
            source.pitch = currentPitch;
        if (paused)
        {
           // Time.timeScale = currentPitch/4;
        }
        else
        {
           // Time.timeScale = currentPitch;
        }
	}

    float lastPitch = 1;
    public static void Pause() {
        if (!instance.paused)
        {
            instance.lastPitch = instance.currentPitch;
            instance.targetPitch = 0.2f;
            instance.paused = true;
        }
    }
    public static void Play()
    {
        if (instance.paused)
        {
            instance.targetPitch = instance.lastPitch;
            instance.paused = false;
        }
    }
    public static void GoToPitch(float pitch,float speed = -1)
    {
        if(speed > 0)
        {
            instance.speed = speed;
        }
        instance.targetPitch = pitch;
    }
}
