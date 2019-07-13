using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour {
	[HideInInspector]
	public int[] scoreAddCount;
	[HideInInspector]
	public int[] scoreLevelCount;
	[HideInInspector]
	public float[] scaleNum;
	[HideInInspector]
	public int objLevel;

	[HideInInspector]
	public int score;
	[HideInInspector]
	public string enemyName;
	public float speed;
	private float veclityScale=0.0f;

	private Transform enemyObj;
	private float scaleNumTemp;
	private bool scaleChangeDO;
	[HideInInspector]
	public int scoreLevel;
	private Transform objToGet;
	private float speedTemp;
	private Transform player;
	private bool eatPlayerDO;
	private bool standByDO;
	// Use this for initialization
	void Start () {
		scoreLevel = 0;
		scaleNumTemp = this.transform.localScale.x;
		speed += Random.Range (-speed / 3, speed / 3);
		enemyObj = this .transform;
		speedTemp = speed;
		player = GameObject.FindGameObjectWithTag ("Player").transform ;

		GameObject nameObj = Instantiate (Resources.Load ("name")as GameObject, transform.position, Quaternion.identity);
		nameObj .GetComponent<name> ().parentObj = this.transform;
		nameObj.transform.Find ("nameText").GetComponent<TextMesh> ().text = "" + enemyName;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (scaleChangeDO) {
			scaleNumTemp = Mathf.SmoothDamp (scaleNumTemp, scaleNum [scoreLevel], ref veclityScale, 0.6f);
			enemyObj.localScale = new Vector3 (scaleNumTemp, scaleNumTemp, scaleNumTemp);
			if (Mathf.Abs (scaleNumTemp - scaleNum [scoreLevel]) < 0.1f)
				scaleChangeDO = false;
		}
		if (objToGet == null) {
			//speedTemp = speed;
			if (FindClosestEnemy ())
				objToGet = FindClosestEnemy ().transform;	
		} else {
			if (!standByDO) {
				
				Vector3 pos = new Vector3 (objToGet.position.x, 0, objToGet.position.z);
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (pos - transform.position), 3 * Time.deltaTime);//缓慢转向目标
				transform.Translate(Vector3.forward*speedTemp*Time.deltaTime);

				float dist = Vector3.Distance (transform.position, objToGet.position);
				if ( dist< 6) {
					
					speedTemp = (speed * dist / 6 +0.5f);
					//Debug.Log ("speedown " + speedTemp);
				}
				if (objToGet.tag != "obj")
					objToGet = null;
				
			}

		}			

		if (eatPlayerDO && objToGet!=player ) {
			float distP = Vector3.Distance (transform.position, player.position);
			if (distP < 20) {
				//Debug.Log ("player:" + player.gameObject.GetComponent<playerControl> ().scoreLevel + "  e" + scoreLevel);
				//Debug.Log ("eeee");
				objToGet = player;
				StartCoroutine (waitStopEatPlayer ());

			} 
		}else if(eatPlayerDO&& objToGet==player ){
			float distP = Vector3.Distance (transform.position, player.position);
			if (distP < 20) {
				if (scoreLevel <= player.gameObject.GetComponent<playerControl> ().scoreLevel) {
					eatPlayerDO = false;
					objToGet = null;
				}
			} 
		}
	}
	IEnumerator waitStopEatPlayer(){
		yield return new WaitForSeconds (Random.Range(10,30));

		objToGet = null;
		eatPlayerDO = false;

	}
	private GameObject closest;
	public GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("obj");

		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance && go.GetComponent<objScore>().objLevel <=scoreLevel ) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}
	public void scoreAdd () {		
		score += scoreAddCount[objLevel];

		if (score > scoreLevelCount [scoreLevel]) {
			scoreLevel += 1;
			if (scoreLevel >= scoreLevelCount.Length)
				scoreLevel = scoreLevelCount.Length - 1;
			scaleChangeDO= true;
			if (scoreLevel > player.gameObject.GetComponent<playerControl> ().scoreLevel) {
				eatPlayerDO = true;

			} else {
				eatPlayerDO = false;
			}
		}

	}
	void OnTriggerStay(Collider other) {
		if (other.tag == "obj") {
			if (other.gameObject.GetComponent<objScore> ().objLevel <= scoreLevel) {
				Vector3 pos = new Vector3 (other.transform.position.x, 0, other.transform.position.z);
				float dist = Vector3.Distance (transform.position, pos);
				if (dist < 1f) {
					other.gameObject.GetComponent<Collider> ().isTrigger = true;
					other.tag = "Untagged";
					objToGet = null;

					standByDO = true;
					StartCoroutine (waitSpeed ());
				}
			}
		} else if (other.tag == "enemy") {
			int SL = other.GetComponent<enemy> ().scoreLevel;
			if (scoreLevel > SL) {
				//Debug.Log ("big");
				if (Vector3.Distance (transform.position, other.transform.position)< (transform.localScale.x/2-other.transform.localScale.x/2)) {
					
					Destroy (other.gameObject);
					Instantiate (Resources.Load ("eat_fx")as GameObject, transform.position, Quaternion.identity).transform.SetParent(this.transform);
					score += 5;
				}
			}else if(scoreLevel < SL){
				if (Vector3.Distance (transform.position, other.transform.position)< (other.transform.localScale.x/2-transform.localScale.x/2)) {					
				
					Destroy (this.gameObject);
				}
			}

		}
	}
	IEnumerator waitSpeed(){
		yield return new WaitForSeconds (1);
		standByDO = false;
		speedTemp = speed;
	}
}
