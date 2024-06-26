using UnityEngine;
using Vuforia;

public class AlphaCardsCombination : MonoBehaviour
{
    public GameObject[] vokalKartu; // 6 GameObject untuk huruf vokal
    public GameObject[] konsonanKartu; // 20 GameObject untuk huruf konsonan
    
    public GameObject[] arObjectA; // Array untuk kombinasi vokalKartu[0] (A) dengan 20 konsonanKartu
    public GameObject[] arObjectI; // Array untuk kombinasi vokalKartu[1] (I) dengan 20 konsonanKartu
    public GameObject[] arObjectE; // Array untuk kombinasi vokalKartu[2] (U) dengan 20 konsonanKartu
    public GameObject[] arObjectÊ; // Array untuk kombinasi vokalKartu[3] (E) dengan 20 konsonanKartu
    public GameObject[] arObjectO; // Array untuk kombinasi vokalKartu[4] (O) dengan 20 konsonanKartu
    public GameObject[] arObjectU; // Array untuk kombinasi vokalKartu[5] (Y) dengan 20 konsonanKartu

    private ObserverBehaviour[] observersVokal;
    private ObserverBehaviour[] observersKonsonan;
    private bool[] isVokalVisible;
    private bool[] isKonsonanVisible;

    void Start()
    {
        // Initialize AR objects and observers
        InitializeARObjects(arObjectA);
        InitializeARObjects(arObjectI);
        InitializeARObjects(arObjectE);
        InitializeARObjects(arObjectÊ);
        InitializeARObjects(arObjectO);
        InitializeARObjects(arObjectU);

        // Initialize observers for vokal and konsonan
        InitializeObservers(vokalKartu, ref observersVokal, ref isVokalVisible);
        InitializeObservers(konsonanKartu, ref observersKonsonan, ref isKonsonanVisible);
    }

    void InitializeARObjects(GameObject[] arObjects)
    {
        foreach (GameObject arObject in arObjects)
        {
            if (arObject != null)
            {
                arObject.SetActive(false);
            }
        }
    }

    void InitializeObservers(GameObject[] kartu, ref ObserverBehaviour[] observers, ref bool[] isVisible)
    {
        observers = new ObserverBehaviour[kartu.Length];
        isVisible = new bool[kartu.Length];

        for (int i = 0; i < kartu.Length; i++)
        {
            if (kartu[i] != null)
            {
                observers[i] = kartu[i].GetComponent<ObserverBehaviour>();
                if (observers[i] != null)
                {
                    observers[i].OnTargetStatusChanged += OnTargetStatusChanged;
                }
                else
                {
                    Debug.LogError($"ObserverBehaviour component not found on {kartu[i].name}");
                }
            }
            else
            {
                Debug.LogError("One of the cards is null in the kartu array.");
            }
        }
    }

    void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        UpdateVisibilityStatus(observersVokal, ref isVokalVisible);
        UpdateVisibilityStatus(observersKonsonan, ref isKonsonanVisible);

        // Update AR objects based on visibility combinations
        UpdateARObjects(arObjectA, 0);
        UpdateARObjects(arObjectI, 1);
        UpdateARObjects(arObjectE, 2);
        UpdateARObjects(arObjectÊ, 3);
        UpdateARObjects(arObjectO, 4);
        UpdateARObjects(arObjectU, 5);
    }

    void UpdateVisibilityStatus(ObserverBehaviour[] observers, ref bool[] isVisible)
    {
        for (int i = 0; i < observers.Length; i++)
        {
            if (observers[i] != null)
            {
                isVisible[i] = observers[i].TargetStatus.Status == Status.TRACKED || observers[i].TargetStatus.Status == Status.EXTENDED_TRACKED;
            }
            else
            {
                Debug.LogError($"Observer at index {i} is null.");
            }
        }
    }

    void UpdateARObjects(GameObject[] arObjects, int vokalIndex)
    {
        if (vokalIndex < isVokalVisible.Length && isVokalVisible[vokalIndex])
        {
            for (int i = 0; i < arObjects.Length; i++)
            {
                if (arObjects[i] != null)
                {
                    arObjects[i].SetActive(isKonsonanVisible[i]);
                }
                else
                {
                    Debug.LogWarning($"AR object at index {i} is null.");
                }
            }
        }
        else
        {
            foreach (var arObject in arObjects)
            {
                if (arObject != null)
                {
                    arObject.SetActive(false);
                }
            }
        }
    }

    void OnDestroy()
    {
        RemoveObservers(observersVokal);
        RemoveObservers(observersKonsonan);
    }

    void RemoveObservers(ObserverBehaviour[] observers)
    {
        if (observers != null)
        {
            foreach (var observer in observers)
            {
                if (observer != null)
                {
                    observer.OnTargetStatusChanged -= OnTargetStatusChanged;
                }
                else
                {
                    Debug.LogError("One of the observers is null in the OnDestroy method.");
                }
            }
        }
    }
}
