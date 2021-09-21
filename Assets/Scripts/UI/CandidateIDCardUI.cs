using System.Collections;
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
        public CandidateInstantiate candidateInstantiate;
        public CandidateManager candidateManager;
        public GameManager gameManager;
        
        public Sprite[] candidatePhotoArray;
        public Text candidateName, candidatePosition, candidateExperience, candidateSalary, fakeIDText;
        public Image candidateInformation, candidatePhoto;

        private const int MaleSprite = 0;
        private const int FemaleSprite = 1;
        
        private GameObject _firstCandidate;
        private Vector2 _candidateInfoDefaultPosition;
        
        private MaleCandidate _maleCandidate;
        private FemaleCandidate _femaleCandidate;

        private bool _male, _female, _fakeCheck, _isDeciding;// _isHired, _isRejected;

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _firstCandidate = candidateInstantiate.allCandidates[0];
            
            _maleCandidate = _firstCandidate.transform.GetComponent<MaleCandidate>();
            _femaleCandidate = _firstCandidate.transform.GetComponent<FemaleCandidate>();

            _male = _maleCandidate;
            _female = _femaleCandidate;
            _candidateInfoDefaultPosition = candidateInformation.rectTransform.position;
        }

        private void Update()
        {
            if (gameManager.inMeeting && !_isDeciding)
            {
                SetCardInformation();
            }

            if (Input.touchCount > 0)
            {
                if (gameManager.inMeeting && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.x > 0)
                    {
                        candidateInformation.transform.Translate(candidateInformation.transform.position * Time.deltaTime,Space.World);
                    }
                    
                    if (Input.GetTouch(0).deltaPosition.x < 0)
                    {
                        candidateInformation.transform.Translate(-candidateInformation.transform.position * Time.deltaTime,Space.World);
                    }
                }
                
                if (_isDeciding && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    bool posCheck = candidateInformation.rectTransform.anchoredPosition.x > -50f;
                    
                    if (candidateInformation.rectTransform.anchoredPosition.x < gameManager.idCardValidDragOffset && posCheck)
                    {
                        candidateInformation.transform.position = _candidateInfoDefaultPosition;
                    }
                    
                    if(candidateInformation.rectTransform.anchoredPosition.x > gameManager.idCardValidDragOffset)
                    {
                        HiredCandidate();
                    }
                    
                    if(candidateInformation.rectTransform.anchoredPosition.x < -gameManager.idCardValidDragOffset && !posCheck)
                    {
                        RejectedCandidate();
                    }
                }
            }
        }
        
        private void SetCardInformation()
        {
            _isDeciding = true;
            if (_isDeciding)
            {
                candidateName.text = _firstCandidate.name;
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
                        fakeIDText.gameObject.SetActive(true);
                    }
                    else
                    {
                        candidatePhoto.sprite = candidatePhotoArray[FemaleSprite];
                    }
                }
                _fakeCheck = true;
            }
        }

        private void HiredCandidate()
        {
            candidateInformation.gameObject.SetActive(false);
            gameManager.inMeeting = false;
            candidateInformation.transform.position = _candidateInfoDefaultPosition;
        }
        
        private void RejectedCandidate()
        {
            candidateInformation.gameObject.SetActive(false);
            gameManager.inMeeting = false;
            candidateInformation.transform.position = _candidateInfoDefaultPosition;
        }
    }
}