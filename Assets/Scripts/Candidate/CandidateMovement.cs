using System.Collections;
using Candidate.ScriptableObjects;
using Core;
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
        private Vector3 _exitDoorLocation;

        private void Awake()
        {
            _firstInLineRotation = Quaternion.Euler(0f, 90f, 0f);
            _exitDoorLocation = exitDoor.transform.position;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _firstCandidate = candidateInstantiate.allCandidates[0];
        }

        private void Update()
        {
            if (gameManager.isGameStarted && !gameManager.inMeeting && !gameManager.isHired && !gameManager.isRejected)
            {
                Movement();
            }

            if (!HappyAnimEnded() && gameManager.isHired)
            {
                _firstCandidate.transform.LookAt(exitDoor.transform.position);
                
                _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                    _exitDoorLocation, candidateManager.movementSpeed * Time.deltaTime);
                
            }

            if (!RejectedAnimEnded() && gameManager.isRejected)
            {
                _firstCandidate.transform.LookAt(exitDoor.transform.position);
                
                _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                    _exitDoorLocation, candidateManager.movementSpeed * Time.deltaTime);
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


        public void HiredCandidateAnimation()
        {
            Animator firstCandidateAnimator = _firstCandidate.GetComponent<Animator>();
            firstCandidateAnimator.Play("Happy");
        }

        public void RejectedCandidateAnimation()
        {
            Animator firstCandidateAnimator = _firstCandidate.GetComponent<Animator>();
            firstCandidateAnimator.Play("Sad");
        }

        private bool HappyAnimEnded()
        {
            bool x = false;
            if (gameManager.isHired)
            {
                x = _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Happy") &&
                    _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f;
            }
            return x;
        }

        private bool RejectedAnimEnded()
        {
            bool x = false;
            if (gameManager.isRejected)
            {
                x = _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Sad") &&
                    _firstCandidate.transform.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f;
            }
            return x;
        }
        
        private void ListOrganizer()
        {
            if (gameManager.inMeeting)
            {
                Debug.Log("IN MEETING LIST ORGANIZER");
            }
        }
    }
}
