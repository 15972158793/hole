using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class name : MonoBehaviour {
	public Transform parentObj;
	public List<Color> colorList = new List<Color>();
	// Use this for initialization
	void Start () {
		this.transform.Find("nameText").gameObject.GetComponent<TextMesh>().color   = colorList [Random.Range (0, colorList.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		if (parentObj) {
			transform.position = new Vector3 (parentObj.position.x, parentObj.position.y, parentObj.position.z-parentObj.localScale.z/2);
		} else {
			Destroy (this.gameObject);
		}	
	}
}
