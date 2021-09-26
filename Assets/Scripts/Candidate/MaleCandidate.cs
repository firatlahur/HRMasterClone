using System;
using Candidate.ScriptableObjects;
using Core;
using UnityEngine;
using Random = UnityEngine.Random;
using Hire_Rules.ScriptableObjects;

namespace Candidate
{
    public class MaleCandidate : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public GameManager gameManager;
        
        public HireRules hireRules;
        public HiringContainer hiringContainer;

        [HideInInspector]public string candidateWorkPosition;
        [HideInInspector]public int candidateExperience, candidateSalaryExpectation;

        private void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            transform.name = candidateManager.maleNames[Random.Range(0,candidateManager.maleNames.Length)];

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
