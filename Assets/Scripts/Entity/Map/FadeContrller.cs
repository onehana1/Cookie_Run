using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeContrller : MonoBehaviour
{
    public GameObject fadePanel;
    public GameObject backGroundImage;
    public Sprite backGroundSprite;

    private void Start()
    {
        if(fadePanel == null)
        {
            Debug.LogError("FadePanel is null");
            return;
        }
    }
    public void OnFadaOutandIn()
    {
        StartCoroutine(CoOnFadaOutandIn());
    }
    IEnumerator CoOnFadaOutandIn()
    {
        float currentTime = 0;
        float fadeTime = 1f;

        fadePanel.SetActive(true);
        while (currentTime <= fadeTime)
        {
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, currentTime / fadeTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        currentTime = 0;
        SpriteRenderer[] sr = backGroundImage.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].sprite = backGroundSprite;
            sr[i].transform.position = new Vector3 (backGroundSprite.bounds.size.x * 2.5f * i, 0.45f, 0f);
        }
        while (currentTime <= fadeTime)
        {
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, currentTime / fadeTime));
            currentTime += Time.deltaTime;
            yield return null;
        }
        fadePanel.SetActive(false);
        Debug.Log("OnFadaOutandIn ¿Ï·á");

        yield break;
    }
}


