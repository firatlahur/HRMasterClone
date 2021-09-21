using Core;
using Hire_Rules.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class HireRulesUI : MonoBehaviour
    {
        public HireRules hireRules;
        public GameManager gameManager;
        
        public Text hiringPositionTop, hiringExpTop, hiringSalaryTop;
        public Text hiringPositionBottom, hiringExpBottom, hiringSalaryBottom;

        public Image hiringRulesTop, hiringRulesBottom;

        private void Start()
        {
            hiringPositionTop.text = hireRules.hiringPositions[Random.Range(0, hireRules.hiringPositions.Length)];
            hiringExpTop.text = "+" + Random.Range(hireRules.minExpYears, hireRules.maxExpYears) + " Years";
            hiringSalaryTop.text = Random.Range(hireRules.minSalaryDollars, hireRules.maxSalaryDollars) + "$";

            hiringPositionBottom.text = hiringPositionTop.text;
            hiringExpBottom.text = hiringExpTop.text;
            hiringSalaryBottom.text = hiringSalaryTop.text;
        }

        public void GameStartButton()
        {
            hiringRulesTop.gameObject.SetActive(false);
            hiringRulesBottom.gameObject.SetActive(true);
            gameManager.isGameStarted = true;
        }
    }
}
