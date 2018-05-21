using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string name;
    private List<Project> projects;
    private bool checkResult = false;
    public bool CheckResult { get { return checkResult; } set { checkResult = value; } }
    public string Name { get { return name; } set { name = value; } }

    public List<Project> Projects { get { return projects; } set { projects = value; } }

    public User(string Name, bool Result)
    {
        name = Name;
        checkResult = Result;
    }
}
public class Project
{
    private string name;
    public string Name { get { return name; } set { name = value; } }
    public Project(string Name)
    {
        name = Name;
    }
}
public class MainCameraScript : MonoBehaviour {
    public static int maxWidth = Screen.width;
    public static int maxHeight = Screen.height;
    public static Rect WindowRect = new Rect(0, 150, maxWidth, maxHeight / 3);
    public bool isWindowShow;
    private bool checkResult = false;
    private string jsonData;
    private JSONNode jsonNode;
    private List<MyUsers> users = new List<MyUsers>();
    GUIStyle myStyle;
    // Use this for initialization
    IEnumerator Start () {
        string url = "http://llblanca.com.ua/getAllUsers2";
        //string url = "https://ssamyraika.000webhostapp.com/my/json";
        WWW www = new WWW(url);
        yield return www;

        // store text in www to json string
        if (string.IsNullOrEmpty(www.error))
        {
            // store www.text in a string type variable
            jsonData = www.text;
        }
        jsonNode = SimpleJSON.JSON.Parse(jsonData);
        for(int i = 0; i < jsonNode.Count; i++)
        {
            users.Add(JsonUtility.FromJson<MyUsers>(jsonNode[i].ToString()));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnGUI()
    {
        myStyle = new GUIStyle();
        myStyle.fontSize = 20;
        drawWindowContent();
    }
    private void drawWindowContent()
    {
        int x = 10;
        int y = 10;
        GUI.BeginGroup(new Rect(0, 0, 250, 250));
        for (int i = 0; i < users.Count; i++)//users1.Count
        {
            users[i].CheckResult = GUI.Toggle(new Rect(x, y, 20, 20), users[i].CheckResult, ""); //radiobatton
            GUI.Label(new Rect(x + 50, y, 100, 25), users[i].User, myStyle);
            y += 30;
        }
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].CheckResult)
            {
                WindowRect = GUI.Window(0, WindowRect, drawWindowContent1, "Проекты:", myStyle);
            }
        }
       
        GUI.EndGroup();
        
    }
    private void drawWindowContent1(int idWindow)
    {

        int x = 10;
        int y = 30;       
        for (int i = 0; i < users.Count; i++)
        {
            if (users[i].CheckResult)
            {
                GUI.Label(new Rect(x + 20, y, 100, 25), users[i].Project, myStyle);
                y += 30;
            }
        }
    }

}
