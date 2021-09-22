using Candidate.ScriptableObjects;
using Core;
using UI;
using UnityEditor.Animations;
using UnityEngine;

namespace Candidate
{
    public class CandidateMovement : MonoBehaviour
    {
        public CandidateManager candidateManager;
        public CandidateInstantiate candidateInstantiate;
        public GameManager gameManager;
        public EndDayReportUI endDayReportUI;
        
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

        private void Update()
        {
            if (gameManager.isGameStarted)
            {
                _firstCandidate = candidateInstantiate.allCandidates[0];
            }
            
            if (gameManager.isGameStarted && !gameManager.inMeeting && !gameManager.isHired && !gameManager.isRejected)
            {
                Movement();
            }

            if (!HappyAnimEnded() && gameManager.isHired)
            {
                _firstCandidate.transform.LookAt(_exitDoorLocation);
                
                _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                    _exitDoorLocation, candidateManager.movementSpeed * Time.deltaTime);
                CandidateStateChecker();
                
            }

            if (!RejectedAnimEnded() && gameManager.isRejected)
            {
                _firstCandidate.transform.LookAt(_exitDoorLocation);
                
                _firstCandidate.transform.position = Vector3.MoveTowards(_firstCandidate.transform.position,
                    _exitDoorLocation, candidateManager.movementSpeed * Time.deltaTime);
                CandidateStateChecker();
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

            if (_firstCandidate.transform.position == chairToSit.transform.position)
            {
                firstCandidateAnimator.Play("Sitting");
                gameManager.inMeeting = true;
            }
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
        
        private void CandidateStateChecker()
        {
            if (gameManager.isHired || gameManager.isRejected)
            {
                if (gameManager.isHired)
                {
                    if (_firstCandidate.transform.position == _exitDoorLocation)
                    {
                        gameManager.isHired = false;
                        _firstCandidate.gameObject.SetActive(false);
                        ListOrganizer();
                    }
                }
                
                if(gameManager.isRejected)
                {
                    if (_firstCandidate.transform.position == _exitDoorLocation)
                    {
                        gameManager.isRejected = false;
                        _firstCandidate.gameObject.SetActive(false);
                        ListOrganizer();
                    }
                }
            }
        }

        private void ListOrganizer()
        {
           if (candidateInstantiate.allCandidates.Count >= 2)
           {
               candidateInstantiate.allCandidates.RemoveAt(0);
           }
           else
           {
               Debug.LogWarning("EVERYONE MET");
               endDayReportUI.CallLastReport();
               gameManager.isGameStarted = false;
           }
        }
    }
}
