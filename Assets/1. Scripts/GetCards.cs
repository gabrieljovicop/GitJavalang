using UnityEngine;
using UnityEngine.UI;

public class LinktoGetCards : MonoBehaviour
{
    public Button[] cardsButton; // Array of buttons for cards
    public string getCards = "https://drive.google.com/drive/folders/156r2_ADxwGKs6dlSSpEGY_rgN4onYrxt?usp=sharing"; // URL to open when card buttons are clicked

    void Start()
    {
        if (cardsButton != null && cardsButton.Length > 0)
        {
            foreach (Button cardButton in cardsButton)
            {
                if (cardButton != null)
                {
                    cardButton.onClick.AddListener(OpenGetCards);
                }
            }
        }
    }

    private void OpenGetCards()
    {
        Application.OpenURL(getCards);
    }
}
