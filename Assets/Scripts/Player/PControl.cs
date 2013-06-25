using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class PControl : MonoBehaviour {

	CharacterController control;

	Vector3 move;

	float gravityX = 1.15F;
	float accelX   = 5;
	float maxVelX   = 10;
		
	float gravityY = 150;
	bool isJumping = false;
	
	float dTime;

	float maxVelY   = 10;
	float burstVel = 5;
	
	float incVel   = 40;
	bool startJump;
	bool maxJump;


	Object player;

	void Start () {
		control = GetComponent<CharacterController> ();
	}


	void Update () {
		dTime = Time.deltaTime;

		updateMovement();
	}


	//**************************************
	// 		MOVEMENT FUNCTIONS
	//**************************************


	void updateMovement()
	{
		if(checkJump ())	move.x = checkWalk(move.x,gravityX);
		else move.x = checkWalk(move.x,gravityX);

		move = new Vector3(move.x, move.y,0);
		control.Move(move*dTime);
	}

	bool checkJump ()
	{	
		if (!control.isGrounded) {
			move.y -= gravityY*dTime;
		}
		if (control.isGrounded) {
			isJumping =false;
			startJump =false;
			maxJump   =false;
			move.y    =0;
			//jCount    =0;
		}

		if(Input.GetKeyUp("space")&&isJumping)
		{
			maxJump =true;
			//accelY  =100;
			//jCount  =jCountMax;
		}

// JUMP MECHANIC 1
	//	if (Input.GetKey ("space") && jCount<jCountMax) {
	//		move.y = accelY;
	//		if(move.y<1000) accelY *= tempVel;
	//		isJumping=true;
	//		jCount++;
	//		return true;
	//	}

// JUMP MECHANIC 2
		if(Input.GetKey("space"))
		{
			if(!startJump){
				move.y    = burstVel;
				startJump =true;
				isJumping =true;
			}
			else if(move.y<maxVelY&&!maxJump)
			{
				move.y +=incVel;
			}
			else {
				maxJump =true;
			}
			return true;
		}
		else return false;
	}

	float checkWalk(float vX,float gX)
	{
		float sign = 0;

		if(Input.GetAxis("Horizontal")>0) sign=1;
		else if (Input.GetAxis("Horizontal")<0)sign=-1;

		vX +=accelX*sign;
		if(vX>-maxVelX) vX+=0.5F;
		if(vX<maxVelX) vX-=0.5F;

		return vX/gX;
	}

	float signOf(float v)
	{
		if(v>0) return 1;
		else if(v<0) return -1;
		else return 0;
	}
}
