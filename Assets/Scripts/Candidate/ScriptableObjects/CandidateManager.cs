using System;
using System.Collections.Generic;
using UnityEngine;

namespace Candidate.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Candidate Manager",menuName = "Scriptable Objects/Candidate Manager")]
    public class CandidateManager : ScriptableObject
    {
        public GameObject[] candidates;

        public int candidatesToSpawn;
        public float movementSpeed;
        
        public string[] maleNames, femaleNames;
    }
}
