using UnityEngine;
using UnityEngine.UI;

namespace Candidate.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Employee Details",menuName = "Scriptable Objects/Employee Details")]
    public class EmployeeDetails : ScriptableObject
    {
        public Sprite employeeImage;
        public string employeeName, salary, exp, department;
    }
}
