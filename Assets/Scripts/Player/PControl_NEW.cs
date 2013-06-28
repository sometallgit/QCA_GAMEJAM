using UnityEngine;
using System.Collections;

public class PControl_NEW : MonoBehaviour {

	Rigidbody phys;
	private Camera main;
	
	private Vector3 botHit;

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
	public float xVelCap = 200;
	
	public float gravity = -400;

	void Start () {
		phys = GetComponent<Rigidbody> ();
		main = Camera.mainCamera;
		botHit = new Vector3(0,transform.localScale.y/2,0);
	}


	void FixedUpdate () {
		dTime = Time.deltaTime;
		
		Physics.IgnoreLayerCollision(8,9);
		
		getInput();
		
		updateMove ();
		
				Debug.DrawRay(transform.position, botHit);
		updateJump();
		
		main.transform.position = Vector3.Lerp(main.transform.position,new Vector3(transform.position.x,transform.position.y,main.transform.position.z),(float)camSpeed*dTime);

	}
	

	//**************************************
	// 		MOVEMENT FUNCTIONS
	//**************************************
	
	void updateMove()
	{
		Mathf.Clamp(phys.velocity.x,0,xVelCap);
		if(isJumping)phys.AddForce(xInput*xMag/2,0,0);
		else phys.AddForce(xInput*xMag,0,0);
		
		if(xInput==0&&!yInput)
		{
			sliding = true;			
		}
	}
	
	void updateJump()
	{	
		Mathf.Clamp(phys.velocity.y,0,yVelCap);
		if(yInput&&!maxJump)	{
			isJumping=true;
			phys.AddForce(0,yMag,0);
			if(phys.velocity.y>=yVelCap) maxJump=true;
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
		Ray ray = new Ray(transform.position,Vector3.down);
		if(Physics.Raycast(ray,botHit.y))
		{
				maxJump=false;
				isJumping=false;
		}

	}

}
