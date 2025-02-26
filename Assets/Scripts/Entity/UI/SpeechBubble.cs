using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class SpeechBubble : MonoBehaviour
{
    private Transform mainCamera;
    private Coroutine speechCoroutine;

    [SerializeField] private TMP_Text speechText; // TextMeshPro UI 참조
    [SerializeField] private string characterName; // 캐릭터 이름 설정
    [SerializeField] private float textChangeInterval = 1; // 텍스트 변경 간격

    private int currentTextIndex = 0;
    private string[] currentSpeeches;

    private string[] teacherSpeeches =
    {
        "어휴...",
        "그러게 평소에...",
        "에휴...",
        "공부좀 해라.."
    };

    private string[] chillSpeeches =
{
        "대단하구만",
        "그래그래",
        "오오",
        "대단해"
    };

    private string[] sulSpeeches =
{
        "축하해요!",
        "멋져요!",
        "대단한데요!",
    };

    private string[] satoSpeeches =
{
        "장하군!",
        "멋지군",
        "대단해",
    };

    private string[] rockSpeeches =
{
        "축하\n합니다요~",
        "오오~",
        "대단해요~",
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
                currentSpeeches = new string[] { "..." }; // 기본값 (비어있는 말풍선 방지)
                break;
        }
    }

    private IEnumerator ChangeSpeech()
    {
        while (true)
        {
            speechText.text = currentSpeeches[currentTextIndex]; // 현재 텍스트 변경
            currentTextIndex = (currentTextIndex + 1) % currentSpeeches.Length; // 다음 텍스트 인덱스로 이동

            yield return new WaitForSeconds(textChangeInterval); // 일정 시간 대기
        }
    }
}
