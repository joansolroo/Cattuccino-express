﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Popup : MonoBehaviour
{
    [SerializeField] float transitionTime = 0.25f;
    [SerializeField] Vector3 scaleDimensions= Vector3.one;

    [SerializeField] bool show;
    [SerializeField] UnityEngine.UI.Button button;
    private void Start()
    {
        if (!show) HideImmediately();
    }
    private void Update()
    {
        if (show)
        {
            show = false;
            Show();
        }
    }
    public void Show() {
        StartCoroutine(DoShow());
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject, new BaseEventData(EventSystem.current));
        }
    }
    public void Hide()
    {
        StartCoroutine(DoHide());
    }
    public void HideImmediately()
    {
        this.transform.localScale = scaleDimensions;
    }
    bool hidding = false;
    IEnumerator DoHide()
    {
        if (!hidding)
        {
            hidding = true;
            float t = 0;
            while (t < transitionTime)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, scaleDimensions, Time.deltaTime / transitionTime);
                yield return new WaitForEndOfFrame();
                t += Time.unscaledDeltaTime;
            }
            HideImmediately();
            hidding = false;
        }
    }
    bool showing = false;
    IEnumerator DoShow()
    {
        if (!showing)
        {
            showing = true;
            float t = 0;
            while (t < transitionTime)
            {
                this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.one, Time.deltaTime / transitionTime);
                yield return new WaitForEndOfFrame();
                t += Time.unscaledDeltaTime;
            }
            this.transform.localScale = Vector3.one;
            showing = false;
        }
    }
}
