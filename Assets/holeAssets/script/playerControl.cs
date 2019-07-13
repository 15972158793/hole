using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;
public class playerControl : MonoBehaviour {

	[HideInInspector]
	public int[] scoreAddCount;
	[HideInInspector]
	public int[] scoreAddEnemyCount;
	[HideInInspector]
	public int[] scoreLevelCount;
	[HideInInspector]
	public float[] cameraField;
	[HideInInspector]
	public float[] scaleNum;
	[HideInInspector]
	public int objLevel;

	private  Text scoreAddText;
	[HideInInspector]
	public  int score;
	private float veclity=0.0f;
	private float veclityScale=0.0f;
	private Camera camera;
	private Transform player;
	private float scaleNumTemp;
	private bool cameraChangeDO;
	private bool scaleChangeDO;
	[HideInInspector]
	public  int scoreLevel;
	private bool gameOverDO;
	private soundResources sr;
	// Use this for initialization
	void Start () {


		scaleNumTemp = this.transform.localScale.x;
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
		player = this .transform;
		scoreAddText = GameObject.FindGameObjectWithTag ("scoreAddText").GetComponent<Text> ();
		scoreAddText.gameObject.SetActive (false);

		GameObject nameObj = Instantiate (Resources.Load ("name")as GameObject, transform.position, Quaternion.identity);
		nameObj .GetComponent<name> ().parentObj = this.transform;
		nameObj.transform.Find ("nameText").GetComponent<TextMesh> ().text = "" + PlayerPrefs.GetString ("yourName");
		sr = GameObject.FindGameObjectWithTag ("gameManage").GetComponent<soundResources > ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cameraChangeDO) {
			camera.fieldOfView = Mathf.SmoothDamp (camera.fieldOfView, cameraField [scoreLevel], ref veclity, 1f);
			if (Mathf.Abs (camera.fieldOfView - cameraField [scoreLevel]) < 0.1f)
				cameraChangeDO = false;
		}
			

		if (scaleChangeDO) {
			scaleNumTemp = Mathf.SmoothDamp (scaleNumTemp, scaleNum [scoreLevel], ref veclityScale, 0.6f);
			player.localScale = new Vector3 (scaleNumTemp, scaleNumTemp, scaleNumTemp);
			if (Mathf.Abs (scaleNumTemp - scaleNum [scoreLevel]) < 0.1f)
				scaleChangeDO = false;
		}
	}
	void FixedUpdate(){
		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	}
	public void scoreAdd () {		
		score += scoreAddCount[objLevel];
		StartCoroutine (waitScoreAddText ());
		scoreAddPlay ();

		sr.play ("Coin");

	}
	private int scoreLevelEnemy;
	public  void scoreAddEnemy () {		
		score += scoreAddEnemyCount[scoreLevelEnemy];
		StartCoroutine (waitScoreAddEnemyText ());
		scoreAddPlay ();
	}
	void scoreAddPlay(){
		GameObject.FindGameObjectWithTag ("gameManage").GetComponent<gameManage> ().scoreText.text  = "Score:" + score;
		if (score > scoreLevelCount [scoreLevel]) {
			scoreLevel += 1;
			if (scoreLevel >= scoreLevelCount.Length)
				scoreLevel = scoreLevelCount.Length - 1;

			cameraChangeDO = true;
			scaleChangeDO= true;

		}
	}
	IEnumerator waitScoreAddText(){
		Text  scoreAT= Instantiate (scoreAddText,scoreAddText.rectTransform)as Text ;
		scoreAT.transform.SetParent (scoreAddText.transform.parent );
		scoreAT.gameObject .SetActive (true);
		scoreAT.text = "+" + scoreAddCount [objLevel];
		yield return new WaitForSeconds (2);
		Destroy (scoreAT.gameObject);
	}
	IEnumerator waitScoreAddEnemyText(){
		Text  scoreAT= Instantiate (scoreAddText,scoreAddText.rectTransform)as Text ;
		scoreAT.transform.SetParent (scoreAddText.transform.parent );
		scoreAT.gameObject .SetActive (true);
		scoreAT.text = "+" + scoreAddEnemyCount [scoreLevelEnemy];
		yield return new WaitForSeconds (2);
		Destroy (scoreAT.gameObject);
	}
	void OnTriggerStay(Collider other) {
		if (gameOverDO)
			return;
		if (other.tag == "enemy") {
			
			int SL = other.GetComponent<enemy> ().scoreLevel;
			if (scoreLevel > SL) {
				//Debug.Log ("big");
				if (Vector3.Distance (transform.position, other.transform.position)< (transform.localScale.x/2-other.transform.localScale.x/2)) {
					scoreLevelEnemy = SL;
					scoreAddEnemy ();
					Destroy (other.gameObject);
					Instantiate (Resources.Load ("eat_fx")as GameObject, transform.position, Quaternion.identity).transform.SetParent(this.transform);

					sr.play ("kill");
				}
			}else if(scoreLevel < SL){
				if (Vector3.Distance (transform.position, other.transform.position)< (other.transform.localScale.x/2-transform.localScale.x/2)) {
					GameObject.FindGameObjectWithTag ("gameManage").GetComponent<gameManage> ().gameOver ();
					Instantiate (Resources.Load ("dead_fx")as GameObject, transform.position, Quaternion.identity);
					gameOverDO = true;

					sr.play ("kill");
				}
			}

		}
	}
}
