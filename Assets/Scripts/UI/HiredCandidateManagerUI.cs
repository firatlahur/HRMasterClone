using System;
using System.Collections;
using Candidate;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HiredCandidateManagerUI : MonoBehaviour
    {
        public GameObject hiredCandidateDetails;
        public RectTransform content;

        private int _totalEmployeeNumber = 1;
        private float _employeeUIOffset;

        private void Awake()
        {
            _totalEmployeeNumber = 1;
            _employeeUIOffset += 200f;
        }

        public void SaveHiredCandidate(GameObject hiredCandidate)
        {
           Instantiate(hiredCandidateDetails, content.transform, false);
        }

        public void ContentSizeManager()
        {
            Debug.Log("AAA");
            _totalEmployeeNumber++;
            
            if (_totalEmployeeNumber > 1)
            {
                content.sizeDelta += new Vector2(0, _employeeUIOffset);
            }
        }
    }
}
