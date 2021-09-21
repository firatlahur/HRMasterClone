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
        
        private MaleCandidate _maleCandidate;
        private FemaleCandidate _femaleCandidate;

        private bool _male, _female, _fakeCheck;

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            _firstCandidate = candidateInstantiate.allCandidates[0];
            
            _maleCandidate = _firstCandidate.transform.GetComponent<MaleCandidate>();
            _femaleCandidate = _firstCandidate.transform.GetComponent<FemaleCandidate>();

            _male = _maleCandidate;
            _female = _femaleCandidate;
        }

        private void Update()
        {
            if (gameManager.inMeeting)
            {
                SetCardInformation();
            }
        }

        private void SetCardInformation()
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
    }
}
