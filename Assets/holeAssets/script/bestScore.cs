using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
public class bestScore : MonoBehaviour {
	public Text scoreText;
	public Text bestScoreText;
	[HideInInspector ]
	public int  score;
	// Use this for initialization
	public void showScore () {
		if(scoreText)scoreText.text = "" + score;
		int bestScore = PlayerPrefs.GetInt ("bestScore");
		if(bestScoreText)bestScoreText.text = "" + bestScore;

		if(score>bestScore){
			PlayerPrefs.SetInt ("bestScore", score);
		}
	}

}
