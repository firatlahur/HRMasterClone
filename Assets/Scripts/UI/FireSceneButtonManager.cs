using System;
using Candidate.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FireSceneButtonManager : MonoBehaviour
    {
        public EmployeeDetails employeeDetails;
        public Text areYouSureText;

        private void Start()
        {
            areYouSureText.text = "Are you sure to fire " + employeeDetails.employeeName + " ?";
        }


        public void FireEmployeeButton()
        {
            Debug.Log("YOU JUST FIRED " + employeeDetails.employeeName);
        }

        public void KeepEmployeeButton()
        {
            Debug.Log("YOU KEEP " + employeeDetails.employeeName);
        }
    }
}
