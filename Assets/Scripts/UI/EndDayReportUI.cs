using System;
using Candidate.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class EndDayReportUI : MonoBehaviour
    {
        public CandidateManager candidateManager;
        
        public Image lastReport;
        public Text totalCandidates, efficientHire, badHire;

        [HideInInspector] public int efficientHireNumber, badHireNumber;

        private void Start()
        {
            totalCandidates.text = candidateManager.candidatesToSpawn.ToString();
        }

        public void CallLastReport()
        {
            efficientHire.text = efficientHireNumber.ToString();
            badHire.text = badHireNumber.ToString();
            lastReport.gameObject.SetActive(true);
        }

        public void RestartButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
