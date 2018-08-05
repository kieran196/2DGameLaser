using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	public int score = 0;
	private Text myText;

	public void Score(int points) {
		score += points;
		myText.text = score.ToString();
	}

	void Reset() {
		score = 0;
		myText.text = 0.ToString ();
		//score.text = 1.ToString();
	}

	// Use this for initialization
	void Start () {
		myText = this.GetComponent<Text> ();
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		//Reset ();
	}
}
