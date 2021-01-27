/*
 * Project: torVRt
 * Authors: Alexander Fischer, Ole Aurich, Philipp Bzdok and Christian Redekop
 * Date:    27.08.2018
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Controller_binding : MonoBehaviour
{

    private Vector4[] groupFeet, groupHands, groupLightHouses;
    private List<Vector4> devices;
    private Vector4 D1, D2, D3, D4, D5, D6;

    private GameObject lighthouse1, lightHouse2, leftHand, rightHand, leftFoot, rightFoot;
    private bool startTposeRoutine;

    // calibrationStatus is used in RadialSlider2.cs, integrated in SliderCanvas > Radial Slider > Slider  
    public int calibrationStatus;

    public float time;
    GameObject slider;

    public Text progressText;
    public Image progressImage;

    void Start()
    {

        calibrationStatus = 0;
        time = 6;

        lighthouse1 = GameObject.Find("Lighthouse1");
        lightHouse2 = GameObject.Find("Lighthouse2");
        leftHand = GameObject.Find("Left Hand");
        rightHand = GameObject.Find("Right Hand");
        leftFoot = GameObject.Find("Left Foot");
        rightFoot = GameObject.Find("Right Foot");

    }

    void Update()
    {

        if (startTposeRoutine)
        {

            progressImage.color = new Color(0, 0, 0, 0.7f);
            time -= Time.deltaTime;
            progressText.text = "Please stand upright! \nStarting in " + (int)time;

            if (time <= 0)
            {
                progressText.text = "";
                progressImage.color = new Color(0, 0, 0, 0);
                startTposeRoutine = false;
                tposeroutine();
            }
        }
        else
        {
            time = 6;
        }

    }

    public void tposeroutine()
    {

        calibrationStatus = 0;
        devices = new List<Vector4>();
        Debug.Log("tposeRoutine Starting!");

        //Getting Coordinates of the Trackers
        D1 = new Vector4(lighthouse1.transform.position.x, lighthouse1.transform.position.y, lighthouse1.transform.position.z, 1);
        D2 = new Vector4(lightHouse2.transform.position.x, lightHouse2.transform.position.y, lightHouse2.transform.position.z, 2);
        D3 = new Vector4(leftHand.transform.position.x, leftHand.transform.position.y, leftHand.transform.position.z, 3);
        D4 = new Vector4(rightHand.transform.position.x, rightHand.transform.position.y, rightHand.transform.position.z, 4);
        D5 = new Vector4(leftFoot.transform.position.x, leftFoot.transform.position.y, leftFoot.transform.position.z, 5);
        D6 = new Vector4(rightFoot.transform.position.x, rightFoot.transform.position.y, rightFoot.transform.position.z, 6);

        devices.Add(D1);
        devices.Add(D2);
        devices.Add(D3);
        devices.Add(D4);
        devices.Add(D5);
        devices.Add(D6);

        groupFeet = new Vector4[2];
        groupHands = new Vector4[2];
        groupLightHouses = new Vector4[2];

        int group1Counter = 2;
        int group2Counter = 2;
        int group3Counter = 2;

        devices.Sort((v1, v2) => v1.y.CompareTo(v2.y));

        for (int i = 0; i < devices.Count; i++)
            Debug.Log("Device " + devices[i].w + ": " + devices[i].y);

        groupFeet[0] = devices[0];
        groupFeet[1] = devices[1];

        groupHands[0] = devices[2];
        groupHands[1] = devices[3];

        groupLightHouses[0] = devices[4];
        groupLightHouses[1] = devices[5];

        Debug.Log("group1Counter has found: " + group1Counter);
        Debug.Log("group2Counter has found: " + group2Counter);
        Debug.Log("group3Counter has found: " + group3Counter);

        if (group1Counter == 2 && group2Counter == 2 && group3Counter == 2)
        {
            Debug.Log("Zwei Pro Gruppe erkannt!");
            Debug.Log("Gruppe1:" + groupFeet[0].w + "und" + groupFeet[1].w);
            Debug.Log("Gruppe2:" + groupHands[0].w + "und" + groupHands[1].w);

            if (groupFeet[0].x > groupFeet[1].x)
            {
                GameObject.Find("Left Foot").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupFeet[0].w);
                GameObject.Find("Right Foot").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupFeet[1].w);

                TrackerCalibrationValues.Instance.devicesToUse = new int[6];
                TrackerCalibrationValues.Instance.devicesToUse[2] = (int)groupFeet[0].w;
                TrackerCalibrationValues.Instance.devicesToUse[3] = (int)groupFeet[1].w;

            }
            else
            {
                GameObject.Find("Left Foot").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupFeet[1].w);
                GameObject.Find("Right Foot").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupFeet[0].w);
                TrackerCalibrationValues.Instance.devicesToUse = new int[6];
                TrackerCalibrationValues.Instance.devicesToUse[2] = (int)groupFeet[1].w;
                TrackerCalibrationValues.Instance.devicesToUse[3] = (int)groupFeet[0].w;
            }

            if (groupHands[0].x > groupHands[1].x)
            {
                GameObject.Find("Left Hand").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupHands[0].w);
                GameObject.Find("Right Hand").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupHands[1].w);
                TrackerCalibrationValues.Instance.devicesToUse[0] = (int)groupHands[0].w;
                TrackerCalibrationValues.Instance.devicesToUse[1] = (int)groupHands[1].w;

            }
            else
            {
                GameObject.Find("Left Hand").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupHands[1].w);
                GameObject.Find("Right Hand").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupHands[0].w);
                TrackerCalibrationValues.Instance.devicesToUse[0] = (int)groupHands[1].w;
                TrackerCalibrationValues.Instance.devicesToUse[1] = (int)groupHands[0].w;
            }



            if (groupLightHouses[0].x > groupLightHouses[1].x)
            {
                GameObject.Find("Lighthouse1").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupLightHouses[0].w);
                GameObject.Find("Lighthouse2").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupLightHouses[1].w);
                TrackerCalibrationValues.Instance.devicesToUse[4] = (int)groupLightHouses[0].w;
                TrackerCalibrationValues.Instance.devicesToUse[5] = (int)groupLightHouses[1].w;

            }
            else
            {
                GameObject.Find("Lighthouse1").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupLightHouses[1].w);
                GameObject.Find("Lighthouse2").GetComponent<SteamVR_TrackedObject>().SetDeviceIndex((int)groupLightHouses[0].w);
                TrackerCalibrationValues.Instance.devicesToUse[4] = (int)groupLightHouses[1].w;
                TrackerCalibrationValues.Instance.devicesToUse[5] = (int)groupLightHouses[0].w;
            }

            Debug.Log("Changed DeviceIDs");

            calibrationStatus = 1;
        }

        else
        {
            calibrationStatus = -1;

        }

    }

    public void setStartTposeRoutine(bool boolean)
    {
        startTposeRoutine = boolean;
        Debug.Log("From setter:" + startTposeRoutine);
    }
}