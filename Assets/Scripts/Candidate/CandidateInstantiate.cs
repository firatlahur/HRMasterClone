using System;
using System.Collections.Generic;
using Candidate.ScriptableObjects;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Candidate
{
    public class CandidateInstantiate : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public List<GameObject> allCandidates;
        public GameObject candidateContainer;
        
        
        private Vector3 _candidateSpawnOffset;
        private Quaternion _inLineRotation, _firstInLineRotation;

        private void Awake()
        {
            allCandidates = new List<GameObject>();
            
            _candidateSpawnOffset = new Vector3(-.55f, 0, .52f);
            
            _inLineRotation = Quaternion.Euler(0f, 120f, 0f);
            _firstInLineRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        private void Start()
        {
            InstantiateCandidates();
        }

        private void InstantiateCandidates()
        {
            for (int i = 0; i < candidateManager.candidatesToSpawn; i++)
            {
              GameObject candidate =
                  Instantiate(candidateManager.candidates[Random.Range(0, candidateManager.candidates.Length)],
                    Vector3.zero, _firstInLineRotation);
              
              if (i > 0)
              {
                  candidate.transform.position += _candidateSpawnOffset;
                  candidate.transform.rotation = _inLineRotation;
                  _candidateSpawnOffset.x--;
                  _candidateSpawnOffset.z++;
              }
              allCandidates.Add(candidate);
              candidate.transform.SetParent(candidateContainer.transform);
            }
        }
    }
}
