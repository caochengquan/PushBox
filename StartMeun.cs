using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMeun : MonoBehaviour {
    private Button newgame;
    private Button CountinuGame;
    private Button ExitGame;
	// Use this for initialization
	void Start () {
        newgame = transform.Find("NewGame").GetComponent<Button>();
        newgame.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        CountinuGame = transform.Find("CountinuGame").GetComponent<Button>();
        CountinuGame.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        ExitGame = transform.Find("ExitGame").GetComponent<Button>();
        ExitGame.onClick.AddListener(() => Application.Quit());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void NewGame()
    //{

    //}

    //public void ContinueGame()
    //{

    //}

    //public void ExitGame()
    //{

    //}
}
