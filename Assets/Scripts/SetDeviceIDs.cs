/*
 * Project: torVRt
 * Author:  Alexander Fischer
 * Date:    27.08.2018
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDeviceIDs : MonoBehaviour {

	// Use this for initialization
	void Awake () {

		if (!(TrackerCalibrationValues.Instance.devicesToUse == null || TrackerCalibrationValues.Instance.devicesToUse.Length == 0)) {
			GameObject.Find ("Left Hand").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[0]);
			GameObject.Find ("Right Hand").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[1]);
			GameObject.Find ("Left Foot").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[2]);
			GameObject.Find ("Right Foot").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[3]);
			GameObject.Find ("Lighthouse1").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[4]);
			GameObject.Find ("Lighthouse2").GetComponent<SteamVR_TrackedObject> ().SetDeviceIndex ((int)TrackerCalibrationValues.Instance.devicesToUse[5]);


		}
	}
	
	void Update () {
		
	}
}
