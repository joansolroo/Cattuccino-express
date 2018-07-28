using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticle : MonoBehaviour
{

    public enum LiquidType
    {
        Other, Coffee, Chocolate, Milk, Foam, Water, Tomato
    }
    public LiquidType liquidType;

    Rigidbody2D rb2D;
    public Rigidbody2D rb2DParent = null;
    static bool TurnStatic = true;

    public Transform currentContainer
    {
        get
        {
            return transform.parent;
        }
        set
        {
            transform.parent = value;
        }
    }
    float minSpeed = 0.1f;
    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    [SerializeField] float velocity;
    float t = 0;
    // Update is called once per frame
    void Update()
    {
        if (rb2D != null)
        {

            velocity = rb2DParent != null ? Mathf.Abs(rb2D.velocity.magnitude - rb2DParent.velocity.magnitude) : rb2D.velocity.magnitude;
            if (t > 1 && velocity < minSpeed)
            {

                if (currentContainer == null)
                {
                    GetComponent<Collider2D>().enabled = false;
                    StartCoroutine(DestroyParticle(gameObject, 2f, 2f));
                }
                else
                {
                    if (TurnStatic)
                    {
                        StartCoroutine(DephysicsParticle(gameObject, 5f));
                    }
                    //transform.parent = currentContainer;
                }
            }
            if (velocity > 25)
            {
                StartCoroutine(DestroyParticle(gameObject, 0.1f, 0.1f));
            }
        }
        t += Time.deltaTime;
        if (t > 3 && rb2DParent == null)
        {
            StartCoroutine(DestroyParticle(gameObject, 0.1f, 0.1f));
        }
    }

    int wallLayer = 9;
    void OnCollisionEnter2D(Collision2D col)
    {
        //Rigidbody2D otherRb = col.rigidbody;
        if (rb2D == null || col.gameObject.layer != wallLayer)
        {
            return;
        }
        float r = Random.value;
        if (r < 0.4f)
        {
            rb2D.simulated = false;
            rb2D.GetComponent<Collider2D>().enabled = false;
            rb2D.transform.localScale = Vector3.Scale(rb2D.transform.localScale, new Vector3(Random.Range(1.5f, 3f), Random.Range(0.25f, 0.7f), 1));
            StartCoroutine(DestroyParticle(gameObject, 2, 5));
        }
        else if (r > .9f)
        {
            rb2D.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyParticle(gameObject, 2, 5));
        }
        else
        {
            StartCoroutine(DephysicsParticle(gameObject, 1f, true));
        }
    }

    bool destroying = false;
    IEnumerator DestroyParticle(GameObject particle, float waitTime, float fadeTime)
    {
        if (!destroying)
        {
            destroying = true;
            yield return new WaitForSeconds(waitTime);
            float t = fadeTime;
            while (t > 0)
            {
                particle.transform.localScale = particle.transform.localScale * 0.995f;
                yield return new WaitForEndOfFrame();
                t -= Time.deltaTime;
            }
            Destroy(particle);
        }
    }
    bool detaching = false;
    IEnumerator DephysicsParticle(GameObject particle, float waitTime, bool disableCollider = false)
    {
        if (!detaching)
        {
            detaching = true;
            yield return new WaitForSeconds(waitTime);
            if (!destroying)
            {
                
                Collider2D c = gameObject.GetComponent<Collider2D>();
                if (c != null)
                {

                    rb2D.simulated = false;
                    //Destroy(rb2D);
                    rb2D = null;
                    gameObject.layer = 11;
                    c.enabled = !disableCollider;
                }
                
            }
        }
    }

}
