using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class StudySkill : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject roll; // 두루마기 이미지
    [SerializeField] private GameObject background; // 두루마기 이미지

    [SerializeField] private TMP_Text questionText; // OX 퀴즈 텍스트

    [SerializeField] private Button oButton;  
    [SerializeField] private Button xButton; 

    private KeyValuePair<string, bool> currentQuestion;
    public bool isQuizActive = false;
    private bool isAnswerSelected = false;
    private int maxQuestions = 5;
    private int currentQuestionCount = 0;

    public float quizDuration = 10f; // 퀴즈 지속 시간
    private float questionWaitTime = 0.1f; // 문제를 푼 후 다음 문제까지 대기 시간

    Dictionary<string, bool> oxQuizzes = new Dictionary<string, bool>
        {
            { "과거 시험은 매년 열렸다.", false },
            { "과거 시험의 최종 관문을 전시라고 했다.", true },
            { "무과 시험도 있었다.", true },
            { "과거 시험은 누구나 응시 가능했다.", false },
            { "과거 급제 후 바로 벼슬했다.", true },
            { "서당은 과거 시험 준비 학교다.", true },
            { "문과 시험에서는 시를 짓는 문제도 출제되었다.", true },
            { "과거 시험은 한양에서만 열렸다.", false },
            { "잡과에서는 의학도 시험 봤다.", true },
            { "과거 시험 답안은 연필로 썼다.", false },
            { "문과 시험에서는 한문이 아닌 한글로 답안을 작성했다. ", false }

        };


    private void Start()
    {
        roll.SetActive(false);
        background.SetActive(false);

        UIManager.Instance.OnOXSelected += CheckAnswer;
    }

    private void Update()
    {
        if (!isQuizActive) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CheckAnswer(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CheckAnswer(false);
        }
    }

    public IEnumerator StartQuiz(float duration)
    {
        Debug.Log("StartQuiz");
        if (isQuizActive)
        {
            Debug.Log("StartQuiz 실행 취소됨 (이미 진행 중)");
            yield break;
        }

        Debug.Log("StartQuiz_실행");

        isQuizActive = true;
        quizDuration = duration;
        currentQuestionCount = 0;

        roll.SetActive(true);
        background.SetActive(true);

        UIManager.Instance.SetQuizMode(true);
        SelectRandomQuestion(); // 첫 번째 문제 즉시 표시

        yield return StartCoroutine(QuizRoutine());


        EndQuiz();
    }


    private IEnumerator QuizRoutine()
    {
        float startTime = Time.time;
        Debug.Log("퀴즈 루틴");

        while (currentQuestionCount < maxQuestions && (Time.time - startTime) < quizDuration)
        {
            Debug.Log("퀴즈 루틴 실행중");

            isAnswerSelected = false;
            roll.SetActive(true);
            background.SetActive(true);


            SelectRandomQuestion(); // 퀴즈 선택 후 UI 업데이트

            // 정답을 선택할 때까지 대기
            while (!isAnswerSelected && (Time.time - startTime) < quizDuration)   // 이거 대기 쳐 해버리니까  안넘어가잖아ㅛ -> 고침
            {
                yield return null;
            }

            if ((Time.time - startTime) >= quizDuration)
            {
                Debug.Log("시간 끝났어 꺼져");
                break;
            }


            yield return new WaitForSeconds(questionWaitTime); // 다음 문제 전 대기

            currentQuestionCount++;

            if (currentQuestionCount < maxQuestions)
            {
                SelectRandomQuestion(); // 다음 문제 선택
            }
        }
    }


    private void SelectRandomQuestion()
    {
        int index = Random.Range(0, oxQuizzes.Count);
        currentQuestion = new KeyValuePair<string, bool>(
            new List<string>(oxQuizzes.Keys)[index],
            new List<bool>(oxQuizzes.Values)[index]
        );

        questionText.text = currentQuestion.Key;
    }


    private void CheckAnswer(bool playerAnswer)
    {
        if (isAnswerSelected) return;
        isAnswerSelected = true;

        if (playerAnswer == currentQuestion.Value)
        {
            Debug.Log("정답!");
          //  PlayManager.Instance.AddScore(100);
        }
        else
        {
            Debug.Log("오답!");
        }
    }



    private void EndQuiz()
    {
        isQuizActive = false;
        roll.SetActive(false);
        background.SetActive(false);

        UIManager.Instance.SetQuizMode(false); // UI 복구
        Debug.Log("EndQuiz - OX 퀴즈 종료!");
    }

}
