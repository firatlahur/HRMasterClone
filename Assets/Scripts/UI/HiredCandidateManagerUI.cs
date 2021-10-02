using System.IO;
using Core;
using UnityEngine;

namespace UI
{
    public class HiredCandidateManagerUI : MonoBehaviour
    {
        public GameObject hiredCandidateDetails;
        public RectTransform content;

        private float _employeeUIOffset;
        private int _totalEmployee;

        private void Awake()
        {
            _employeeUIOffset += 200f;
            _totalEmployee = 1;
        }

        public void InstantiateUIid()
        {
           GameObject hiredCandidate = Instantiate(hiredCandidateDetails, content.transform, false);
           hiredCandidate.gameObject.SetActive(false);
        }

        public void ContentSizeManager()
        {
            _totalEmployee++;
            
            if (_totalEmployee > 1)
            {
                content.sizeDelta += new Vector2(0, _employeeUIOffset);
            }
        }
    }
}
