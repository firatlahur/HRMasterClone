using System;
using System.Collections;
using System.IO;
using Candidate;
using Candidate.ScriptableObjects;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class FireEmployeeButtonUI : MonoBehaviour
    {
        public HiredCandidateDetailsPrefab employeeDetails;
        public EmployeeDetails scpObjSaveData;

        private void Awake()
        {
            employeeDetails = GetComponent<HiredCandidateDetailsPrefab>();
        }
        public void FireEmployee()
        {
            EmployeeDetails();
            SceneManager.LoadScene("FireEmployeeScene");
        }

        private void EmployeeDetails()
        {
            scpObjSaveData.employeeImage = employeeDetails.employeeImage.sprite;
            scpObjSaveData.employeeName = employeeDetails.employeeName.text;
            scpObjSaveData.department = employeeDetails.employeeDepartment.text;
            scpObjSaveData.exp = employeeDetails.employeeExp.text;
            scpObjSaveData.salary = employeeDetails.employeeSalary.text;
        }
    }
}
