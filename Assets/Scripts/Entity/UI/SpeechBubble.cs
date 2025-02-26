using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class SpeechBubble : MonoBehaviour
{
    private Transform mainCamera;
    private Coroutine speechCoroutine;

    [SerializeField] private TMP_Text speechText; // TextMeshPro UI ����
    [SerializeField] private string characterName; // ĳ���� �̸� ����
    [SerializeField] private float textChangeInterval = 1; // �ؽ�Ʈ ���� ����

    private int currentTextIndex = 0;
    private string[] currentSpeeches;

    private string[] teacherSpeeches =
    {
        "����...",
        "�׷��� ��ҿ�...",
        "����...",
        "������ �ض�.."
    };

    private string[] chillSpeeches =
{
        "����ϱ���",
        "�׷��׷�",
        "����",
        "�����"
    };

    private string[] sulSpeeches =
{
        "�����ؿ�!",
        "������!",
        "����ѵ���!",
    };

    private string[] satoSpeeches =
{
        "���ϱ�!",
        "������",
        "�����",
    };

    private string[] rockSpeeches =
{
        "����\n�մϴٿ�~",
        "����~",
        "����ؿ�~",
    };



    private void Start()
    {
        mainCamera = Camera.main.transform;

        SelectSpeechByCharacter();

        if (speechText != null && currentSpeeches.Length > 0)
        {
            if (speechCoroutine != null)
                StopCoroutine(speechCoroutine);

            speechCoroutine = StartCoroutine(ChangeSpeech());
        }
    }


    private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }

    private void SelectSpeechByCharacter()
    {
        switch (characterName)
        {
            case "Teacher":
                currentSpeeches = teacherSpeeches;
                break;
            case "Chill":
                currentSpeeches = chillSpeeches;
                break;
            case "Sul":
                currentSpeeches = sulSpeeches;
                break;
            case "Sato":
                currentSpeeches = satoSpeeches;
                break;
            case "Rock":
                currentSpeeches = rockSpeeches;
                break;
            default:
                currentSpeeches = new string[] { "..." }; // �⺻�� (����ִ� ��ǳ�� ����)
                break;
        }
    }

    private IEnumerator ChangeSpeech()
    {
        while (true)
        {
            speechText.text = currentSpeeches[currentTextIndex]; // ���� �ؽ�Ʈ ����
            currentTextIndex = (currentTextIndex + 1) % currentSpeeches.Length; // ���� �ؽ�Ʈ �ε����� �̵�

            yield return new WaitForSeconds(textChangeInterval); // ���� �ð� ���
        }
    }
}
