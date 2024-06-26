using UnityEngine;
using TMPro;

public class DisplayName : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI userNameProfile;
    public TextMeshProUGUI displayNameProfile;
    public TextMeshProUGUI displayNamePlaceholder;
    public TextMeshProUGUI userNameStatic;

    void Start()
    {
        string userName = PlayerPrefs.GetString("UserName", "User");
        if(nameText != null)
        {
        nameText.text = "Hello,\n" + userName + "!";
        }
        if(userNameProfile != null)
        {
        userNameProfile.text = "@" + userName;
        }
        if(displayNameProfile != null)
        {
        displayNameProfile.text = userName;
        }
        if(displayNamePlaceholder != null)
        {
        displayNamePlaceholder.text = userName;
        }
        if(userNameStatic != null)
        {
        userNameStatic.text = userName;
        }

    }
}
