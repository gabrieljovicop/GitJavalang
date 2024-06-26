using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Prac1AlphaHandler : MonoBehaviour
{
    public GameObject TrueScreen;
    public GameObject FalseScreen;
    public GameObject[] Questions; // Array GameObject untuk menyimpan GameObject yang akan dinavigasikan
    private int currentIndex = 0; // Indeks GameObject saat ini

    void Start()
    {
        // Pastikan indeks saat ini berada di dalam rentang yang valid
        currentIndex = Mathf.Clamp(currentIndex, 0, Questions.Length - 1);
        // Tampilkan GameObject pertama saat aplikasi dimulai
        ShowCurrentGameObject();
    }

    public void Next()
    {
        // Navigasi ke GameObject berikutnya
        currentIndex++;
        if (currentIndex >= Questions.Length)
        {
            currentIndex = 0; // Kembali ke GameObject pertama jika sudah mencapai akhir daftar
        }
        ShowCurrentGameObject();
    }

    public void Previous()
    {
        // Navigasi ke GameObject sebelumnya
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = Questions.Length - 1; // Kembali ke GameObject terakhir jika sudah mencapai awal daftar
        }
        ShowCurrentGameObject();
    }

    void ShowCurrentGameObject()
    {
        // Sembunyikan semua GameObject
        foreach (GameObject go in Questions)
        {
            go.SetActive(false);
        }
        // Tampilkan GameObject yang sesuai dengan indeks saat ini
        Questions[currentIndex].SetActive(true);
    }
    public GameObject MakeSureBack;

    public void makesureback(){
        MakeSureBack.SetActive(true);
    }
    public void nosureback(){
        MakeSureBack.SetActive(false);
    }
    public void yessureback(){
        MakeSureBack.SetActive(false);
        SceneManager.LoadScene("AlphabetLesson");
    }

}
