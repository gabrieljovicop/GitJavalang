// using UnityEngine;
// using Vuforia;

// public class CombinationDetector : MonoBehaviour
// {
//     public GameObject kartuA;
//     public GameObject kartuB;
//     public GameObject kartuE;
//     public GameObject kartuÊ;
    
//     public GameObject arObjectBA;
//     public GameObject arObjectBE;
//     public GameObject arObjectBÊ;

//     private ObserverBehaviour observerA;
//     private ObserverBehaviour observerB;
//     private ObserverBehaviour observerE;
//     private ObserverBehaviour observerÊ;

//     void Start()
//     {
//         arObjectBA.SetActive(false);
//         arObjectBE.SetActive(false);
//         arObjectBÊ.SetActive(false);

//         observerA = kartuA.GetComponent<ObserverBehaviour>();
//         observerB = kartuB.GetComponent<ObserverBehaviour>();
//         observerE = kartuE.GetComponent<ObserverBehaviour>();
//         observerÊ = kartuÊ.GetComponent<ObserverBehaviour>();

//         observerA.OnTargetStatusChanged += OnTargetStatusChanged;
//         observerB.OnTargetStatusChanged += OnTargetStatusChanged;
//         observerE.OnTargetStatusChanged += OnTargetStatusChanged;
//         observerÊ.OnTargetStatusChanged += OnTargetStatusChanged;
//     }

//     void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
//     {
//         bool isKartuAVisible = observerA.TargetStatus.Status == Status.TRACKED || observerA.TargetStatus.Status == Status.EXTENDED_TRACKED;
//         bool isKartuBVisible = observerB.TargetStatus.Status == Status.TRACKED || observerB.TargetStatus.Status == Status.EXTENDED_TRACKED;
//         bool isKartuEVisible = observerE.TargetStatus.Status == Status.TRACKED || observerE.TargetStatus.Status == Status.EXTENDED_TRACKED;
//         bool isKartuÊVisible = observerÊ.TargetStatus.Status == Status.TRACKED || observerÊ.TargetStatus.Status == Status.EXTENDED_TRACKED;

//         arObjectBA.SetActive(isKartuBVisible && isKartuAVisible);
//         arObjectBE.SetActive(isKartuBVisible && isKartuEVisible);
//         arObjectBÊ.SetActive(isKartuBVisible && isKartuÊVisible);
//     }

//     bool CheckRelativePosition(ObserverBehaviour mainCard, ObserverBehaviour relativeCard, string expectedPosition)
//     {
//         Vector3 mainPosition = mainCard.transform.position;
//         Vector3 relativePosition = relativeCard.transform.position;

//         Vector3 direction = (relativePosition - mainPosition).normalized;

//         switch (expectedPosition)
//         {
//             case "right":
//                 return Vector3.Dot(mainCard.transform.right, direction) > 0.5f;
//             case "left":
//                 return Vector3.Dot(mainCard.transform.right, direction) < -0.5f;
//             case "up":
//                 return Vector3.Dot(mainCard.transform.up, direction) > 0.5f;
//             case "down":
//                 return Vector3.Dot(mainCard.transform.up, direction) < -0.5f;
//             default:
//                 return false;
//         }
//     }
// }

using UnityEngine;
using Vuforia;
//using System.Linq;
using System.Collections.Generic;

public class CombinationDetector : MonoBehaviour
{
    public GameObject[] kartu;
    public GameObject[] arObjects; 

    private ObserverBehaviour[] observers;

    private bool[,] combinationSet;

    void Start()
    {
        // Initialize combination set
        combinationSet = new bool[26, 6];
        for (int i = 6; i < 26; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                combinationSet[i, j] = true;
            }
        }

        // Initialize AR objects and observers
        foreach (GameObject arObject in arObjects)
        {
            if (arObject != null)
            {
                arObject.SetActive(false);
            }
        }

        observers = new ObserverBehaviour[kartu.Length];
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
        if (observers == null)
        {
            Debug.LogError("Observers array is null.");
            return;
        }

