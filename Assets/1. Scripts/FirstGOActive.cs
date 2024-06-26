using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOActiveAfterChangeScene : MonoBehaviour
{
    public GameObject Halaman;
    
    void Start()
    {
        ShowPages();
    }

    void ShowPages()
    {
        if(Halaman != null)
        {
        Halaman.SetActive(true);
        }
    }
}

