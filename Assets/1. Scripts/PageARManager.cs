using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages; // Array of all pages
    public Button[] buttons; // Array of buttons associated with each page

    private bool anyButtonPressed = false;

    void Start()
    {
        // Check if the arrays are of the same length
        if (buttons.Length != pages.Length)
        {
            Debug.LogError("The length of buttons array and pages array must be the same!");
            return;
        }

        // Attach button click listeners
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture the index for use in the lambda
            buttons[index].onClick.AddListener(() => OnButtonClicked(index));
        }

        // Set initial active state of pages
        foreach (var page in pages)
        {
            page.SetActive(true);
        }
    }

    void OnButtonClicked(int index)
    {
        // Deactivate the page associated with the button clicked
        pages[index].SetActive(false);

        // If this is the first button press, deactivate all other pages and set flag
        if (!anyButtonPressed)
        {
            anyButtonPressed = true;

            // Deactivate all pages
            foreach (var page in pages)
            {
                page.SetActive(false);
            }
        }
    }

    void Update()
    {
        // If any button has been pressed, ensure all pages remain inactive
        if (anyButtonPressed)
        {
            foreach (var page in pages)
            {
                if (page.activeSelf)
                {
                    page.SetActive(false);
                }
            }
        }
    }
}
