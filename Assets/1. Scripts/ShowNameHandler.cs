using UnityEngine;
using TMPro;

public class DisplayName : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI nameProfile;

    void Start()
    {
        string userName = PlayerPrefs.GetString("UserName", "User");
        nameText.text = "Hello,\n" + userName + "!";
        nameProfile.text = userName;
    }
}
