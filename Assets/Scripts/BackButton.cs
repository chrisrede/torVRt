/*
 * Project: torVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour {


	void OnTriggerEnter(Collider other){

		if (other.tag == "LeftHand") {
			
			SceneManager.LoadScene("MenuScene");

		}
	}
		
}
