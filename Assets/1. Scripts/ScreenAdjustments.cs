using UnityEngine;

public class ScreenAdjustments : MonoBehaviour
{
    private int navBarHeight = 0;
    //private int statusBarHeight = 0;

    void Start()
    {
        navBarHeight = GetNavigationBarHeight();
        //statusBarHeight = GetStatusBarHeight();
        Debug.Log("Navigation Bar Height: " + navBarHeight);
        //Debug.Log("Status Bar Height: " + statusBarHeight);

        AdjustUIForScreen();
    }

    int GetNavigationBarHeight()
    {
        int result = 0;
        #if UNITY_ANDROID
        try
        {
            using (AndroidJavaObject activity = new AndroidJavaObject("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject context = activity.Get<AndroidJavaObject>("currentActivity"))
                {
                    using (AndroidJavaObject resources = context.Call<AndroidJavaObject>("getResources"))
                    {
                        int resourceId = resources.Call<int>("getIdentifier", "navigation_bar_height", "dimen", "android");
                        if (resourceId > 0)
                        {
                            result = resources.Call<int>("getDimensionPixelSize", resourceId);
                        }
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to get navigation bar height: " + e.Message);
        }
        #endif
        return result;
    }

    // int GetStatusBarHeight()
    // {
    //     int result = 0;
    //     #if UNITY_ANDROID
    //     try
    //     {
    //         using (AndroidJavaObject activity = new AndroidJavaObject("com.unity3d.player.UnityPlayer"))
    //         {
    //             using (AndroidJavaObject context = activity.Get<AndroidJavaObject>("currentActivity"))
    //             {
    //                 using (AndroidJavaObject resources = context.Call<AndroidJavaObject>("getResources"))
    //                 {
    //                     int resourceId = resources.Call<int>("getIdentifier", "status_bar_height", "dimen", "android");
    //                     if (resourceId > 0)
    //                     {
    //                         result = resources.Call<int>("getDimensionPixelSize", resourceId);
    //                     }
    //                 }
    //             }
    //         }
    //     }
    //     catch (System.Exception e)
    //     {
    //         Debug.LogError("Failed to get status bar height: " + e.Message);
    //     }
    //     #endif
    //     return result;
    // }

    void AdjustUIForScreen()
    {
        // Adjust your UI elements here based on the navBarHeight and statusBarHeight
        // This example assumes you have a RectTransform you want to adjust
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.offsetMin = new Vector2(rectTransform.offsetMin.x, rectTransform.offsetMin.y + navBarHeight); // Bottom offset
            //rectTransform.offsetMax = new Vector2(rectTransform.offsetMax.x, rectTransform.offsetMax.y - statusBarHeight); // Top offset
        }
    }
}
