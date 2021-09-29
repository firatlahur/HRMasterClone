using System;
using Hire_Rules.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
       public HiringContainer hiringContainer;

       public float idCardDragSpeed;

       [HideInInspector] public int selectedJobCategory;
       [HideInInspector]public bool isGameStarted, inMeeting, isHired, isRejected;

       private void Start()
       {
           selectedJobCategory = Random.Range(0, hiringContainer.jobCategories.Length);
       }
    }
}
