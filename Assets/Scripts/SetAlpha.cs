using UnityEngine;
using System.Collections;

public class SetAlpha : MonoBehaviour {
	
	public float alpha = 0;

	// Use this for initialization
	void Start () {
			renderer.material.color = new Color(1,1,1,alpha);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
