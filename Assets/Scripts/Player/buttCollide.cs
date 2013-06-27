using UnityEngine;
using System.Collections;

public class buttCollide : MonoBehaviour {
	
	public GameObject player;
	public PControl_NEW pControl;
	// Use this for initialization
	void Start () {
		pControl = player.GetComponent<PControl_NEW>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter (Collision col)
	{
		Debug.Log (true);
		if(col.gameObject.name!="Player"&&col.gameObject.name!="PlayerSprite")
		{	
			pControl.maxJump=false;
			pControl.isJumping=false;
		}
	}
}
