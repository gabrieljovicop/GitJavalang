using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingHandler : MonoBehaviour
{
    public GameObject HalamanRun1; public GameObject HalamanRun2;

    void Start(){
        StartCoroutine(ShowPages());
    }

    IEnumerator ShowPages()
    {
        // Menampilkan halaman awalan run 1
        HalamanRun1.SetActive(true);

        // Menunggu selama 0.5 detik
        yield return new WaitForSeconds(0.5f);

        // Menyembunyikan halaman awalan run 1
        HalamanRun1.SetActive(false);

        // Menampilkan halaman awalan run 2
        HalamanRun2.SetActive(true);

        // Menunggu selama 1 detik
        yield return new WaitForSeconds(1f);

        // Menambahkan baris untuk mengganti scene ke scene lain
        SceneManager.LoadScene("ApkFirstRun");
    }
}
