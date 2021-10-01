using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Candidate
{
    public class HiredCandidateDetailsPrefab : MonoBehaviour
    {
        public HiredCandidateManagerUI hiredCandidateManagerUI;
        public ScrollViewContentDetailsUI scrollViewContentDetailsUI;
        
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
                else
                {
                    employeeName.color = Color.black;
                }
                employeeImage.sprite = candidatePhotoArray[MalePhoto];
                employeeDepartment.text = hiredCandidate.GetComponent<MaleCandidate>().candidateWorkPosition;
                employeeSalary.text = hiredCandidate.GetComponent<MaleCandidate>().candidateSalaryExpectation + "$";
                employeeExp.text = hiredCandidate.GetComponent<MaleCandidate>().candidateExperience + " Years of Experience";
                

                if (scrollViewContentDetailsUI == null)
                {
                    scrollViewContentDetailsUI = FindObjectOfType<ScrollViewContentDetailsUI>();
                    scrollViewContentDetailsUI.SetMaleCandidateDetailsString(hiredCandidate.GetComponent<MaleCandidate>(), candidatePhotoArray[MalePhoto], employeeName.color);
                }
                else
                {
                    scrollViewContentDetailsUI.SetMaleCandidateDetailsString(hiredCandidate.GetComponent<MaleCandidate>(), candidatePhotoArray[MalePhoto], employeeName.color);
                }

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
                else
                {
                    employeeName.color = Color.black;
                }
                employeeImage.sprite = candidatePhotoArray[FemalePhoto];
                employeeDepartment.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateWorkPosition;
                employeeSalary.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateSalaryExpectation + "$";
                employeeExp.text = hiredCandidate.GetComponent<FemaleCandidate>().candidateExperience + " Years of Experience";
                
                if (scrollViewContentDetailsUI == null)
                {
                    scrollViewContentDetailsUI = FindObjectOfType<ScrollViewContentDetailsUI>();
                    scrollViewContentDetailsUI.SetFemaleCandidateDetailsString(hiredCandidate.GetComponent<FemaleCandidate>(), candidatePhotoArray[FemalePhoto], employeeName.color);
                }
                else
                {
                    scrollViewContentDetailsUI.SetFemaleCandidateDetailsString(hiredCandidate.GetComponent<FemaleCandidate>(), candidatePhotoArray[FemalePhoto], employeeName.color);
                }
                
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
