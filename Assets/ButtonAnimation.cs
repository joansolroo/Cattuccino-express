using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour {

    [SerializeField] GameObject pivot;
    [SerializeField] float animationTime = 0.25f;
    [SerializeField] Vector3 minSize = Vector3.one;
    Vector3 maxSize = Vector3.one;

    private void Start()
    {
        maxSize = transform.localScale;
    }
    public void ButtonClick()
    {
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        float t = 0;
        while (t < animationTime)
        {
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, minSize, (this.transform.localScale - minSize).magnitude * Time.deltaTime / animationTime);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }
         t = 0;
        while (t < animationTime)
        {
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, maxSize, (this.transform.localScale - maxSize).magnitude * Time.deltaTime / animationTime);
            yield return new WaitForEndOfFrame();
            t += Time.deltaTime;
        }
    }
}
