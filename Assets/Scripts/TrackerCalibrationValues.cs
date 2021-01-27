/*
 * Project: rorVRt
 * Author:  Alexander Fischer
 * Date:    27.08.2018
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerCalibrationValues : MonoBehaviour {

	public static TrackerCalibrationValues Instance{ get; private set; }


	public int[] devicesToUse;
	public bool devicesSet;
	public bool isTposeRunning;

	private void Awake(){
		if (Instance == null) {
			Instance = this;
			Instance.devicesSet = false;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
