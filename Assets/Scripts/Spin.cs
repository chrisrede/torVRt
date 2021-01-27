/*
 * Project: torVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	public float speed = 0;
	public static bool isInGaze;
	public static bool isRadialFillAmountFull;

	void Start(){
		isInGaze = false;
		isRadialFillAmountFull = false;
	}
	
	void Update () {
		
		transform.Rotate (Vector3.up, speed * Time.deltaTime);

		if (isInGaze && speed < 200 && !isRadialFillAmountFull) {
			speed+= 10;


		} else if(!isInGaze && speed > 0)
			speed-= 10;
		 

	}
}
