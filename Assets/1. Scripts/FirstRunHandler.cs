using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRunHandler : MonoBehaviour
{
    public GameObject HalamanPilihBahasa; public GameObject HalamanRegistrasi;
    public GameObject HalamanIsiNama;

    void Start(){
        ShowPages();
    }

    void ShowPages(){
        //IENumerator hanya berfungsi normal jika ada yield return
        // Menampilkan halaman pilih bahasa tanpa penundaan
        HalamanPilihBahasa.SetActive(true);
    }
    
    public void changeToRegis(){
        HalamanPilihBahasa.SetActive(false);
        HalamanRegistrasi.SetActive(true);
    }

    public void changeToIsiNama(){
        HalamanRegistrasi.SetActive(false);
        HalamanIsiNama.SetActive(true);
    }
    
}
