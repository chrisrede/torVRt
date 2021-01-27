//======= Copyright (c) Valve Corporation, All rights reserved. ===============
/*
 * Project: torVRt
 * Authors: Christian Redekop
 * Date:    27.08.2018
 * 
*/

using UnityEngine;
using System.Collections;

public struct GazeEventArgs
{
    public float distance;
}

public delegate void GazeEventHandler(object sender, GazeEventArgs e);

public class SteamVR_GazeTracker : MonoBehaviour
{
    public bool isInGaze = false;
    public event GazeEventHandler GazeOn;
    public event GazeEventHandler GazeOff;
    public float gazeInCutoff = 0.15f;
    public float gazeOutCutoff = 0.4f;

	public SteamVR_TrackedObject hmd;

    // Contains a HMD tracked object that we can use to find the user's gaze
    Transform hmdTrackedObject = null;

	void Start ()
    {
				
	}

    public virtual void OnGazeOn(GazeEventArgs e)
    {
        if (GazeOn != null) GazeOn(this, e);
    }

    public virtual void OnGazeOff(GazeEventArgs e)
    {
        if (GazeOff != null) GazeOff(this, e);
    }
    
	void Update ()
    {
		if(hmd != null) hmdTrackedObject = hmd.transform;

        if (hmdTrackedObject)
        {
            Ray r = new Ray(hmdTrackedObject.position, hmdTrackedObject.forward);
            Plane p = new Plane(hmdTrackedObject.forward, transform.position);

            float enter = 0.0f;
            if (p.Raycast(r, out enter))
            {
                Vector3 intersect = hmdTrackedObject.position + hmdTrackedObject.forward * enter;
                float dist = Vector3.Distance(intersect, transform.position);
                if (dist < gazeInCutoff && !isInGaze)
                {
                    isInGaze = true;
					Spin.isInGaze = true;
                    GazeEventArgs e;
                    e.distance = dist;
                    OnGazeOn(e);
                }
                else if (dist >= gazeOutCutoff && isInGaze)
                {
                    isInGaze = false;
					Spin.isInGaze = false;
                    GazeEventArgs e;
                    e.distance = dist;
                    OnGazeOff(e);
                }
            }

        }

    }
}
