using UnityEngine;
using UnityEngine.UI;

public class BackButtonHandler : MonoBehaviour
{
    public GameObject exitPanel; // Panel yang berisi tombol keluar

    void Start()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(false); // Pastikan panel tidak aktif saat aplikasi dimulai
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Tombol back pada perangkat Android
        {
            if (exitPanel != null)
            {
                exitPanel.SetActive(true); // Aktifkan panel saat tombol back ditekan
            }
        }
    }

    public void ExitApplication()
    {
        Application.Quit(); // Keluar dari aplikasi
    }

    public void CancelExit()
    {
        if (exitPanel != null)
        {
            exitPanel.SetActive(false); // Nonaktifkan panel jika pengguna membatalkan keluar
        }
    }
}
