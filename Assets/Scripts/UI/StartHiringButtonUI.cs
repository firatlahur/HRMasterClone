using System;
using Candidate;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartHiringButtonUI : MonoBehaviour
    {
        public CandidateInstantiate candidateInstantiate;
        public Image hiringRulesTop;
        public Button startHiringButton;
        private Camera _mainCamera;
        private Vector3 _cameraHiringOffset;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _cameraHiringOffset = new Vector3(4f, 1.4f, 0f);
        }

        public void StartHiring()
        {
            hiringRulesTop.gameObject.SetActive(true);
            startHiringButton.gameObject.SetActive(false);
            _mainCamera.transform.position = _cameraHiringOffset;
            candidateInstantiate.InstantiateCandidates();
        }
    }
}
