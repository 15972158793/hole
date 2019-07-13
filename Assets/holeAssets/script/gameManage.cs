using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
using UnityEngine.SceneManagement  ;
public class gameManage : MonoBehaviour {	

	public Text scoreText;
	public int[] scoreAddCount;
	public int[] scoreAddEnemyCount;
	public int[] scoreLevelCount;
	public float[] cameraField;
	public float[] scaleNum;

	public Text timeShowText;
	public float timeGame;
	public GameObject gameOverUI;
	public int enemyCount;
	[HideInInspector]
	public string[] enemyName;
	public float createEnemyAgainCD;
	public GameObject timeLastUI;
	private float timeTemp;
	private GameObject player;

	private bool gameOverDO;

	private System.DateTime lastTime;
	private System.DateTime nowTime;
	private System.TimeSpan textTime;
	void enemyNameCheck(){
		enemyName=new string[33];
		enemyName [0] ="Shine";
		enemyName [1] ="Tracy";
		enemyName [2] ="Kobe";
		enemyName [3] ="Lucifer mike";
		enemyName [4] ="tigerman";
		enemyName [5] ="SPIKE";
		enemyName [6] ="Alexander";
		enemyName [7] ="eggegger";
		enemyName [8] ="Jigsaw";
		enemyName [9] ="super boy";
		enemyName [10] ="Hornnie";
		enemyName [11] ="jacquelyn";
		enemyName [12] ="Paul";
		enemyName [13] ="Iverson";
		enemyName [14] ="Tracy";
		enemyName [15] ="Luis";
		enemyName [16] ="THEBALM";
		enemyName [17] ="Cowboy St";
		enemyName [18] ="horizon";
		enemyName [19] ="Chistian";
		enemyName [20] ="Virginia";
		enemyName [21] ="Vaughan";
		enemyName [22] ="Killer Blade";
		enemyName [23] ="Cedric";
		enemyName [24] ="Kevin";
		enemyName [25] ="Mo Maek";
		enemyName [26] ="Conclude";
		enemyName [27] ="Ace Killek";
		enemyName [28] ="Machine gun";
		enemyName [29] ="Favorties";
		enemyName [30] ="Μystery";
		enemyName [31] ="Spherica";
		enemyName [32] ="Saint Soldier";
	}

	// Use this for initialization
	void Start () {
		enemyNameCheck ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerControl pc = player.GetComponent<playerControl> ();
		pc.scoreAddCount = scoreAddCount;
		pc.scoreAddEnemyCount = scoreAddEnemyCount;
		pc.scoreLevelCount = scoreLevelCount;
		pc.cameraField = cameraField;
		pc.scaleNum = scaleNum;

		int ePointCount = GameObject.FindGameObjectsWithTag ("enemyCreatPoint").Length;
		if (enemyCount > ePointCount)
			enemyCount = ePointCount;
		for (int i = 0; i < enemyCount; i++) {
			GameObject[] ePoints = GameObject.FindGameObjectsWithTag ("enemyCreatPoint");
			int eNum = Random.Range (0, ePoints.Length);

			enemy e = Instantiate (Resources.Load ("enemy")as GameObject, ePoints [eNum].transform.position,Quaternion.identity  ).GetComponent<enemy> ();
			e.scoreAddCount = scoreAddCount;
			e.scoreLevelCount = scoreLevelCount;
			e.scaleNum = scaleNum;

			int a = Mathf.FloorToInt (enemyName.Length / enemyCount);
			e.enemyName=enemyName[Random.Range(i*a,i*a+a )];
			ePoints [eNum].tag = "Untagged";
		}

		lastTime = System.DateTime.Now;
		timeTemp = Time.time + createEnemyAgainCD;
	}
	void FixedUpdate(){
		if (gameOverDO)
			return;
		
		if (Time.time > timeTemp) {
			GameObject[] enemys = GameObject.FindGameObjectsWithTag ("enemy");
			if (enemys.Length < 17) {
				GameObject[] ePoints = GameObject.FindGameObjectsWithTag ("enemyCreatPoint");
				int eNum = Random.Range (0, ePoints.Length);
				GameObject enemyObj = Instantiate (Resources.Load ("enemy")as GameObject, ePoints [eNum].transform.position, Quaternion.identity);
				enemy e = enemyObj.GetComponent<enemy> ();
				e.scoreAddCount = scoreAddCount;
				e.scoreLevelCount = scoreLevelCount;
				e.scaleNum = scaleNum;
				e.enemyName=enemyName[Random.Range(0,enemyName.Length )];
				Instantiate (Resources.Load ("dead_fx")as GameObject, enemyObj.transform.position, Quaternion.identity);
			}

			timeTemp = Time.time + createEnemyAgainCD;
		}
		nowTime = lastTime.AddSeconds (timeGame);
		textTime = nowTime - System.DateTime.Now;

		if (textTime.Ticks <= 0) {
			gameOver ();		
		}else {
			timeShowText.text =  textTime.Minutes.ToString() + ':' + textTime.Seconds.ToString() ;
			if (textTime.TotalSeconds <= 11) {
				//Debug.Log ("last");
				timeLastUI.SetActive (true);
				timeLastUI.transform.Find ("timeLastText").GetComponent<Text> ().text = "" + textTime.Seconds.ToString();
			}
		}


	}
	// Update is called once per frame

	public void gameOver(){
		gameOverDO = true;
		gameOverUI.SetActive (true);
		bestScore bs = this.GetComponent <bestScore> ();
		bs.score =player.GetComponent<playerControl> ().score;
		bs.showScore ();
		timeShowText.gameObject.SetActive (false);

		//AdManager.Instance.GameOver();
	}
	public void btnRestart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	public void btnMain(){
		SceneManager.LoadScene ("main");
	}

}
