using UnityEngine;

public class NavigationBarController : MonoBehaviour
{
    void Start()
    {
        // Menjaga navigation bar tetap menyala
        EnableNavigationBar();
    }

    void EnableNavigationBar()
    {
        // Hanya berlaku untuk Android
        #if UNITY_ANDROID
        // Menggunakan plugin Android untuk mengatur navigation bar
        try
        {
            using (AndroidJavaObject activity = new AndroidJavaObject("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject window = activity.Get<AndroidJavaObject>("currentActivity").Call<AndroidJavaObject>("getWindow"))
                {
                    window.Call("clearFlags", new AndroidJavaObject("android.view.WindowManager$LayoutParams").GetStatic<int>("FLAG_FULLSCREEN"));
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to enable navigation bar: " + e.Message);
        }
        #endif
    }
}
