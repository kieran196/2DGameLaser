using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadNextLevel() {
		Application.LoadLevel (Application.loadedLevel + 1);
	}
	
	//Param level name
	public void LoadLevel(string name) { 
		Debug.Log ("Level load requested for: "+name);
		Application.LoadLevel(name);
		//SceneManager.LoadScene (name);
	}

	public void QuitRequest() {
		Debug.Log ("Requesting to exit the game...");
		Application.Quit ();
	}
}
