/*
 * Project: torVRt
 * Authors: Alexander Fischer and Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI.Extensions
{
	[AddComponentMenu ("UI/Extensions/Radial Slider")]
	[RequireComponent (typeof(Image))]
	public class RadialSlider2 : MonoBehaviour
	{
		private Image imageOfRadial;
		private Controller_binding ctrlBinder;
		private GameObject threeBallsParent;
		private GameObject bigBall;
		private GameObject sliderObject;
		private float counter;
		public Text menuText;
        
		void Start ()
		{
            imageOfRadial = GetComponent<Image>();

            TrackerCalibrationValues.Instance.isTposeRunning = false;
			
			threeBallsParent = GameObject.Find ("BallParent");
            threeBallsParent.SetActive(false);

            bigBall = GameObject.Find("BigBall");
			ctrlBinder = GameObject.Find ("MenuBallAndCtrlScript").GetComponent<Controller_binding> ();

            counter = 0f;

            if (TrackerCalibrationValues.Instance.devicesSet)
				afterTpose ();
			
		}
		
		void Update ()
		{

			if (!TrackerCalibrationValues.Instance.devicesSet) {
                
				imageOfRadial.color = Color.Lerp (new Color (1, 1, 1, 0.7f), new Color (0.09f, 1, 0.62f, 0.3f), imageOfRadial.fillAmount);

				if (Spin.isInGaze)
					imageOfRadial.fillAmount += Time.deltaTime * 0.35f;

				else if (imageOfRadial.fillAmount < 1 && imageOfRadial.fillAmount > 0)
					imageOfRadial.fillAmount -= Time.deltaTime * 0.2f;

				if (imageOfRadial.fillAmount >= 1) {
					Spin.isRadialFillAmountFull = true;
					if (!TrackerCalibrationValues.Instance.isTposeRunning) {
						ctrlBinder.setStartTposeRoutine (true);
						menuText.text = "Please stand upright an look straight forward";
						TrackerCalibrationValues.Instance.isTposeRunning = true;
					}

				}
			}

			if (ctrlBinder.calibrationStatus == -1) 
				unsuccesful ();
			
            else if (ctrlBinder.calibrationStatus == 1) 
				afterTpose ();
			
			ctrlBinder.calibrationStatus = 0;
		}

	

		private void unsuccesful ()
		{
			ctrlBinder.time = 6;
			ctrlBinder.setStartTposeRoutine (true);
			menuText.text = "Something did not work. Please stand upright again!";
		}

		public void afterTpose ()
		{
			TrackerCalibrationValues.Instance.devicesSet = true;

            threeBallsParent.SetActive (true);
            bigBall.SetActive(false);

            menuText.text = "Look at one of the five balls in front of you";

            ctrlBinder.calibrationStatus = 0;
            counter += 0.01f;

            imageOfRadial.color = new Color (255, 255, 255, 0f);			
		}
	}
}
