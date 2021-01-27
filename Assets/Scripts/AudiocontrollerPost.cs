/*
 * Project: TorVRt
 * Author: Ole Aurich
 * Date:27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudiocontrollerPost : MonoBehaviour {

	//Random random = new Random();

	int randomNumber;
	public AudioClip PunchPost;

	public AudioSource source;
	void Start ()   
	{
		source = GetComponent <AudioSource> ();

		source.playOnAwake = false;
		source.clip = PunchPost;
	}        

	void OnCollisionEnter (Collision other)  
	{
		if (other.collider.tag == "LeftTracker" || other.collider.tag == "RightTracker" || 
            other.collider.tag == "LeftHand" || other.collider.tag == "RightHand") {

			source.Play ();
		}


	}
}