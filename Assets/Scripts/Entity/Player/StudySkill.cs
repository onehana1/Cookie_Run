using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class StudySkill : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject roll; // �η縶�� �̹���
    [SerializeField] private GameObject background; // �η縶�� �̹���

    [SerializeField] private TMP_Text questionText; // OX ���� �ؽ�Ʈ

    [SerializeField] private Button oButton;  
    [SerializeField] private Button xButton; 

    private KeyValuePair<string, bool> currentQuestion;
    public bool isQuizActive = false;
    private bool isAnswerSelected = false;
    private int maxQuestions = 5;
    private int currentQuestionCount = 0;

    public float quizDuration = 10f; // ���� ���� �ð�
    private float questionWaitTime = 0.1f; // ������ Ǭ �� ���� �������� ��� �ð�

    Dictionary<string, bool> oxQuizzes = new Dictionary<string, bool>
        {
            { "���� ������ �ų� ���ȴ�.", false },
            { "���� ������ ���� ������ ���ö�� �ߴ�.", true },
            { "���� ���赵 �־���.", true },
            { "���� ������ ������ ���� �����ߴ�.", false },
            { "���� ���� �� �ٷ� �����ߴ�.", true },
            { "������ ���� ���� �غ� �б���.", true },
            { "���� ���迡���� �ø� ���� ������ �����Ǿ���.", true },
            { "���� ������ �Ѿ翡���� ���ȴ�.", false },
            { "��������� ���е� ���� �ô�.", true },
            { "���� ���� ����� ���ʷ� ���.", false },
            { "���� ���迡���� �ѹ��� �ƴ� �ѱ۷� ����� �ۼ��ߴ�. ", false }

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
            Debug.Log("StartQuiz ���� ��ҵ� (�̹� ���� ��)");
            yield break;
        }

        Debug.Log("StartQuiz_����");

        isQuizActive = true;
        quizDuration = duration;
        currentQuestionCount = 0;

        roll.SetActive(true);
        background.SetActive(true);

        UIManager.Instance.SetQuizMode(true);
        SelectRandomQuestion(); // ù ��° ���� ��� ǥ��

        yield return StartCoroutine(QuizRoutine());


        EndQuiz();
    }


    private IEnumerator QuizRoutine()
    {
        float startTime = Time.time;
        Debug.Log("���� ��ƾ");

        while (currentQuestionCount < maxQuestions && (Time.time - startTime) < quizDuration)
        {
            Debug.Log("���� ��ƾ ������");

            isAnswerSelected = false;
            roll.SetActive(true);
            background.SetActive(true);


            SelectRandomQuestion(); // ���� ���� �� UI ������Ʈ

            // ������ ������ ������ ���
            while (!isAnswerSelected && (Time.time - startTime) < quizDuration)   // �̰� ��� �� �ع����ϱ�  �ȳѾ�ݾƤ� -> ��ħ
            {
                yield return null;
            }

            if ((Time.time - startTime) >= quizDuration)
            {
                Debug.Log("�ð� ������ ����");
                break;
            }


            yield return new WaitForSeconds(questionWaitTime); // ���� ���� �� ���

            currentQuestionCount++;

            if (currentQuestionCount < maxQuestions)
            {
                SelectRandomQuestion(); // ���� ���� ����
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
            Debug.Log("����!");
          //  PlayManager.Instance.AddScore(100);
        }
        else
        {
            Debug.Log("����!");
        }
    }



    private void EndQuiz()
    {
        isQuizActive = false;
        roll.SetActive(false);
        background.SetActive(false);

        UIManager.Instance.SetQuizMode(false); // UI ����
        Debug.Log("EndQuiz - OX ���� ����!");
    }

}
