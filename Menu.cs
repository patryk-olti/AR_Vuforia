using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene("main");
        }
	}


    public void Start_Game()    {
        SceneManager.LoadScene("main");    }

    public void Authors()    {
        SceneManager.LoadScene("authors");    }

    public void How()    {
        SceneManager.LoadScene("howToPlay");    }

    public void Quit_Game()    {
        Application.Quit();    }
}
