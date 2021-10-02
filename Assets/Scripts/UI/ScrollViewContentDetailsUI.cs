using System;
using System.Collections.Generic;
using Candidate;
using UnityEngine;
using System.IO;
using System.IO.Compression;
using System.Text;
using Core;

namespace UI
{
    [Serializable]
    public class ScrollViewContentDetailsUI : MonoBehaviour
    {
        [HideInInspector] public int childCount;
        [HideInInspector] public float height;
        [HideInInspector] public string json;
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
                json = File.ReadAllText(Application.dataPath + "/EmployeeDetails.json");
                JsonUtility.FromJsonOverwrite(Decompress(json), this);
                SetDetailsOnRestart();
            }
        }

        public void SaveProgress() //save current employee
        {
            json = JsonUtility.ToJson(this);
            string compress = Compress(json);
            File.WriteAllText(Application.dataPath + "/EmployeeDetails.json", compress);
        }

        private void OnApplicationQuit()
        {
            SaveProgress();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if(pauseStatus)
                SaveProgress();
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
            if (savedEmployeesList.Count != 0)
            {
                _rectTransform.sizeDelta = new Vector2(0, height);

                for (int i = 0; i < childCount; i++)
                {
                    GameObject savedEmployee = Instantiate(hiredCandidateDetailsPrefab, transform, false);

                    HiredCandidateDetailsPrefab prefab = savedEmployee.GetComponent<HiredCandidateDetailsPrefab>();

                    prefab.employeeName.text = savedEmployeesList[i].employeeName;
                    prefab.employeeName.color = savedEmployeesList[i].fakeCheckColor;
                    prefab.employeeDepartment.text = savedEmployeesList[i].department;
                    prefab.employeeExp.text = savedEmployeesList[i].experience + " Years of Experience";
                    prefab.employeeSalary.text = savedEmployeesList[i].salary + "$";

                    if (savedEmployee.GetComponent<MaleCandidate>())
                    {
                        prefab.employeeImage.sprite = savedEmployeesList[i].sprite;
                    }
                    else
                    {
                        prefab.employeeImage.sprite = savedEmployeesList[i].sprite;
                    }
                    savedEmployee.gameObject.SetActive(false);
                }
            }
        }

        public static string Compress(string s)
        {
            var bytes = Encoding.Unicode.GetBytes(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }
                return Convert.ToBase64String(mso.ToArray());
            }
        }

        public static string Decompress(string s)
        {
            var bytes = Convert.FromBase64String(s);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }
        }
    }
}
