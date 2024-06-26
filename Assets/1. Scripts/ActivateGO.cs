using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGO : MonoBehaviour
{
    public GameObject[] gameObjects; // Array GameObject

    void Start()
    {
        BeforeGOAppear();
    }

    void BeforeGOAppear()
    {
        // Menonaktifkan semua GameObject dalam array
        foreach (GameObject go in gameObjects)
        {
            go.SetActive(false);
        }
    }

    public void MakeGOAppear(int index)
    {
        // Memastikan indeks valid
        if (index >= 0 && index < gameObjects.Length)
        {
            // Mengaktifkan GameObject pada indeks yang diberikan
            gameObjects[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Indeks di luar batas array");
        }
    }

    public void CloseGO(int index)
    {
        // Memastikan indeks valid dan GameObject pada indeks yang diberikan aktif
        if (index >= 0 && index < gameObjects.Length && gameObjects[index] != null && gameObjects[index].activeSelf)
        {
            // Menonaktifkan GameObject pada indeks yang diberikan
            gameObjects[index].SetActive(false);
        }
        else
        {
            Debug.LogError("Indeks di luar batas array atau GameObject tidak aktif");
        }
    }
}
