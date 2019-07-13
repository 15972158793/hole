using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class existTime : MonoBehaviour {
	public float eTime = 2;
	// Use this for initialization
	void Start () {
		Destroy (gameObject,eTime);
	}
}
