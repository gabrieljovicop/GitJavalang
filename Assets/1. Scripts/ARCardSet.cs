using UnityEngine;
using Vuforia;
using System.Collections.Generic;

public class MultipleDualImageTargetController : MonoBehaviour
{
    [System.Serializable]
    public class ARSet
    {
        public GameObject arObject;  // Objek AR yang akan muncul
        public ImageTargetBehaviour imageTarget1;  // Image Target pertama
        public ImageTargetBehaviour imageTarget2;  // Image Target kedua
    }

    public List<ARSet> arSets;

    private Dictionary<ARSet, (bool, bool)> trackingStatus;

    void Start()
    {
        trackingStatus = new Dictionary<ARSet, (bool, bool)>();

        foreach (var arSet in arSets)
        {
            // Pastikan objek AR tidak terlihat saat mulai
            arSet.arObject.SetActive(false);

            // Inisialisasi status tracking
            trackingStatus[arSet] = (false, false);

            // Daftarkan event handler untuk masing-masing image target
            arSet.imageTarget1.OnTargetStatusChanged += (behaviour, targetStatus) => OnTargetStatusChanged(behaviour, targetStatus, arSet, true);
            arSet.imageTarget2.OnTargetStatusChanged += (behaviour, targetStatus) => OnTargetStatusChanged(behaviour, targetStatus, arSet, false);
        }
    }

    void OnDestroy()
    {
        foreach (var arSet in arSets)
        {
            // Hapus event handler ketika objek dihancurkan
            arSet.imageTarget1.OnTargetStatusChanged -= (behaviour, targetStatus) => OnTargetStatusChanged(behaviour, targetStatus, arSet, true);
            arSet.imageTarget2.OnTargetStatusChanged -= (behaviour, targetStatus) => OnTargetStatusChanged(behaviour, targetStatus, arSet, false);
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus, ARSet arSet, bool isFirstTarget)
    {
        bool isTracked = (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED);

        if (isFirstTarget)
        {
            trackingStatus[arSet] = (isTracked, trackingStatus[arSet].Item2);
        }
        else
        {
            trackingStatus[arSet] = (trackingStatus[arSet].Item1, isTracked);
        }

        bool isBothTracked = trackingStatus[arSet].Item1 && trackingStatus[arSet].Item2;

        // Tampilkan atau sembunyikan objek AR berdasarkan status tracking
        arSet.arObject.SetActive(isBothTracked);
        
    }
}

