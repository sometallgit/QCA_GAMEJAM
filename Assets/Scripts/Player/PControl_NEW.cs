using UnityEngine;
using System.Collections;

public class PControl_NEW : MonoBehaviour {

	Rigidbody phys;
	private Camera main;
	
	public Collider butt;
	
	public double camSpeed = 12;
	
	private bool sliding;
	
	private float dTime;
	
	public float xForce = 0;
	public float yForce = 0;
	
	public float xInput;
	public bool yInput;
	public bool maxJump;
	public bool isJumping;
	
	public float xMag = 60;
	public float yMag = 20;
	
	public float yVelCap = 200;
	
	public float gravity = -400;

	void Start () {
		phys = GetComponent<Rigidbody> ();
		main = Camera.mainCamera;
	}


	void FixedUpdate () {
		dTime = Time.deltaTime;
		
		Physics.IgnoreLayerCollision(8,9);
		
		getInput();
		
		updateMove ();
		
		updateJump();
		
		main.transform.position = Vector3.Lerp(main.transform.position,new Vector3(transform.position.x,transform.position.y,main.transform.position.z),(float)camSpeed*dTime);

	}
	

	//**************************************
	// 		MOVEMENT FUNCTIONS
	//**************************************
	
	void updateMove()
	{
		if(isJumping)phys.AddForce(xInput*xMag/2,0,0);
		else phys.AddForce(xInput*xMag,0,0);
		
		if(xInput==0&&!yInput)
		{
			sliding = true;			
		}
	}
	
	void updateJump()
	{	
		if(yInput&&!maxJump)	{
			isJumping=true;
			if(phys.velocity.y<yVelCap)	phys.AddForce(0,yMag,0);
			else maxJump=true;
		}
			phys.AddForce (0,gravity,0);
	}
	
	
	void getInput()
	{
		xInput = Input.GetAxis("Horizontal");
		yInput = Input.GetKey("space");
		if(Input.GetKeyUp("space")) maxJump = true;
	}
	
	//**************************************
	// 		COLLISION FUNCTIONS
	//**************************************
	
	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.name!="PlayerSprite")
		{	
			maxJump=false;
			isJumping=false;
		}
	}

}
