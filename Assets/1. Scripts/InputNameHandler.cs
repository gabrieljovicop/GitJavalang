using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveUserName : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button continueButton; // Tombol untuk melanjutkan
    public TextMeshProUGUI warningText; // Teks untuk peringatan

    private void Start()
    {
        // Menambahkan listener untuk memeriksa input setiap kali nilai berubah
        nameInputField.onValueChanged.AddListener(delegate { ValidateInput(); });
        // Menonaktifkan tombol lanjut saat awal
        continueButton.interactable = false;
    }

    public void SaveName()
    {
        string userName = nameInputField.text;
        PlayerPrefs.SetString("UserName", userName);
        Debug.Log("Username saved: " + userName);
    }

    private void ValidateInput()
    {
        // Memeriksa apakah input diisi dan panjangnya kurang dari atau sama dengan 15 karakter
        if (string.IsNullOrEmpty(nameInputField.text) || nameInputField.text.Length > 15 || nameInputField.text.Length < 3)
        {
            // Menampilkan peringatan sesuai kondisi
            if (string.IsNullOrEmpty(nameInputField.text))
                warningText.text = "Username can't be empty";
            else
                warningText.text = "Username must contain 3-15 characters";
            
            // Menonaktifkan tombol jika input tidak valid
            continueButton.interactable = false;
        }
        else
        {
            // Menghilangkan peringatan jika input valid
            warningText.text = "";
            // Mengaktifkan tombol jika input valid
            continueButton.interactable = true;
        }
    }
}
