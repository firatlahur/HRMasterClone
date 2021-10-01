using System.Collections.Generic;
using UnityEngine;

namespace Hire_Rules.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Hire Rules",menuName = "Scriptable Objects/Hire Rules")]
    public class HireRules : ScriptableObject
    {
        public string[] hiringPositions;
        public int minExpYears, maxExpYears, minSalaryDollars, maxSalaryDollars;


    }
}
