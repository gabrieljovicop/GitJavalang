using UnityEngine;
using UnityEngine.UI;

public class LinkOpener : MonoBehaviour
{
    public Button emailButton;
    public Button websiteButton;
    public Button cardsButton;
    public Button whatsappButton;
    public string email = "javalangapp@gmail.com"; 
    public string websiteURL = "https://javalang.my.canva.site/";
    public string getCards = "https://drive.google.com/drive/folders/156r2_ADxwGKs6dlSSpEGY_rgN4onYrxt?usp=sharing";
    public string whatsappNumber = "6282139137145";

    void Start()
    {
        if (emailButton != null)
        {
            emailButton.onClick.AddListener(OpenEmailClient);
        }
        if (websiteButton != null)
        {
            websiteButton.onClick.AddListener(OpenWebsite);
        }
        if (cardsButton != null)
        {
            cardsButton.onClick.AddListener(OpenGetCards);
        }
        if (whatsappButton != null)
        {
            whatsappButton.onClick.AddListener(OpenWhatsApp);
        }
    }

    private void OpenEmailClient()
    {
        string emailUrl = string.Format("mailto:{0}", email);
        Application.OpenURL(emailUrl);
    }

    private void OpenWebsite()
    {
        Application.OpenURL(websiteURL);
    }

    private void OpenGetCards()
    {
        Application.OpenURL(getCards);
    }
    
    private void OpenWhatsApp()
    {
        string whatsappUrl = string.Format("https://wa.me/{0}", whatsappNumber);
        Application.OpenURL(whatsappUrl);
    }
}