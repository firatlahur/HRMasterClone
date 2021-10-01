using Candidate.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SetFireSceneEmployeeDetails : MonoBehaviour
    {
        public Image employeeImage;
        public Text employeeName, employeeSalary, employeeExp, employeeDepartment;
        public EmployeeDetails employeeDetails;
        
        public void Start()
        {
            employeeImage.sprite = employeeDetails.employeeImage;
            employeeName.text = employeeDetails.employeeName;
            employeeSalary.text = employeeDetails.salary;
            employeeExp.text = employeeDetails.exp;
            employeeDepartment.text = employeeDetails.department;
        }
    }
}