        bool[] isKartuVisible = new bool[observers.Length];
        for (int i = 0; i < observers.Length; i++)
        {
            if (observers[i] != null)
            {
                isKartuVisible[i] = observers[i].TargetStatus.Status == Status.TRACKED || observers[i].TargetStatus.Status == Status.EXTENDED_TRACKED;
            }
            else
            {
                Debug.LogError($"Observer at index {i} is null.");
            }
        }

        // Update AR objects based on visibility combinations
        for (int i = 0; i < arObjects.Length; i++)
        {
            int index1 = i / 6 + 6;  // Assuming the first index in combinations
            int index2 = i % 6;      // Assuming the second index in combinations

            if (index1 < isKartuVisible.Length && index2 < isKartuVisible.Length)
            {
                if (arObjects[i] != null)
                {
                    arObjects[i].SetActive(combinationSet[index1, index2] && isKartuVisible[index1] && isKartuVisible[index2]);
                }
                else
                {
                    Debug.LogWarning($"AR object at index {i} is null.");
                }
            }
        }
    }

    void OnDestroy()
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
 

//     private HashSet<(int, int)> combinationSet;
//     private bool[] isKartuVisible;

//     void Start()
//     {
//         // Initialize combination set
//         combinationSet = new HashSet<(int, int)>
//         {
//             (6, 0), (6, 1), (6, 2), (6, 3), (6, 4), (6, 5),
//             (7, 0), (7, 1), (7, 2), (7, 3), (7, 4), (7, 5),
//             (8, 0), (8, 1), (8, 2), (8, 3), (8, 4), (8, 5),
//             (9, 0), (9, 1), (9, 2), (9, 3), (9, 4), (9, 5),
//             (10, 0), (10, 1), (10, 2), (10, 3), (10, 4), (10, 5),
//             (11, 0), (11, 1), (11, 2), (11, 3), (11, 4), (11, 5),
//             (12, 0), (12, 1), (12, 2), (12, 3), (12, 4), (12, 5),
//             (13, 0), (13, 1), (13, 2), (13, 3), (13, 4), (13, 5),
//             (14, 0), (14, 1), (14, 2), (14, 3), (14, 4), (14, 5),
//             (15, 0), (15, 1), (15, 2), (15, 3), (15, 4), (15, 5),
//             (16, 0), (16, 1), (16, 2), (16, 3), (16, 4), (16, 5),
//             (17, 0), (17, 1), (17, 2), (17, 3), (17, 4), (17, 5),
//             (18, 0), (18, 1), (18, 2), (18, 3), (18, 4), (18, 5),
//             (19, 0), (19, 1), (19, 2), (19, 3), (19, 4), (19, 5),
//             (20, 0), (20, 1), (20, 2), (20, 3), (20, 4), (20, 5),
//             (21, 0), (21, 1), (21, 2), (21, 3), (21, 4), (21, 5),
//             (22, 0), (22, 1), (22, 2), (22, 3), (22, 4), (22, 5),
//             (23, 0), (23, 1), (23, 2), (23, 3), (23, 4), (23, 5),
//             (24, 0), (24, 1), (24, 2), (24, 3), (24, 4), (24, 5),
//             (25, 0), (25, 1), (25, 2), (25, 3), (25, 4), (25, 5)
//         };

//         // Initialize AR objects and observers
//         foreach (GameObject arObject in arObjects)
//         {
//             if (arObject != null)
//             {
//                 arObject.SetActive(false);
//             }
//         }

//         observers = new ObserverBehaviour[kartu.Length];
//         isKartuVisible = new bool[kartu.Length];

//         for (int i = 0; i < kartu.Length; i++)
//         {
//             if (kartu[i] != null)
//             {
//                 observers[i] = kartu[i].GetComponent<ObserverBehaviour>();
//                 if (observers[i] != null)
//                 {
//                     observers[i].OnTargetStatusChanged += OnTargetStatusChanged;
//                 }
//                 else
//                 {
//                     Debug.LogError($"ObserverBehaviour component not found on {kartu[i].name}");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("One of the cards is null in the kartu array.");
//             }
//         }
//     }

