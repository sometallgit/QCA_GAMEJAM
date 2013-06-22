using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour 
{
	public SpriteAnim[] animations; //The array of animations that are configured in the inspector
	public string currentAnimation;
	
	private float frameTime = 0; //The time that the frame has been active for
	private uint currentFrame = 0; 
	private float frameRate = 24.0f;
	private int numFrames;
	private int frameSkip;
	private float currentTime;
	private float nextFrameTime;
	private float timeAllowance = 0.01f;
	private bool currentAnimIsLooping;
	
	// Use this for initialization
	void Start () 
	{
		frameTime = 1.0f / frameRate;
		currentTime = Time.time;
		nextFrameTime = currentTime + frameTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Advance the currentframe
		currentTime = Time.time;
		if (currentTime > (nextFrameTime - timeAllowance))
		{
			currentFrame++;
			nextFrameTime = currentTime + frameTime;
			if (currentFrame > numFrames)
			{
				if (isLooping())
				{
					currentFrame = 0;
				}
				
				else
				{
					currentFrame = 0;
					for (int i = 0; i < animations.Length; i++)
					{
						if (animations[i].getName() == currentAnimation)
						{
							currentAnimation =  animations[i].getNextAnim();
						}
					}
				}
			}
			//frameSkip= 0;
		}
		/*
		else
		{
			frameSkip++;
		}
		*/
		play (currentAnimation);
	}
	
	
	void play (string animName = "null", bool loop = false)
	{
		//Abort if a name wasn't given for an animation
		if (animName == "null")
		{
			Debug.Log("ERROR: No name given in AnimationManager.play()");
			return;
		}

		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].getName() == animName)
			{
				getAnimData(i);
				gameObject.renderer.material.mainTexture = animations[i].play(currentFrame);
				
				//Debug.Log(("animation"));
			}
		}
	}
	
	void getAnimData(int index)
	{
		frameRate = animations[index].getFrameRate();
		numFrames = animations[index].getNumFrames();
		frameTime = 1.0f / frameRate;
	}
	
	bool isLooping()
	{
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].getName() == currentAnimation)
			{
				return animations[i].loop;
			}
		}
		Debug.Log("ERROR: isLooping() could not find currentAnimation (check if  'Next Anim If No Loop is valid')");
		return false;
	}
	

}
