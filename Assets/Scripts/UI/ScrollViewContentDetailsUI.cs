using System;
using System.Collections.Generic;
using Candidate;
using UnityEngine;
using System.IO;
using Core;

namespace UI
{
    [Serializable]
    public class ScrollViewContentDetailsUI : MonoBehaviour
    {
        [HideInInspector] public int childCount;
        [HideInInspector] public float height;
        public GameObject hiredCandidateDetailsPrefab;
        
        public List<SavedEmployees> savedEmployeesList;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            savedEmployeesList = new List<SavedEmployees>();
        }
        
        private void Start() //load saved employee
        {
            if (File.Exists(Application.dataPath + "/EmployeeDetails.json"))
            {
                string json = File.ReadAllText(Application.dataPath + "/EmployeeDetails.json");
                JsonUtility.FromJsonOverwrite(json, this);
                SetDetailsOnRestart();
            }
        }

        private void OnApplicationQuit() //save current employee
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(Application.dataPath + "/EmployeeDetails.json",json);
        }


        private void OnGUI()
        {
            childCount = transform.childCount;
            height = _rectTransform.sizeDelta.y;
        }

        public void SetMaleCandidateDetailsString(MaleCandidate employeeDetails, Sprite employeeSprite, Color nameColor)
        {
            SavedEmployees savedEmployees = new SavedEmployees
            {
                department = employeeDetails.candidateWorkPosition,
                experience = employeeDetails.candidateExperience.ToString(),
                salary = employeeDetails.candidateSalaryExpectation.ToString(),
                employeeName = employeeDetails.name,
                sprite = employeeSprite,
                fakeCheckColor = nameColor
            };
            savedEmployeesList.Add(savedEmployees);
        }

        public void SetFemaleCandidateDetailsString(FemaleCandidate employeeDetails, Sprite employeeSprite, Color nameColor)
        {
            SavedEmployees savedEmployees = new SavedEmployees
            {
                department = employeeDetails.candidateWorkPosition,
                experience = employeeDetails.candidateExperience.ToString(),
                salary = employeeDetails.candidateSalaryExpectation.ToString(),
                employeeName = employeeDetails.name,
                sprite = employeeSprite,
                fakeCheckColor = nameColor
            };
            savedEmployeesList.Add(savedEmployees);
        }

        private void SetDetailsOnRestart()
        {
            if (transform.childCount == 0 && savedEmployeesList[0] != null)
            {
                _rectTransform.sizeDelta = new Vector2(0, height);

                for (int i = 0; i < childCount; i++)
                {
                  GameObject savedEmployee = Instantiate(hiredCandidateDetailsPrefab, transform, false);

                  HiredCandidateDetailsPrefab prefab = savedEmployee.GetComponent<HiredCandidateDetailsPrefab>();

                  prefab.employeeName.text = savedEmployeesList[i].employeeName;
                  prefab.employeeName.color = savedEmployeesList[i].fakeCheckColor;
                  prefab.employeeDepartment.text = savedEmployeesList[i].department;
                  prefab.employeeExp.text = savedEmployeesList[i].experience  + " Years of Experience";
                  prefab.employeeSalary.text = savedEmployeesList[i].salary + "$";

                  if (savedEmployee.GetComponent<MaleCandidate>())
                  {
                      prefab.employeeImage.sprite = savedEmployeesList[i].sprite;
                  }
                  else
                  {
                      prefab.employeeImage.sprite = savedEmployeesList[i].sprite;
                  }
                      
                }
            }
        }
    }
}
