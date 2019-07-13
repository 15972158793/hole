using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRay : MonoBehaviour {
	private GameObject hitObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Ray ray = Camera.main.ScreenPointToRay(this.transform.position );
		Ray ray=new Ray(this.transform.position,this.transform.forward);
		RaycastHit hitInfo;
		if(Physics.Raycast(ray,out hitInfo))
		{       
			if (hitInfo.transform.tag == "obj") {
				if (hitObj != hitInfo.transform.gameObject) {
					if (hitObj) {
						//前一个半透明模型恢复
						Texture tex = hitObj.GetComponent<MeshRenderer> ().material.mainTexture;
						Material m = hitObj.GetComponent<MeshRenderer> ().material;
						m. CopyPropertiesFromMaterial ( Resources.Load ("toumingOff")as Material);
						m.mainTexture = tex;
					}
					//更换要半透明的模型
					hitObj = hitInfo.transform.gameObject;
					//Texture tex2 = hitObj.GetComponent<Renderer> ().material.mainTexture;
					//Material material2 = new Material (Shader.Find ("Transparent/Diffuse"));
					//material2.color = new Color (1.0f, 1.0f, 1.0f, 0.3f);
					//material2.mainTexture = tex2;
					//hitObj.GetComponent<Renderer> ().material = material2;
					Texture tex2 = hitObj.GetComponent<MeshRenderer> ().material.mainTexture;
					Material m2 = hitObj.GetComponent<MeshRenderer> ().material;
					m2.CopyPropertiesFromMaterial ( Resources.Load ("touming")as Material);
					m2.mainTexture = tex2;
				}

			} else {
				if (hitObj) {
					//前一个半透明模型恢复
					Texture tex = hitObj.GetComponent<MeshRenderer > ().material.mainTexture;
					Material m = hitObj.GetComponent<MeshRenderer> ().material;
					m . CopyPropertiesFromMaterial ( Resources.Load ("toumingOff")as Material);
					m.mainTexture = tex;
					hitObj = null;
				}
			}      
		}
	}

}
