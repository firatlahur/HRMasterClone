using System;
using Candidate;
using Candidate.ScriptableObjects;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class CandidateIDCardUI : MonoBehaviour
    {
        public HiredCandidateDetailsPrefab hiredCandidateDetailsPrefab;
        public HiredCandidateManagerUI hiredCandidateManagerUI;
        public CandidateInstantiate candidateInstantiate;
        public CandidateMovement candidateMovement;
        public CandidateManager candidateManager;
        public EndDayReportUI endDayReportUI;
        public HireRulesUI hireRulesUI;
        public GameManager gameManager;

        public Sprite[] candidatePhotoArray;
        public Text candidateName, candidatePosition, candidateExperience, candidateSalary, fakeIDText;
        public Image candidateInformation, candidatePhoto;

        private const int MaleSprite = 0;
        private const int FemaleSprite = 1;
        private const int FakeCandidateLayer = 6;

        private GameObject _firstCandidate;
        private Vector2 _candidateInfoDefaultPosition;

        private MaleCandidate _maleCandidate;
        private FemaleCandidate _femaleCandidate;

        private bool _male, _female, _fakeCheck, _isDeciding;

        private void Awake()
        {
            _candidateInfoDefaultPosition = candidateInformation.rectTransform.position;
        }

        private void Update()
        {
            if (gameManager.isGameStarted)
            {
                _firstCandidate = candidateInstantiate.allCandidates[0];
            }

            if (gameManager.inMeeting && !_isDeciding)
            {
                SetCardInformation();
            }

            if (Input.touchCount > 0 && candidateInformation.IsActive())
            {
                TouchMovement();
            }
        }

        private void TouchMovement()
        {
            if (gameManager.inMeeting && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (Input.GetTouch(0).deltaPosition.x > 0)
                {
                    candidateInformation.transform.Translate(
                        candidateInformation.transform.position * gameManager.idCardDragSpeed * Time.deltaTime,
                        Space.World);
                }

                if (Input.GetTouch(0).deltaPosition.x < 0)
                {
                    candidateInformation.transform.Translate(
                        -candidateInformation.transform.position * gameManager.idCardDragSpeed * Time.deltaTime,
                        Space.World);
                }
            }

            if (_isDeciding && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                bool hiredAxisCheck = candidateInformation.rectTransform.anchoredPosition.x > -50f;

                if (candidateInformation.rectTransform.anchoredPosition.x < 75f && hiredAxisCheck)
                {
                    candidateInformation.transform.position = _candidateInfoDefaultPosition;
                }

                if (candidateInformation.rectTransform.anchoredPosition.x > 75f)
                {
                    HiredCandidate();
                }

                if (Input.GetTouch(0).deltaPosition.x < 0 && !hiredAxisCheck)
                {
                    RejectedCandidate();
                }
            }
        }

        private void SetCardInformation()
        {
            _maleCandidate = _firstCandidate.transform.GetComponent<MaleCandidate>();
            _femaleCandidate = _firstCandidate.transform.GetComponent<FemaleCandidate>();

            _male = _maleCandidate;
            _female = _femaleCandidate;

            _isDeciding = true;

            if (_isDeciding)
            {
                candidateInformation.gameObject.SetActive(true);

                if (_male)
                {
                    candidateExperience.text = "+" + _maleCandidate.candidateExperience + " Years";

                    candidatePosition.text = _maleCandidate.candidateWorkPosition;

                    candidateSalary.text = _maleCandidate.candidateSalaryExpectation + "$";
                    IdentityCheck();
                }
                else if (_female)
                {
                    candidateExperience.text = "+" + _femaleCandidate.candidateExperience + " Years";

                    candidatePosition.text = _femaleCandidate.candidateWorkPosition;

                    candidateSalary.text = _femaleCandidate.candidateSalaryExpectation + "$";
                    IdentityCheck();
                }
            }
        }

        private void IdentityCheck()
        {
            if (!_fakeCheck)
            {
                if (_male)
                {
                    if (Random.Range(0, 100) > 85)
                    {
                        candidatePhoto.sprite = candidatePhotoArray[FemaleSprite];

                        _firstCandidate.name =
                            candidateManager.femaleNames[Random.Range(0, candidateManager.femaleNames.Length)];
                        _firstCandidate.gameObject.layer = FakeCandidateLayer;
                        fakeIDText.gameObject.SetActive(true);
                    }
                    else
                    {
                        candidatePhoto.sprite = candidatePhotoArray[MaleSprite];
                    }
                }
                else if (_female)
                {
                    if (Random.Range(0, 100) > 85)
                    {
                        candidatePhoto.sprite = candidatePhotoArray[MaleSprite];

                        _firstCandidate.name =
                            candidateManager.maleNames[Random.Range(0, candidateManager.maleNames.Length)];
                        _firstCandidate.gameObject.layer = FakeCandidateLayer;
                        fakeIDText.gameObject.SetActive(true);
                    }
                    else
                    {
                        candidatePhoto.sprite = candidatePhotoArray[FemaleSprite];
                    }
                }

                candidateName.text = _firstCandidate.name;
                _fakeCheck = true;
            }
        }

        private void HiredCandidate()
        {
            candidateInformation.gameObject.SetActive(false);
            fakeIDText.gameObject.SetActive(false);

            HiredCandidateEfficiency(_firstCandidate);
            hiredCandidateDetailsPrefab.SetEmployeeDetails(_firstCandidate);
            hiredCandidateManagerUI.SaveHiredCandidate(_firstCandidate);

            gameManager.inMeeting = false;
            gameManager.isHired = true;
            _isDeciding = false;
            _fakeCheck = false;

            candidateInformation.transform.position = _candidateInfoDefaultPosition;
            candidateMovement.HiredCandidateAnimation();
        }

        private void RejectedCandidate()
        {
            candidateInformation.gameObject.SetActive(false);
            fakeIDText.gameObject.SetActive(false);

            gameManager.inMeeting = false;
            gameManager.isRejected = true;
            _isDeciding = false;
            _fakeCheck = false;

            candidateInformation.transform.position = _candidateInfoDefaultPosition;
            candidateMovement.RejectedCandidateAnimation();
        }

        private void HiredCandidateEfficiency(GameObject hiredCandidate)
        {
            if (hiredCandidate.GetComponent<MaleCandidate>())
            {
                MaleCandidate maleCandidateStats = hiredCandidate.GetComponent<MaleCandidate>();

                if (maleCandidateStats.candidateWorkPosition != hireRulesUI.hiringPositionString ||
                    maleCandidateStats.candidateExperience < hireRulesUI.hiringExpInt ||
                    maleCandidateStats.candidateSalaryExpectation > hireRulesUI.hiringSalaryInt ||
                    hiredCandidate.gameObject.layer == FakeCandidateLayer)
                {
                    endDayReportUI.badHireNumber++;
                }
                else
                {
                    endDayReportUI.efficientHireNumber++;
                }

            }
            else if (hiredCandidate.GetComponent<FemaleCandidate>())
            {
                FemaleCandidate femaleCandidateStats = hiredCandidate.GetComponent<FemaleCandidate>();

                if (femaleCandidateStats.candidateWorkPosition != hireRulesUI.hiringPositionString ||
                    femaleCandidateStats.candidateExperience < hireRulesUI.hiringExpInt ||
                    femaleCandidateStats.candidateSalaryExpectation > hireRulesUI.hiringSalaryInt ||
                    hiredCandidate.gameObject.layer == FakeCandidateLayer)
                {
                    endDayReportUI.badHireNumber++;
                }
                else
                {
                    endDayReportUI.efficientHireNumber++;
                }
            }
        }
    }
}
