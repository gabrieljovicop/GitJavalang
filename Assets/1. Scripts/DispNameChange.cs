using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class DisplayNameChange : MonoBehaviour
{
    public TMP_InputField nameInputField;  // InputField untuk memasukkan nama
    public TextMeshProUGUI displayText;    // TextMeshProUGUI untuk menampilkan nama
    //public TextMeshProUGUI displayTextHome;    // TextMeshProUGUI untuk menampilkan nama
    public Button changeNameButton;        // Tombol untuk memulai perubahan nama
    public Button saveChangesButton;       // Tombol untuk menyimpan perubahan nama
    public Button cancelChangesButton;     // Tombol untuk membatalkan perubahan nama
    public GameObject changeStatus;        // GameObject untuk mengubah status
    public GameObject wantChanges;         // GameObject yang berisi save dan cancel button
    public TextMeshProUGUI namePlaceholder;  // Placeholder untuk menampilkan nama setelah diganti
    public TextMeshProUGUI errorMessage;   // TextMeshProUGUI untuk menampilkan pesan kesalahan

    private const string DisplayNameKey = "DisplayName";  // Kunci untuk menyimpan display name di PlayerPrefs
    private string originalName;  // Nama asli sebelum perubahan

    private void Start()
    {
        // Memuat display name terakhir dari PlayerPrefsManager
        LoadDisplayName();

        // Menambahkan listener ke tombol untuk memanggil fungsi masing-masing saat tombol diklik
        changeNameButton.onClick.AddListener(EnableNameInput);
        saveChangesButton.onClick.AddListener(DisplayEnteredName);
        cancelChangesButton.onClick.AddListener(CancelChanges);

        // Menonaktifkan wantChanges, changeStatus, dan errorMessage pada awalnya
        wantChanges.SetActive(false);
        changeStatus.SetActive(false);
        errorMessage.gameObject.SetActive(false); // Pesan kesalahan di-nonaktifkan pada awalnya

        // Nonaktifkan interaksi pada nameInputField pada awalnya
        nameInputField.interactable = false;
    }

    private void EnableNameInput()
    {
        Debug.Log("Change Name Button Clicked");
        // Simpan nama asli sebelum diubah
        originalName = displayText.text;

        // Matikan placeholder dan tampilkan input field serta wantChanges saat tombol changeName diklik
        namePlaceholder.gameObject.SetActive(false);
        nameInputField.interactable = true;
        nameInputField.text = displayText.text; // Isi input field dengan nama yang ada
        wantChanges.SetActive(true);
        errorMessage.gameObject.SetActive(false); // Sembunyikan pesan kesalahan saat mulai memasukkan nama
    }

    private void DisplayEnteredName()
    {
        Debug.Log("Save Changes Button Clicked");
        // Mendapatkan teks dari InputField
        string enteredName = nameInputField.text;

        // Validasi panjang nama
        if (enteredName.Length >= 3 && enteredName.Length <= 15)
        {
            // Menyimpan display name ke PlayerPrefsManager
            PlayerPrefs.SetString(DisplayNameKey, enteredName);
            PlayerPrefs.Save();

            // Menampilkan nama di TextMeshProUGUI dan placeholder
            displayText.text = enteredName;
            //displayTextHome.text = enteredName;
            namePlaceholder.text = enteredName; // Atur teks pada placeholder
            namePlaceholder.gameObject.SetActive(true); // Aktifkan placeholder

            // Mengaktifkan changeStatus jika nama berhasil ditampilkan
            StartCoroutine(ShowChangeStatus());

            // Menonaktifkan InputField dan wantChanges setelah nama disimpan
            nameInputField.interactable = false;
            wantChanges.SetActive(false);
            errorMessage.gameObject.SetActive(false); // Sembunyikan pesan kesalahan
        }
        else
        {
            // Menampilkan pesan kesalahan jika nama tidak valid
            errorMessage.text = "The display name must have 3-15 characters";
            errorMessage.gameObject.SetActive(true);

            // Kembalikan displayText ke nama asli
            displayText.text = originalName;
            //displayTextHome.text = originalName;
        }
    }

    private void LoadDisplayName()
    {
        // Memuat display name terakhir dari PlayerPrefsManager
        string savedName = PlayerPrefs.GetString(DisplayNameKey, string.Empty);
        if (!string.IsNullOrEmpty(savedName))
        {
            displayText.text = savedName;
            //displayTextHome.text = savedName;
            namePlaceholder.text = savedName;
            namePlaceholder.gameObject.SetActive(true); // Pastikan placeholder aktif saat memulai
        }
    }

    private IEnumerator ShowChangeStatus()
    {
        changeStatus.SetActive(true);
        yield return new WaitForSeconds(2); // Menunggu selama 2 detik
        changeStatus.SetActive(false);
    }

    private void CancelChanges()
    {
        Debug.Log("Cancel Changes Button Clicked");
        // Menonaktifkan InputField dan wantChanges jika perubahan dibatalkan
        nameInputField.interactable = false;
        wantChanges.SetActive(false);
        errorMessage.gameObject.SetActive(false); // Sembunyikan pesan kesalahan
        namePlaceholder.gameObject.SetActive(true); // Tampilkan kembali placeholder dengan nama lama

        // Kembalikan displayText ke nama asli
        displayText.text = originalName;
        //displayTextHome.text = originalName;
    }
}
