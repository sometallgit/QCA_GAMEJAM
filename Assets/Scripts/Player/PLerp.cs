using UnityEngine;
using System.Collections;

public class PLerp : MonoBehaviour {
	
	public GameObject pSphere;
	public float lerpSpeed = 10;
	
	// Use this for initialization
	void Start () {
		pSphere = GameObject.Find("Player");
		this.transform.position = new Vector3(pSphere.transform.position.x,pSphere.transform.position.y,this.transform.position.z);
		//renderer.material.color = new Color(1,1,1,0);
	}
	
	// Update is called once per frame
	void Update () {
			this.transform.position = Vector3.Lerp(this.transform.position,new Vector3(pSphere.transform.position.x,pSphere.transform.position.y,this.transform.position.z),Time.deltaTime*lerpSpeed);
	}
}
