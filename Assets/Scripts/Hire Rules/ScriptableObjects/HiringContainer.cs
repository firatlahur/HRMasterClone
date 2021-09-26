using UnityEngine;

namespace Hire_Rules.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Job Categories Container",menuName = "Scriptable Objects/Job Categories Container")]
    public class HiringContainer : ScriptableObject
    {
        public HireRules[] jobCategories;
    }
}
