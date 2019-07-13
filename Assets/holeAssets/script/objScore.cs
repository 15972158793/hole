using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objScore : MonoBehaviour {
	public int objLevel;
	private Vector3 pos;
	private Vector3 rot;
	// Use this for initialization
	void Start () {
		this.tag="obj";
		this.GetComponent<Rigidbody> ().mass = objLevel*10;
		pos = this.transform.position;
		rot = this.transform.eulerAngles ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other) {
		if (other.tag == "score") {
			if (other.transform.parent.tag == "Player") {
				playerControl pc = other.transform.parent.GetComponent<playerControl> ();
				pc.objLevel = objLevel;
				pc.scoreAdd ();

			} else {
				enemy enemyS = other.transform.parent.GetComponent<enemy> ();
				enemyS.objLevel = objLevel;
				enemyS.scoreAdd ();

			}
			this.tag="Untagged";
			this.GetComponent<Collider> ().enabled = false;
			StartCoroutine (waitDestroy ());
		}
	}
	IEnumerator waitDestroy(){
		yield return new WaitForSeconds (3);
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		yield return new WaitForSeconds (90);
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = false;
		this.transform.position = pos;
		this.transform.position += new Vector3 (0, 1, 0);
		this.transform.eulerAngles = rot;

		this.tag="obj";
		this.GetComponent<Collider> ().enabled = true ;
		this.GetComponent<Collider> ().isTrigger = false;
	}
}
