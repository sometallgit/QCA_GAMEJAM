using UnityEngine;
using System.Collections;

public class SandParticleBehaviour : MonoBehaviour 
{
	
	//This is the behaviour script for the sand particles
	
	private Rigidbody phys;
	private float lifeTime;
	public float lifeTimeCap = 5;
	private float creationTime;
	
	// Use this for initialization
	void Start () 
	{
		//Give the particle a random rotation when it is created
		//float randomAngle = Random.Range(0, 360);
		//Vector3 randomRot = new Vector3(0f, 0f, randomAngle);
		//gameObject.transform.localRotation = Quaternion.Euler(randomRot);
		
		phys = GetComponent<Rigidbody>();
		
		creationTime = Time.time;
	}
	
	// Update is called once per frame
	
	void Update () 
	{
		lifeTime = Time.time - creationTime;
		if (phys.isKinematic == true&&lifeTime>lifeTimeCap*1.5)
		//if(phys.isKinematic) return;
		if (lifeTime > lifeTimeCap)
		{
			Destroy(gameObject);
			//phys.Sleep();
			//phys.isKinematic = true;
		}
		if (phys.IsSleeping() && !phys.isKinematic)
		{
			//Debug.Log("check");
			//phys.Sleep();
			phys.isKinematic = true;
			//gameObject.isStatic = true;
		}
		else if (!phys.IsSleeping())
		{
			//phys.Sleep();
			if (phys.angularVelocity.z < phys.sleepAngularVelocity && 
				phys.angularVelocity.z > -phys.sleepAngularVelocity &&
				phys.velocity.x < phys.sleepVelocity && 
				phys.velocity.x > -phys.sleepVelocity &&
				phys.velocity.y < phys.sleepVelocity &&
				phys.velocity.y > -phys.sleepVelocity)
			{
				//Debug.Log("Sleep");
			}
		}
		else
		{
			
		}

	}
	
	public void setNewLifeTime(float newTime)
	{
		lifeTimeCap = newTime;
		creationTime += newTime;
	}
	
	public bool isAsleep()
	{
		return phys.IsSleeping();
	}
	
//	void OnCollisionStay(Collision col)
	//{
		/*
		SandParticleBehaviour s = col.gameObject.GetComponent<SandParticleBehaviour>();
		if (s != null)
		{
			if (s.isAsleep())
			{
				if (s.transform.position.x > transform.position.x)
				{
					Destroy(gameObject);
				}
			}
		}
		*/
	//}
	
}
