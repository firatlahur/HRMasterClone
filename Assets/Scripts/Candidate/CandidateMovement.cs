using System;
using System.Collections;
using Candidate.ScriptableObjects;
using Core;
using TMPro.EditorUtilities;
using UnityEditor.Animations;
using UnityEngine;

namespace Candidate
{
    public class CandidateMovement : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public CandidateInstantiate candidateInstantiate;
        public GameManager gameManager;
        public GameObject chairToSit, exitDoor;

        public AnimatorController nonIdleAnimController;

        private Quaternion _firstInLineRotation;
        private GameObject _firstCandidate;

        private void Awake()
        {
            _firstInLineRotation = Quaternion.Euler(0f, 90f, 0f);
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _firstCandidate = candidateInstantiate.allCandidates[0];
        }

        private void Update()
        {
            if (gameManager.isGameStarted && !gameManager.inMeeting && !gameManager.isHired)
            {
                Movement();
            }

            if (!HappyAnimEnded() && gameManager.isHired)
            {
                _firstCandidate.transform.LookAt(exitDoor.transform.position);
                _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                    exitDoor.transform.position, candidateManager.movementSpeed * Time.deltaTime);
            }
        }

        private void Movement()
        {
            Animator firstCandidateAnimator = _firstCandidate.GetComponent<Animator>();
            
            _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                chairToSit.transform.position, candidateManager.movementSpeed * Time.deltaTime);

            firstCandidateAnimator.runtimeAnimatorController = nonIdleAnimController;


            for (int i = 1; i < candidateInstantiate.allCandidates.Count; i++)
            {
                GameObject candidatesInLine = candidateInstantiate.allCandidates[i];
                
                if (candidatesInLine.transform.position != Vector3.zero)
                {
                    candidatesInLine.transform.position = Vector3.MoveTowards(
                        candidatesInLine.transform.position, Vector3.zero,
                        candidateManager.movementSpeed * Time.deltaTime);
                }
                else
                {
                    candidatesInLine.transform.rotation = _firstInLineRotation;
                }
            }
            WalkingAnimationManager();
            
            if (_firstCandidate.transform.position != chairToSit.transform.position) return;
            firstCandidateAnimator.Play("Sitting");
            gameManager.inMeeting = true;
            ListOrganizer();
        }

        private void WalkingAnimationManager()
        {
            for (int i = 1; i < candidateInstantiate.allCandidates.Count; i++)
            {
               Animator[] animator = candidateInstantiate.allCandidates[i].transform.GetComponents<Animator>();

                foreach (Animator anim in animator)
                {
                    anim.Play(candidateInstantiate.allCandidates[1].transform.position != Vector3.zero
                        ? "Walking"
                        : "Idle");
                }
            }
        }

        private void ListOrganizer()
        {
            if (gameManager.inMeeting)
            {
                Debug.Log("IN MEETING LIST ORGANIZER");
            }
        }

        public void HiredCandidateAnimation() //isHired true
        {
            Animator firstCandidateAnimator = _firstCandidate.GetComponent<Animator>();
            firstCandidateAnimator.Play("Happy");
        }

        private bool HappyAnimEnded()
        {
            bool x = false;
            if (gameManager.isHired)
            {
                x = _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Happy") &&
                    _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f;
                Debug.Log(x);
            }
            return x;
        }
    }
}
