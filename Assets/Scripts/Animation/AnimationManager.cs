using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour 
{
	public SpriteAnim[] animations; //The array of animations that are configured in the inspector
	public string currentAnimation; //The currently playing animation
	public bool flipTextureVertically = true; //Unity's UV system is flipped to how it's usually used. This means that the textures on planes are flipped vertically
	
	private float frameTime = 0; //The time that the frame has been active for
	private uint currentFrame = 0; 
	private float frameRate = 24.0f;
	private int numFrames; //The number of frames in the current animation
	private float currentTime; //The current time in the execution
	private float nextFrameTime; //The time when the next frame is due
	private float timeAllowance = 0.01f; //The time in seconds that the animation is allowed to deviate by (helps for lower framerates)
	private bool currentAnimIsLooping;
	
	//Call this function to set the animation, don't alter currentAnimation directly
	public void setNewAnimation(string newAnimation = "null")
	{
		//Set the frame back to 0 so the new animation starts from the start
		//This also avoids the risk of going out of bounds if changing to an animation with a lower frame count
		currentFrame = 0;
		
		if (newAnimation != "null")
		{
			currentAnimation = newAnimation;
		}
		else
		{
			Debug.Log("ERROR: No name given in AnimationManager.setNewAnimation()");
		}
	}
	
	// Use this for initialization
	void Start () 
	{
		frameTime = 1.0f / frameRate;
		currentTime = Time.time;
		nextFrameTime = currentTime + frameTime;
		if (flipTextureVertically) flipTex();
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
				//If the animation is looping, set the current frame back to 0
				if (isLooping())
				{
					currentFrame = 0;
				}
				
				//Otherwise, find and play the animation specified in the inspector
				else
				{
					currentFrame = 0;
					for (int i = 0; i < animations.Length; i++)
					{
						if (animations[i].getName() == currentAnimation)
						{
							setNewAnimation(animations[i].getNextAnim());
							break;
						}
					}
				}
			}
		}
		play (currentAnimation);
	}
	
	
	void play (string animName = "null")
	{
		//Abort if a name wasn't given for an animation
		if (animName == "null")
		{
			Debug.Log("ERROR: No name given in AnimationManager.play()");
			return;
		}
		
		//Find the animation with the matching name and assign the current texture to the current frame
		for (int i = 0; i < animations.Length; i++)
		{
			if (animations[i].getName() == animName)
			{
				getAnimData(i);
				gameObject.renderer.material.mainTexture = animations[i].play(currentFrame);
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
	
	void flipTex()
	{
		Vector2 offset = new Vector2(0,1);
		Vector2 scale = new Vector2(1,-1);
		gameObject.renderer.material.SetTextureOffset("_MainTex", offset);
		gameObject.renderer.material.SetTextureScale("_MainTex", scale);
	}
}