//     void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
//     {
//         for (int i = 0; i < observers.Length; i++)
//         {
//             if (observers[i] != null)
//             {
//                 isKartuVisible[i] = observers[i].TargetStatus.Status == Status.TRACKED || observers[i].TargetStatus.Status == Status.EXTENDED_TRACKED;
//             }
//             else
//             {
//                 Debug.LogError($"Observer at index {i} is null.");
//             }
//         }

//         // Update AR objects based on visibility combinations
//         for (int i = 0; i < arObjects.Length; i++)
//         {
//             if (arObjects[i] != null)
//             {
//                 int index1 = i / 6 + 6;
//                 int index2 = i % 6;

//                 if (index1 < isKartuVisible.Length && index2 < isKartuVisible.Length)
//                 {
//                     arObjects[i].SetActive(combinationSet.Contains((index1, index2)) && isKartuVisible[index1] && isKartuVisible[index2]);
//                 }
//                 else
//                 {
//                     Debug.LogWarning($"Invalid indices: {index1}, {index2}");
//                 }
//             }
//             else
//             {
//                 Debug.LogWarning($"AR object at index {i} is null.");
//             }
//         }
//     }

//     void OnDestroy()
//     {
//         if (observers != null)
//         {
//             foreach (var observer in observers)
//             {
//                 if (observer != null)
//                 {
//                     observer.OnTargetStatusChanged -= OnTargetStatusChanged;
//                 }
//                 else
//                 {
//                     Debug.LogError("One of the observers is null in the OnDestroy method.");
//                 }
//             }
//         }
//     }
// }

// using UnityEngine;
// using Vuforia;
// using System.Collections.Generic;

// public class CombinationDetector : MonoBehaviour
// {
//     public GameObject[] kartu;
//     public GameObject[] arObjects; 

//     private ObserverBehaviour[] observers;      


//     void Start()
//     {
//         // Initialize AR objects and observers
//         foreach (GameObject arObject in arObjects)
//         {
//             if (arObject != null)
//                 arObject.SetActive(false);
//         }

//         observers = new ObserverBehaviour[kartu.Length];
//         for (int i = 0; i < kartu.Length; i++)
//         {
//             if (kartu[i] != null)
//             {
//                 observers[i] = kartu[i].GetComponent<ObserverBehaviour>();
//                 if (observers[i] != null)
//                 {
//                     observers[i].OnTargetStatusChanged += OnTargetStatusChanged;
//                 }
//                 else
//                 {
//                     Debug.LogError($"ObserverBehaviour component not found on {kartu[i].name}");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("One of the cards is null in the kartu array.");
//             }
//         }
//     }

//     void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
//     {
//         if (observers == null)
//         {
//             Debug.LogError("Observers array is null.");
//             return;
//         }

//         bool[] isKartuVisible = new bool[observers.Length];
//         for (int i = 0; i < observers.Length; i++)
//         {
//             if (observers[i] != null)
//             {
//                 isKartuVisible[i] = observers[i].TargetStatus.Status == Status.TRACKED || observers[i].TargetStatus.Status == Status.EXTENDED_TRACKED;
//             }
//             else
//             {
//                 Debug.LogError($"Observer at index {i} is null.");
//             }
//         }

//         // Ensure the indices match the combination requirements [B+A, B+E, B+Ê]
//         if (arObjects.Length >= 1)
//         {
//             arObjects[0].SetActive(isKartuVisible[6] && isKartuVisible[0]); // BA
//             arObjects[1].SetActive(isKartuVisible[6] && isKartuVisible[1]); // BI
//             arObjects[2].SetActive(isKartuVisible[6] && isKartuVisible[2]); // BE
//             arObjects[3].SetActive(isKartuVisible[6] && isKartuVisible[3]); // BÊ
//             arObjects[4].SetActive(isKartuVisible[6] && isKartuVisible[4]); // BO
//             arObjects[5].SetActive(isKartuVisible[6] && isKartuVisible[5]); // BU

