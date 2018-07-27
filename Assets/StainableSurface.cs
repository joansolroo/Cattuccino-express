using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainableSurface : MonoBehaviour
{/*
    [SerializeField] LayerMask layer;
    int particleLayer = 8;
    void OnCollisionEnter2D(Collision2D col)
    {
        Rigidbody2D otherRb = col.rigidbody;
        if(col.gameObject.layer != particleLayer)
        {
            return;
        }
        float r = Random.value;
        if (r < 0.4f)
        {
            otherRb.simulated = false;
            otherRb.GetComponent<Collider2D>().enabled = false;
            otherRb.transform.localScale = Vector3.Scale(otherRb.transform.localScale, new Vector3(Random.Range(1.5f, 3f), Random.Range(0.25f, 0.7f), 1));
            StartCoroutine(DestroyParticle(col.gameObject));
        }
        else if (r > .9f)
        {
            otherRb.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyParticle(col.gameObject));
        }

    }
    void OnCollisionStay2D(Collision2D col)
    {
        Rigidbody2D otherRb = col.rigidbody;
        if (col.gameObject.layer != particleLayer)
        {
            return;
        }
        float r = Random.value;
        if (r < 0.2f)
        {
            otherRb.simulated = false;
            otherRb.transform.localScale = Vector3.Scale(otherRb.transform.localScale, new Vector3(Random.Range(1.5f, 3f), Random.Range(0.05f, 0.25f), 1));
            StartCoroutine(DestroyParticle(col.gameObject));
        }
        else if (r > .8f)
        {
            otherRb.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(DestroyParticle(col.gameObject));
        }
    }

    IEnumerator DestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(2);
        float t = 5;
        while (t > 0)
        {
            if (particle != null)
            {
                particle.transform.localScale = particle.transform.localScale * 0.995f;
                yield return new WaitForEndOfFrame();
            }
            t -= Time.deltaTime;
        }
        Destroy(particle);
    }*/
}
