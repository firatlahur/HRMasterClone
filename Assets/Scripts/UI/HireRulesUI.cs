using Core;
using Hire_Rules.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class HireRulesUI : MonoBehaviour
    {
        public HiringContainer hiringContainer;
        public GameManager gameManager;
        
        public Text hiringPosition, hiringExp, hiringSalary;
        public Text hiringPositionBottom, hiringExpBottom, hiringSalaryBottom;

        public Image hiringRulesTop, hiringRulesBottom;

       [HideInInspector] public string hiringPositionString;
       [HideInInspector] public int hiringSalaryInt, hiringExpInt;

        private void Start()
        {
            hiringPositionString = hiringContainer.jobCategories[gameManager.selectedJobCategory]
                .hiringPositions[
                    Random.Range(0, hiringContainer.jobCategories[gameManager.selectedJobCategory].hiringPositions.Length)];

            hiringSalaryInt = Random.Range(hiringContainer.jobCategories[gameManager.selectedJobCategory].minSalaryDollars,
                hiringContainer.jobCategories[gameManager.selectedJobCategory].maxSalaryDollars);

            hiringExpInt = Random.Range(hiringContainer.jobCategories[gameManager.selectedJobCategory].minExpYears,
                hiringContainer.jobCategories[gameManager.selectedJobCategory].maxExpYears);
            
             hiringPosition.text = hiringPositionString;
             hiringExp.text = "+" + hiringExpInt + " Years";
             hiringSalary.text = hiringSalaryInt + "$";
            
             hiringPositionBottom.text = hiringPosition.text;
             hiringExpBottom.text = hiringExp.text;
             hiringSalaryBottom.text = hiringSalary.text;
        }

        public void GameStartButton()
        {
            hiringRulesTop.gameObject.SetActive(false);
            hiringRulesBottom.gameObject.SetActive(true);
            gameManager.isGameStarted = true;
        }
    }
}
