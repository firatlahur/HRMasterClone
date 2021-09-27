using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Candidate
{
    public class HiredCandidateDetailsPrefab : MonoBehaviour
    {
        public HiredCandidateManagerUI hiredCandidateManagerUI;
        
        public Image employeeImage;
        public Text employeeName, employeeDepartment, employeeSalary, employeeExp;
        public Sprite[] candidatePhotoArray;

        private const int MalePhoto = 0;
        private const int FemalePhoto = 1;
        private const int FakeCandidateLayer = 6;

        public void SetEmployeeDetails(GameObject hiredCandidate)
        {
            if (hiredCandidate.GetComponent<MaleCandidate>() != null)
            {
                employeeName.text = hiredCandidate.transform.name;
                if (hiredCandidate.gameObject.layer == FakeCandidateLayer)
                {
                    employeeName.color = Color.red;
                }
                employeeImage.sprite = candidatePhotoArray[MalePhoto];
                employeeDepartment.text = hiredCandidate.GetComponent<MaleCandidate>().candidateWorkPosition;
                employeeSalary.text = hiredCandidate.GetComponent<MaleCandidate>().candidateSalaryExpectation.ToString();
                employeeExp.text = hiredCandidate.GetComponent<MaleCandidate>().candidateExperience.ToString();

                if (hiredCandidateManagerUI == null)
                {
                    hiredCandidateManagerUI = FindObjectOfType<HiredCandidateManagerUI>();
                    hiredCandidateManagerUI.ContentSizeManager();
                }
                else
                {
                    hiredCandidateManagerUI.ContentSizeManager();
                }
            }
            else if (hiredCandidate.GetComponent<FemaleCandidate>() != null)
            {
                employeeName.text = hiredCandidate.transform.name;
                if (hiredCandidate.gameObject.layer == FakeCandidateLayer)
                {
                    employeeName.color = Color.red;
                }
                employeeImage.sprite = candidatePhotoArray[FemalePhoto];
                employeeDepartment.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateWorkPosition;
                employeeSalary.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateSalaryExpectation.ToString();
                employeeExp.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateExperience.ToString();
                
                if (hiredCandidateManagerUI == null)
                {
                    hiredCandidateManagerUI = FindObjectOfType<HiredCandidateManagerUI>();
                    hiredCandidateManagerUI.ContentSizeManager();
                }
                else
                {
                    hiredCandidateManagerUI.ContentSizeManager();
                }
            }
        }
    }
}