//             arObjects[6].SetActive(isKartuVisible[7] && isKartuVisible[0]); // CA
//             arObjects[7].SetActive(isKartuVisible[7] && isKartuVisible[1]); // CI
//             arObjects[8].SetActive(isKartuVisible[7] && isKartuVisible[2]); // CE
//             arObjects[9].SetActive(isKartuVisible[7] && isKartuVisible[3]); // CÊ
//             arObjects[10].SetActive(isKartuVisible[7] && isKartuVisible[4]); // CO
//             arObjects[11].SetActive(isKartuVisible[7] && isKartuVisible[5]); // CU

//             arObjects[12].SetActive(isKartuVisible[8] && isKartuVisible[0]); // DA
//             arObjects[13].SetActive(isKartuVisible[8] && isKartuVisible[1]); // DI
//             arObjects[14].SetActive(isKartuVisible[8] && isKartuVisible[2]); // DE
//             arObjects[15].SetActive(isKartuVisible[8] && isKartuVisible[3]); // DÊ
//             arObjects[16].SetActive(isKartuVisible[8] && isKartuVisible[4]); // DO
//             arObjects[17].SetActive(isKartuVisible[8] && isKartuVisible[5]); // DU

//             arObjects[18].SetActive(isKartuVisible[9] && isKartuVisible[0]); // GA
//             arObjects[19].SetActive(isKartuVisible[9] && isKartuVisible[1]); // GI
//             arObjects[20].SetActive(isKartuVisible[9] && isKartuVisible[2]); // GE
//             arObjects[21].SetActive(isKartuVisible[9] && isKartuVisible[3]); // GÊ
//             arObjects[22].SetActive(isKartuVisible[9] && isKartuVisible[4]); // GO
//             arObjects[23].SetActive(isKartuVisible[9] && isKartuVisible[5]); // GU

//             arObjects[24].SetActive(isKartuVisible[10] && isKartuVisible[0]); // HA
//             arObjects[25].SetActive(isKartuVisible[10] && isKartuVisible[1]); // HI
//             arObjects[26].SetActive(isKartuVisible[10] && isKartuVisible[2]); // HE
//             arObjects[27].SetActive(isKartuVisible[10] && isKartuVisible[3]); // HÊ
//             arObjects[28].SetActive(isKartuVisible[10] && isKartuVisible[4]); // HO
//             arObjects[29].SetActive(isKartuVisible[10] && isKartuVisible[5]); // HU

//             arObjects[30].SetActive(isKartuVisible[11] && isKartuVisible[0]); // JA
//             arObjects[31].SetActive(isKartuVisible[11] && isKartuVisible[1]); // JI
//             arObjects[32].SetActive(isKartuVisible[11] && isKartuVisible[2]); // JE
//             arObjects[33].SetActive(isKartuVisible[11] && isKartuVisible[3]); // JÊ
//             arObjects[34].SetActive(isKartuVisible[11] && isKartuVisible[4]); // JO
//             arObjects[35].SetActive(isKartuVisible[11] && isKartuVisible[5]); // JU

//             arObjects[36].SetActive(isKartuVisible[12] && isKartuVisible[0]); // KA
//             arObjects[37].SetActive(isKartuVisible[12] && isKartuVisible[1]); // KI
//             arObjects[38].SetActive(isKartuVisible[12] && isKartuVisible[2]); // KE
//             arObjects[39].SetActive(isKartuVisible[12] && isKartuVisible[3]); // KÊ
//             arObjects[40].SetActive(isKartuVisible[12] && isKartuVisible[4]); // KO
//             arObjects[41].SetActive(isKartuVisible[12] && isKartuVisible[5]); // KU

//             arObjects[42].SetActive(isKartuVisible[13] && isKartuVisible[0]); // LA
//             arObjects[43].SetActive(isKartuVisible[13] && isKartuVisible[1]); // LI
//             arObjects[44].SetActive(isKartuVisible[13] && isKartuVisible[2]); // LE
//             arObjects[45].SetActive(isKartuVisible[13] && isKartuVisible[3]); // LÊ
//             arObjects[46].SetActive(isKartuVisible[13] && isKartuVisible[4]); // LO
//             arObjects[47].SetActive(isKartuVisible[13] && isKartuVisible[5]); // LU

