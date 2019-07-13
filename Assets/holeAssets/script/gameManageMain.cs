using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement ;
public class gameManageMain : MonoBehaviour {
	public InputField yourNameText;
	// Use this for initialization
	void Start () {
		this.GetComponent<bestScore> ().showScore ();
		yourNameText.text = "" + PlayerPrefs.GetString ("yourName");
	}
	
	// Update is called once per frame
	public  void btnStart() {
		//Debug.Log (yourNameText.text);
		if (yourNameText.text !="" ) {
			PlayerPrefs.SetString ("yourName", yourNameText.text);
			//Debug.Log (yourNameText.text);
		} else {
			if (PlayerPrefs.HasKey ("yourName") == null) {
				PlayerPrefs.SetString ("yourName", "you");
			}	
		}
		SceneManager.LoadScene ("game");

	}

}
