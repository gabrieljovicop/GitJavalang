using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TriviaManager : MonoBehaviour
{
    public GameObject[] questions;
    public GameObject MakeSureBack;
    public GameObject BackButton;
    public GameObject StartPractice;
    private int currentQuestionIndex = 0;
    private int soalTotal = 0;
    private int answeredCorrectly = 0;
    private bool isAnswering = false;
    public TextMeshProUGUI QuestionWithCorrectAnswer;

    void Start()
    {
        StartCoroutine(InitialObject());
    }

    IEnumerator InitialObject()
    {
        StartPractice.SetActive(true);
        yield return new WaitForSeconds(2f);
        StartPractice.SetActive(false);
        BackButton.SetActive(true);
        ActivateQuestion(currentQuestionIndex);
    }

    void ActivateQuestion(int index)
    {
        foreach (GameObject question in questions)
        {
            question.SetActive(false);
        }
        questions[index].SetActive(true);
        
        if (currentQuestionIndex == questions.Length - 1)
        {
            BackButton.SetActive(false);
            QuestionWithCorrectAnswer.text = "Correct: " + answeredCorrectly + "/" + soalTotal;
        }
        else
        {
            BackButton.SetActive(true);
        }
    }

    public void TrueScreen()
    {
        if (!isAnswering)
        {
            isAnswering = true;
            GameObject currentQuestion = questions[currentQuestionIndex];
            GameObject trueInfo = currentQuestion.GetComponent<QuestionInfo>().TrueInfo;
            trueInfo.SetActive(true);
            answeredCorrectly++;
            DisableOtherButtons(trueInfo);
        }
    }

    public void FalseScreen()
    {
        if (!isAnswering)
        {
            isAnswering = true;
            GameObject currentQuestion = questions[currentQuestionIndex];
            GameObject falseInfo = currentQuestion.GetComponent<QuestionInfo>().FalseInfo;
            falseInfo.SetActive(true);
            DisableOtherButtons(falseInfo);
        }
    }

    void DisableOtherButtons(GameObject activePanel)
    {
        foreach (GameObject question in questions)
        {
            if (question != activePanel.transform.parent.gameObject)
            {
                Button[] buttons = question.GetComponentsInChildren<Button>();
                foreach (Button button in buttons)
                {
                    button.interactable = false;
                }
            }
        }
    }

    public void NextSoal()
    {
        GameObject currentQuestion = questions[currentQuestionIndex];
        QuestionInfo questionInfo = currentQuestion.GetComponent<QuestionInfo>();
        if (questionInfo != null && questionInfo.TrueInfo != null)
        {
            questionInfo.TrueInfo.SetActive(false);
        }
        if (questionInfo != null && questionInfo.FalseInfo != null)
        {
            questionInfo.FalseInfo.SetActive(false);
        }
        currentQuestion.SetActive(false);
        currentQuestionIndex++;
        soalTotal++;
        if (currentQuestionIndex < questions.Length)
        {
            ActivateQuestion(currentQuestionIndex);
        }
        // else
        // {
        //     if (answeredQuestions == questions.Length - 1)
        //     {
        //         ActivateFinalQuestion();
        //     }
        // }
        EnableAllButtons();
        isAnswering = false;
    }

    public void NextOnly()
    { 
        GameObject currentQuestion = questions[currentQuestionIndex];
        currentQuestion.SetActive(false);
        currentQuestionIndex++;
        if (currentQuestionIndex < questions.Length)
        {
            ActivateQuestion(currentQuestionIndex);
        }
        // else
        // {
        //     if (answeredQuestions == questions.Length - 1)
        //     {
        //         ActivateFinalQuestion();
        //     }
        // }
        EnableAllButtons();
        isAnswering = false;
    }

    // void ActivateFinalQuestion()
    // {
    //     questions[currentQuestionIndex - 1].SetActive(true);
    // }

    void EnableAllButtons()
    {
        foreach (GameObject question in questions)
        {
            Button[] buttons = question.GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                button.interactable = true;
            }
        }
    }

    public void MakingSureBack()
    {
        MakeSureBack.SetActive(true);
    }

    public void NoSureBack()
    {
        MakeSureBack.SetActive(false);
    }

    public void YesSureBack()
    {
        MakeSureBack.SetActive(false);
        SceneManager.LoadScene("AlphabetLesson");
    }
    
    public void Retry()
    {
        currentQuestionIndex = 0;
        soalTotal = 0;
        answeredCorrectly = 0;
        isAnswering = false;
        
        foreach (GameObject question in questions)
        {
            QuestionInfo questionInfo = question.GetComponent<QuestionInfo>();
            if (questionInfo != null)
            {
                if (questionInfo.TrueInfo != null)
                {
                    questionInfo.TrueInfo.SetActive(false);
                }
                if (questionInfo.FalseInfo != null)
                {
                    questionInfo.FalseInfo.SetActive(false);
                }
            }
            question.SetActive(false);
        }

        StartCoroutine(InitialObject());
        EnableAllButtons();
    }
}