//             arObjects[48].SetActive(isKartuVisible[14] && isKartuVisible[0]); // MA
//             arObjects[49].SetActive(isKartuVisible[14] && isKartuVisible[1]); // MI
//             arObjects[50].SetActive(isKartuVisible[14] && isKartuVisible[2]); // ME
//             arObjects[51].SetActive(isKartuVisible[14] && isKartuVisible[3]); // MÊ
//             arObjects[52].SetActive(isKartuVisible[14] && isKartuVisible[4]); // MO
//             arObjects[53].SetActive(isKartuVisible[14] && isKartuVisible[5]); // MU

//             arObjects[54].SetActive(isKartuVisible[15] && isKartuVisible[0]); // NA
//             arObjects[55].SetActive(isKartuVisible[15] && isKartuVisible[1]); // NI
//             arObjects[56].SetActive(isKartuVisible[15] && isKartuVisible[2]); // NE
//             arObjects[57].SetActive(isKartuVisible[15] && isKartuVisible[3]); // NÊ
//             arObjects[58].SetActive(isKartuVisible[15] && isKartuVisible[4]); // NO
//             arObjects[59].SetActive(isKartuVisible[15] && isKartuVisible[5]); // NU

//             arObjects[60].SetActive(isKartuVisible[16] && isKartuVisible[0]); // PA
//             arObjects[61].SetActive(isKartuVisible[16] && isKartuVisible[1]); // PI
//             arObjects[62].SetActive(isKartuVisible[16] && isKartuVisible[2]); // PE
//             arObjects[63].SetActive(isKartuVisible[16] && isKartuVisible[3]); // PÊ
//             arObjects[64].SetActive(isKartuVisible[16] && isKartuVisible[4]); // PO
//             arObjects[65].SetActive(isKartuVisible[16] && isKartuVisible[5]); // PU

//             arObjects[66].SetActive(isKartuVisible[17] && isKartuVisible[0]); // RA
//             arObjects[67].SetActive(isKartuVisible[17] && isKartuVisible[1]); // RI
//             arObjects[68].SetActive(isKartuVisible[17] && isKartuVisible[2]); // RE
//             arObjects[69].SetActive(isKartuVisible[17] && isKartuVisible[3]); // RÊ
//             arObjects[70].SetActive(isKartuVisible[17] && isKartuVisible[4]); // RO
//             arObjects[71].SetActive(isKartuVisible[17] && isKartuVisible[5]); // RU

//             arObjects[72].SetActive(isKartuVisible[18] && isKartuVisible[0]); // SA
//             arObjects[73].SetActive(isKartuVisible[18] && isKartuVisible[1]); // SI
//             arObjects[74].SetActive(isKartuVisible[18] && isKartuVisible[2]); // SE
//             arObjects[75].SetActive(isKartuVisible[18] && isKartuVisible[3]); // SÊ
//             arObjects[76].SetActive(isKartuVisible[18] && isKartuVisible[4]); // SO
//             arObjects[77].SetActive(isKartuVisible[18] && isKartuVisible[5]); // SU

//             arObjects[78].SetActive(isKartuVisible[19] && isKartuVisible[0]); // TA
//             arObjects[79].SetActive(isKartuVisible[19] && isKartuVisible[1]); // TI
//             arObjects[80].SetActive(isKartuVisible[19] && isKartuVisible[2]); // TE
//             arObjects[81].SetActive(isKartuVisible[19] && isKartuVisible[3]); // TÊ
//             arObjects[82].SetActive(isKartuVisible[19] && isKartuVisible[4]); // TO
//             arObjects[83].SetActive(isKartuVisible[19] && isKartuVisible[5]); // TU

