using UnityEngine;

public class ScreenOrientationManager : MonoBehaviour
{
    // Orientasi layar yang diinginkan
    public ScreenOrientation desiredOrientation;

    // Fungsi yang dipanggil saat skrip diaktifkan
    private void Start()
    {
        // Set orientasi layar menjadi orientasi yang diinginkan
        Screen.orientation = desiredOrientation;
    }

    // Fungsi yang dipanggil saat skrip dinonaktifkan
    private void OnDestroy()
    {
        // Set orientasi layar kembali ke orientasi default (Auto Rotation)
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
