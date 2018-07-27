using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class move : MonoBehaviour {

    [SerializeField] public float speed= 1;
    Rigidbody2D rb2d;
    [SerializeField] bool autoMove = true;
	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}

    float t = 0;
	// Update is called once per frame
	void Update () {

        if (t > 0.25f)
        {
            rb2d.velocity = new Vector2(speed * (autoMove ? 1 : Input.GetAxis("Horizontal")), 0);
        }
        else{ t += Time.deltaTime; }
	}
}
