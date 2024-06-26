using UnityEngine;
using System.Collections;

public class GOSwitcher : MonoBehaviour
{
    // Array of game objects to switch between
    public GameObject[] gameObjects;

    // Current index in the array
    private int currentIndex = 0;

    void Start()
    {
        // Ensure only the first object is active at the start
        UpdateGameObjects();
    }

    // Function to switch to the next object in the array
    public void Next()
    {
        if (gameObjects.Length == 0)
            return;

        // Deactivate current object
        gameObjects[currentIndex].SetActive(false);

        // Move to the next index, looping back to the start if necessary
        currentIndex = (currentIndex + 1) % gameObjects.Length;

        // Activate the new current object
        gameObjects[currentIndex].SetActive(true);
    }

    // Function to switch to the previous object in the array
    public void Previous()
    {
        if (gameObjects.Length == 0)
            return;

        // Deactivate current object
        gameObjects[currentIndex].SetActive(false);

        // Move to the previous index, looping back to the end if necessary
        currentIndex = (currentIndex - 1 + gameObjects.Length) % gameObjects.Length;

        // Activate the new current object
        gameObjects[currentIndex].SetActive(true);
    }

    // Helper function to activate only the current game object
    private void UpdateGameObjects()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == currentIndex);
        }
    }
}
