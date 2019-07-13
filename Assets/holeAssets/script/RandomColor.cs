using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

	public Light light;

	public List<Color> colorList = new List<Color>();

	// Use this for initialization
	void Start () {

		light.color  = colorList [Random.Range (0, colorList.Count)];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
