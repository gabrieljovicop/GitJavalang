using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private const string DisplayNameKey = "DisplayName";

    void Awake()
    {
        // Hapus DisplayNameKey hanya jika di penjalanan sebelumnya ada
        if (PlayerPrefs.HasKey(DisplayNameKey))
        {
            PlayerPrefs.DeleteKey(DisplayNameKey);
            PlayerPrefs.Save();
        }
    }
}