//             arObjects[84].SetActive(isKartuVisible[20] && isKartuVisible[0]); // WA
//             arObjects[85].SetActive(isKartuVisible[20] && isKartuVisible[1]); // WI
//             arObjects[86].SetActive(isKartuVisible[20] && isKartuVisible[2]); // WE
//             arObjects[87].SetActive(isKartuVisible[20] && isKartuVisible[3]); // WÊ
//             arObjects[88].SetActive(isKartuVisible[20] && isKartuVisible[4]); // WO
//             arObjects[89].SetActive(isKartuVisible[20] && isKartuVisible[5]); // WU

//             arObjects[90].SetActive(isKartuVisible[21] && isKartuVisible[0]); // YA
//             arObjects[91].SetActive(isKartuVisible[21] && isKartuVisible[1]); // YI
//             arObjects[92].SetActive(isKartuVisible[21] && isKartuVisible[2]); // YE
//             arObjects[93].SetActive(isKartuVisible[21] && isKartuVisible[3]); // YÊ
//             arObjects[94].SetActive(isKartuVisible[21] && isKartuVisible[4]); // YO
//             arObjects[95].SetActive(isKartuVisible[21] && isKartuVisible[5]); // YU

//             arObjects[96].SetActive(isKartuVisible[22] && isKartuVisible[0]); // DHA
//             arObjects[97].SetActive(isKartuVisible[22] && isKartuVisible[1]); // DHI
//             arObjects[98].SetActive(isKartuVisible[22] && isKartuVisible[2]); // DHE
//             arObjects[99].SetActive(isKartuVisible[22] && isKartuVisible[3]); // DHÊ
//             arObjects[100].SetActive(isKartuVisible[22] && isKartuVisible[4]); // DHO
//             arObjects[101].SetActive(isKartuVisible[22] && isKartuVisible[5]); // DHU

//             arObjects[102].SetActive(isKartuVisible[23] && isKartuVisible[0]); // THA
//             arObjects[103].SetActive(isKartuVisible[23] && isKartuVisible[1]); // THI
//             arObjects[104].SetActive(isKartuVisible[23] && isKartuVisible[2]); // THE
//             arObjects[105].SetActive(isKartuVisible[23] && isKartuVisible[3]); // THÊ
//             arObjects[106].SetActive(isKartuVisible[23] && isKartuVisible[4]); // THO
//             arObjects[107].SetActive(isKartuVisible[23] && isKartuVisible[5]); // THU

//             arObjects[108].SetActive(isKartuVisible[24] && isKartuVisible[0]); // NGA
//             arObjects[109].SetActive(isKartuVisible[24] && isKartuVisible[1]); // NGI
//             arObjects[110].SetActive(isKartuVisible[24] && isKartuVisible[2]); // NGE
//             arObjects[111].SetActive(isKartuVisible[24] && isKartuVisible[3]); // NGÊ
//             arObjects[112].SetActive(isKartuVisible[24] && isKartuVisible[4]); // NGO
//             arObjects[113].SetActive(isKartuVisible[24] && isKartuVisible[5]); // NGU

//             arObjects[114].SetActive(isKartuVisible[25] && isKartuVisible[0]); // NYA
//             arObjects[115].SetActive(isKartuVisible[25] && isKartuVisible[1]); // NYI
//             arObjects[116].SetActive(isKartuVisible[25] && isKartuVisible[2]); // NYE
//             arObjects[117].SetActive(isKartuVisible[25] && isKartuVisible[3]); // NYÊ
//             arObjects[118].SetActive(isKartuVisible[25] && isKartuVisible[4]); // NYO
//             arObjects[119].SetActive(isKartuVisible[25] && isKartuVisible[5]); // NYU
//         }
//         else
//         {
//             Debug.LogError("Not enough AR objects specified in the arObjects array.");
//         }
//     }

//     void OnDestroy()
//     {
//         if (observers != null)
//         {
//             foreach (var observer in observers)
//             {
//                 if (observer != null)
//                 {
//                     observer.OnTargetStatusChanged -= OnTargetStatusChanged;
//                 }
//                 else
//                 {
//                     Debug.LogError("One of the observers is null in the OnDestroy method.");
//                 }
//             }
//         }
//     }
// }
