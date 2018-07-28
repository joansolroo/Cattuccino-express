using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimator : MonoBehaviour {

    [SerializeField] float speed = 1;
    [SerializeField] float scale = 1;

    [SerializeField] SpriteRenderer headRenderer;
    [SerializeField] Sprite[] heads;
    [SerializeField] bool[] flipHead;
    bool left = true;
    // Use this for initialization
    void Start()
    {
        headRenderer = GetComponent<SpriteRenderer>();
    }
    public int idx;
    // Update is called once per frame
    void LateUpdate()
    {
        idx = CoffeeReceiver.lastScore + 1;
        headRenderer.sprite = heads[idx];
        headRenderer.flipX = left == flipHead[idx];
    }
    	
	// Update is called once per frame
	void Update () {
        this.transform.localEulerAngles = new Vector3(0,0, Mathf.Sin(Playlist.totalTime*speed)*scale);
	}

    public void Left()
    {
        left = true;
    }
    public void Right()
    {
        left = false;
    }
}
