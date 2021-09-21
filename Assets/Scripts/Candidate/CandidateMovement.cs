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

        public AnimatorController idleController, otherAnimsController;

        private bool _inMeeting;
        private Quaternion _firstInLineRotation;

        private void Awake()
        {
            _firstInLineRotation = Quaternion.Euler(0f, 90f, 0f);

        }

        private void Update()
        {
            if (gameManager.isGameStarted && !_inMeeting)
            {
                Movement();
            }
        }

        private void Movement()
        {
            GameObject firstCandidate = candidateInstantiate.allCandidates[0];
            Animator firstCandidateAnimator = firstCandidate.GetComponent<Animator>();

            firstCandidate.transform.position = Vector3.MoveTowards(firstCandidate.transform.position,
                chairToSit.transform.position, candidateManager.movementSpeed * Time.deltaTime);

            firstCandidateAnimator.runtimeAnimatorController = otherAnimsController;


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
            AnimationManager();
            
            if (firstCandidate.transform.position != chairToSit.transform.position) return;
            firstCandidateAnimator.Play("Sitting");
            _inMeeting = true;
            ListOrganizer();
        }

        private void AnimationManager()
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
            if (_inMeeting)
            {
                Debug.Log("IN MEETING");
            }
        }
    }
}
