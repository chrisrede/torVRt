/*
 * Project: torVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinLevelBalls : MonoBehaviour {

	public float speed = 0;
	public SteamVR_GazeTracker gaze;
	public bool isRadialFillAmountFull;

	void Start(){
        gaze = GetComponent<SteamVR_GazeTracker>();
		isRadialFillAmountFull = false;
	}
	
	void Update () {
		
		transform.Rotate (Vector3.up, speed * Time.deltaTime);

		if (gaze.isInGaze && speed < 200 && !isRadialFillAmountFull) 
			speed+= 10;

        else if(speed > 0) speed-= 10;
		 
	}
}