using Candidate.ScriptableObjects;
using Hire_Rules.ScriptableObjects;
using UnityEngine;

namespace Candidate
{
    public class FemaleCandidate : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public HireRules hireRules;
       
        [HideInInspector]public string candidateWorkPosition;
        [HideInInspector]public int candidateExperience, candidateSalaryExpectation;
        
        private void Start()
        {
            transform.name = candidateManager.femaleNames[Random.Range(0,candidateManager.femaleNames.Length)];
            
            candidateWorkPosition = hireRules.hiringPositions[Random.Range(0, hireRules.hiringPositions.Length)];
            
            candidateExperience = Random.Range(hireRules.minExpYears, hireRules.maxExpYears);
            
            candidateSalaryExpectation = Random.Range(hireRules.minSalaryDollars, hireRules.maxSalaryDollars);
        }
    }
}
