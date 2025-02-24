using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageUI : MonoBehaviour
{
    void ResizeImage()
    {
        RectTransform rt = GetComponent<RectTransform>();
        float screenRatio = (float)Screen.width / Screen.height;
        float imageRatio = rt.sizeDelta.x / rt.sizeDelta.y;

        if (imageRatio > screenRatio)
        {
            // �ʺ� �������� ũ�� ����
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, rt.sizeDelta.x / screenRatio);
        }
        else
        {
            // ���̸� �������� ũ�� ����
            rt.sizeDelta = new Vector2(rt.sizeDelta.y * screenRatio, rt.sizeDelta.y);
        }
    }
}
