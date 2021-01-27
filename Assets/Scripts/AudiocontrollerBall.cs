/*
 * Project: TorVRt
 * Author: Ole Aurich
 * Date:27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudiocontrollerBall : MonoBehaviour {

	//Random random = new Random();
	public Rigidbody rb;
	Vector3 v3Velocity;
	float x,y,z;

	int randomNumber;
	public AudioClip HitFloor1;
	public AudioClip HitPost;
	public AudioClip HitGlove1;
	public AudioClip HitGlove2;
	public AudioClip HitGlove3;


	public AudioClip HitFloor2;
	AudioSource source;
	void Start ()   
	{
		source = GetComponent <AudioSource> ();
        
		source.playOnAwake = false;
		source.clip = HitFloor1;
	} 

	void Update (){
		v3Velocity  = rb.velocity; 

	}

	void OnCollisionEnter (Collision other)  
	{
		
		randomNumber = UnityEngine.Random.Range(0,100);
		x = v3Velocity.x;
		y = -v3Velocity.y/5;
		z = v3Velocity.z;


		if (y > 1)
			y = 1;

		y = y * y;
		


		if (other.collider.tag == "TopPost" || other.collider.tag == "LeftPost" || other.collider.tag == "RightPost")
        {
			source.volume = 1;
			source.Play ();
		}

		if (other.collider.tag == "LeftTracker" || other.collider.tag == "RightTracker" ||
            other.collider.tag == "LeftHand" || other.collider.tag == "RightHand")
        {
			source.volume = 1;
			if (randomNumber > 66) {
				source.clip = HitGlove1;
			} else if (randomNumber > 33) {
				source.clip = HitGlove2;
			} else {
				source.clip = HitGlove3;
			}

			GetComponent<AudioSource> ().Play ();

		}

		if (other.collider.tag == "Ground") {
			source.volume = y;

			if (randomNumber > 50) {
				source.clip = HitFloor1;

			} else {
				source.clip = HitFloor2;
			}
			source.Play ();
		}

	}
}