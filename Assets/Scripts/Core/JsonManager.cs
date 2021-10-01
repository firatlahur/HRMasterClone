using System.IO;
using UI;
using UnityEngine;

namespace Core
{
    public class JsonManager : MonoBehaviour
    {
        public ScrollViewContentDetailsUI content;
        

        private void Start() //load saved employee
        {
            string json = File.ReadAllText(Application.dataPath + "/EmployeeDetails.json");
        
            JsonUtility.FromJsonOverwrite(json, content);
        }

        // private void OnApplicationQuit() //save current employee
        // {
        //         string json = JsonUtility.ToJson(content);
        //         File.WriteAllText(Application.dataPath + "/EmployeeDetails.json",json);
        //         Debug.Log(json);
        // }
    }
}
