// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SignInButton : MonoBehaviour
// {
//     // Nama scene yang akan dituju
//     public string sceneName;

//     // GameObject yang akan diaktifkan setelah pindah scene
//     public GameObject targetObject;

//     // Fungsi untuk menangani klik tombol SignIn
//     public void OnSignInButtonClicked()
//     {
//         // Memuat scene baru
//         SceneManager.LoadScene(sceneName);

//         // Memanggil fungsi yang akan dijalankan setelah scene selesai dimuat
//         SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode);
//     }

//     // Fungsi yang akan dijalankan setelah scene selesai dimuat
//     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     {
//         // Mencari GameObject target di dalam scene baru
//         GameObject newTargetObject = GameObject.Find(targetObject.name);

//         // Memastikan GameObject target ditemukan
//         if (newTargetObject != null)
//         {
//             // Menonaktifkan semua GameObject di scene kecuali targetObject
//             foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
//             {
//                 if (obj != newTargetObject)
//                 {
//                     obj.SetActive(false);
//                 }
//             }

//             // Mengaktifkan GameObject target
//             newTargetObject.SetActive(true);
//         }
//         else
//         {
//             Debug.LogWarning("Target object not found in the new scene.");
//         }

//         // Memanggil fungsi ini hanya sekali, kemudian melepaskan event listener
//         SceneManager.sceneLoaded -= OnSceneLoaded;
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonActionType
{
    SignIn,
    Home,
    // Tambahkan jenis aksi lainnya di sini jika diperlukan
}

public class ButtonAction : MonoBehaviour
{
    // Nama scene yang akan dituju
    public string sceneName;

    // Nama GameObject yang akan diaktifkan setelah pindah scene
    public string targetObjectName;

    // Jenis aksi tombol
    public ButtonActionType actionType;

    // Fungsi untuk menangani klik tombol
    public void OnButtonClicked()
    {
        // Memuat scene baru
        SceneManager.LoadScene(sceneName);

        // Memanggil fungsi yang akan dijalankan setelah scene selesai dimuat
        SceneManager.sceneLoaded += (scene, mode) => OnSceneLoaded(scene, mode);
    }

    // Fungsi yang akan dijalankan setelah scene selesai dimuat
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Mencari GameObject target di dalam scene baru berdasarkan nama
        GameObject newTargetObject = GameObject.Find(targetObjectName);

        // Memastikan GameObject target ditemukan
        if (newTargetObject != null)
        {
            // Menonaktifkan semua GameObject di scene kecuali targetObject
            foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
            {
                if (obj != newTargetObject)
                {
                    obj.SetActive(false);
                }
            }

            // Mengaktifkan GameObject target
            newTargetObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Target object not found in the new scene.");
        }

        // Memanggil fungsi ini hanya sekali, kemudian melepaskan event listener
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
