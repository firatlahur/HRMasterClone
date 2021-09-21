using Candidate.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;
using Hire_Rules.ScriptableObjects;

namespace Candidate
{
    public class MaleCandidate : MonoBehaviour
    {
        public CandidateManager candidateManager;
        
        public HireRules hireRules;

        [HideInInspector]public string candidateWorkPosition;
        [HideInInspector]public int candidateExperience, candidateSalaryExpectation;
        
        private void Start()
        {
            transform.name = candidateManager.maleNames[Random.Range(0,candidateManager.maleNames.Length)];
            
            candidateWorkPosition = hireRules.hiringPositions[Random.Range(0, hireRules.hiringPositions.Length)];
            
            candidateExperience = Random.Range(hireRules.minExpYears, hireRules.maxExpYears);
            
            candidateSalaryExpectation = Random.Range(hireRules.minSalaryDollars, hireRules.maxSalaryDollars);
        }
    }
}
