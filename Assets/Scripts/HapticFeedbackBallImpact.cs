/*
 * Project: torVRt
 * Authors: Ole Aurich, Alexander Fischer
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticFeedbackBallImpact : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnTriggerEnter(Collider other){

		if (other.tag == "SoccerBall") {


		}


	}

}