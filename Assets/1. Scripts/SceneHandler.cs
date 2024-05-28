using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void ChangeSceneTo(string sceneName)
    {
        // Memuat scene baru
        SceneManager.LoadScene(sceneName);
    }

}
