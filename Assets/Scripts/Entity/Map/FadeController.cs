using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public GameObject fadePanel;//페이드 효과를 위한 이미지
    public GameObject backGroundImage;
    public Sprite backGroundSprite;//바뀔 이미지

    private void Start()
    {
        if (fadePanel == null)//오류처리
        {
            Debug.LogError("FadePanel is null");
            return;
        }
    }

    public void OnFadaOutandIn()//페이드 아웃과 페이드 인 효과
    {
        StartCoroutine(CoOnFadaOutandIn());
    }

    IEnumerator CoOnFadaOutandIn()
    {
        float currentTime = 0;
        float fadeTime = 1f;

        fadePanel.SetActive(true);

        while (currentTime <= fadeTime)//페이드 아웃
        {
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(0f, 1f, currentTime / fadeTime));
            currentTime += Time.deltaTime;
            yield return null;
        }

        currentTime = 0;
        SpriteRenderer[] sr = backGroundImage.GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < sr.Length; i++)//베경이미지 변경
        {
            sr[i].sprite = backGroundSprite;
            sr[i].transform.position = new Vector3 (backGroundSprite.bounds.size.x * 2.5f * i, 0.45f, 0f);
        }

        while (currentTime <= fadeTime)//페이드 인
        {
            fadePanel.GetComponent<CanvasRenderer>().SetAlpha(Mathf.Lerp(1f, 0f, currentTime / fadeTime));
            currentTime += Time.deltaTime;
            yield return null;
        }

        fadePanel.SetActive(false);
        Debug.Log("OnFadaOutandIn 완료");
    }
}


