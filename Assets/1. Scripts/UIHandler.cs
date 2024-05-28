using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public GameObject HalamanHome; public GameObject HalamanProfile;
    public GameObject HalamanAboutApp;
    
    void Start(){
        ShowPages();
    }

    void ShowPages(){
        HalamanHome.SetActive(true);
    }

    public void changeToHome(){
        HalamanProfile.SetActive(false);
        HalamanHome.SetActive(true);
    }
    public void changeToProfile(){
        HalamanHome.SetActive(false);
        HalamanAboutApp.SetActive(false);
        HalamanProfile.SetActive(true);
    }
    public void changeToAboutApp(){
        HalamanProfile.SetActive(false);
        HalamanAboutApp.SetActive(true);
    }

}
