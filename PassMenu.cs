using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PassMenu : MonoBehaviour {
    private Button Button0;
    private Button Button1;
    private Button Button2;
    private Button Button3;
	// Use this for initialization
	void Start () {
        Button0 = transform.Find("Button0").GetComponent<Button>();
        Button0.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        Button1 = transform.Find("Button1").GetComponent<Button>();
        Button1.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        Button2 = transform.Find("Button2").GetComponent<Button>();
        Button2.onClick.AddListener(() => SceneManager.LoadScene("Start"));
        Button3 = transform.Find("Button3").GetComponent<Button>();
        Button3.onClick.AddListener(() => Application.Quit());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
