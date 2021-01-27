/*
 * Project: torVRt
 * Authors: Philipp Bzdok
 * Date:27.08.2018
 * 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DroidAnimController : MonoBehaviour {

    public Animator droidAnim;

    void Start () {
        droidAnim = GetComponent<Animator> ();
        droidAnim.Play ("DroidAnimation");
    }

    void FixedUpdate () {
        if (BallTimer.timePerPeriod == 0 && BallTimer.timeLeft > 0) {
            droidAnim.CrossFade ("DroidAnimation", 0.5f, -1, 0);
        }
        if (BallTimer.timeLeft <= 0)
        {
            droidAnim.CrossFade("FreezeAnimation", 5f, -1, 0);
        }
    }
}