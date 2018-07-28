using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour {

    [SerializeField] float speed = 1;
    [SerializeField] float scale = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.localEulerAngles = new Vector3(0,0, Mathf.Sin(Playlist.totalTime*speed)*scale);
	}
}
