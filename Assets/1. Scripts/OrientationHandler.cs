using UnityEngine;

public class GameObjectLayarSwitcher : MonoBehaviour
{
    // Game object dalam mode portrait
    public GameObject portraitObject;

    // Game object dalam mode landscape
    public GameObject landscapeObject; 
    public GameObject landscapeKhusus;

    // Fungsi yang dipanggil setiap kali ukuran layar berubah
    private void Update()
    {
        if(Screen.width > Screen.height){
            if(Screen.width >= Screen.height*2)
            {
                //Untuk memenuhi standard layar hp saya pula
                SwitchObjects(landscapeKhusus, portraitObject, landscapeObject);
            }else{
                // Mode landscape
                SwitchObjects(landscapeObject, portraitObject, landscapeKhusus);
            }
        }else{
                // Mode portrait
                SwitchObjects(portraitObject, landscapeObject, landscapeKhusus);
        }
    }

    // Fungsi untuk beralih antara game object
    private void SwitchObjects(GameObject enableObject, GameObject disableObject1, GameObject disableObject2)
    {
        enableObject.SetActive(true);
        disableObject1.SetActive(false); disableObject2.SetActive(false);
    }
}
