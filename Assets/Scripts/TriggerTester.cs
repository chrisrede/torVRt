using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTester : MonoBehaviour {

    private SteamVR_TrackedObject trackedObject;

    [SerializeField]
    private bool trigger;
    [SerializeField]
    private bool trackpad;
    [SerializeField]
    private bool grip;
    [SerializeField]
    private bool menu;



    // Use this for initialization
    void Start () {

        trackedObject = GetComponent<SteamVR_TrackedObject>();
    


}
	
	void Update () {
    var device = SteamVR_Controller.Input((int)trackedObject.index);
        device.TriggerHapticPulse(500);
        trigger = device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        grip = device.GetPress(Valve.VR.EVRButtonId.k_EButton_Grip);
        trackpad = device.GetPress(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        menu = device.GetPress(Valve.VR.EVRButtonId.k_EButton_ApplicationMenu);
	}
}
