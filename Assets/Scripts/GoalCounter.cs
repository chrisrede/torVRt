/*
 * Project: torVRt
 * Authors: Ole Aurich, Alexander Fischer, Philipp Bzdok and Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalCounter : MonoBehaviour {

    public int counter = 0;
    public Text countText;


        private void Start()
    {
        countText.text = "Scored: " + counter.ToString();
    }

    void OnTriggerEnter(Collider other){

		if (other.tag == "SoccerBall") {

            counter++;
            countText.text = "Scored: " + counter.ToString();
            //Debug.Log(counter);
		}
	}
	
    public int getCount()
    {
        return counter;
    }
}
