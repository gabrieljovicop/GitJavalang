using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject TrueScreen;
    public GameObject FalseScreen;
    public GameObject[] Questions;
    
    // Start is called before the first frame update
    void Start()
    {
        //Questions[1].GameObject.SetActive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FalseAnswer(){
        //false screen salah
        //go to next soal
        //
    }
    public void TrueAnswer(int noQuestions){
        //matiin soal
        //go to next soal
    }
}
