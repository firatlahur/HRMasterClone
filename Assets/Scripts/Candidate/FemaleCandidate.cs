using Candidate.ScriptableObjects;
using Core;
using Hire_Rules.ScriptableObjects;
using UnityEngine;

namespace Candidate
{
    public class FemaleCandidate : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public HireRules hireRules;
        public HiringContainer hiringContainer;
        public GameManager gameManager;
       
        [HideInInspector]public string candidateWorkPosition;
        [HideInInspector]public int candidateExperience, candidateSalaryExpectation;
        
        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        
        private void Start()
        {
            transform.name = candidateManager.femaleNames[Random.Range(0,candidateManager.femaleNames.Length)];
            
            candidateWorkPosition = hiringContainer.jobCategories[gameManager.selectedJobCategory]
                .hiringPositions[
                    Random.Range(0,
                        hiringContainer.jobCategories[gameManager.selectedJobCategory].hiringPositions.Length)];

            candidateExperience =
                Random.Range(hiringContainer.jobCategories[gameManager.selectedJobCategory].minExpYears,
                    hiringContainer.jobCategories[gameManager.selectedJobCategory].maxExpYears);

            candidateSalaryExpectation =
                Random.Range(hiringContainer.jobCategories[gameManager.selectedJobCategory].minSalaryDollars,
                    hiringContainer.jobCategories[gameManager.selectedJobCategory].maxSalaryDollars);
        }
    }
}
