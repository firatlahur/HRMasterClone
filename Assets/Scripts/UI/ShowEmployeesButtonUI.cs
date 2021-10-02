using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShowEmployeesButtonUI : MonoBehaviour
    {
        public GameObject scrollViewContent, scrollView;

        private Mask _scrollViewMask;
        private bool _isActive;

        private void Awake()
        {
            _scrollViewMask = scrollView.GetComponent<Mask>();
            _isActive = false;
        }

        public void EmployeeMenu()
        {
            if (!_isActive)
            {
                _scrollViewMask.showMaskGraphic = true;
                foreach (Transform contents in scrollViewContent.transform)
                {
                    contents.gameObject.SetActive(true);
                }
                _isActive = true;
            }
            else
            {
                _scrollViewMask.showMaskGraphic = false;
                foreach (Transform contents in scrollViewContent.transform)
                {
                    contents.gameObject.SetActive(false);
                }
                _isActive = false;
            }
        }
    }
}
