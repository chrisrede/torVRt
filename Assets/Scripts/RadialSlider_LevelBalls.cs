/*
 * Project: torVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/Radial Slider")]
    [RequireComponent(typeof(Image))]

    public class RadialSlider_LevelBalls : MonoBehaviour
    {

        public GameObject levelBall;
        public string scene;

        private Image imageOfRadial;
        
        private GameObject sliderObject;
        private float counter;

        private SteamVR_GazeTracker spin;
        
        void Start()
        {
            spin = levelBall.GetComponent<SteamVR_GazeTracker>();
            imageOfRadial = GetComponent<Image>();
            counter = 0;
        }
        
        void Update()
        {
            imageOfRadial.color = Color.Lerp(new Color(1, 1, 1, 0.7f), new Color(0.09f, 1, 0.62f, 0.3f), imageOfRadial.fillAmount);

            if (spin.isInGaze)
                imageOfRadial.fillAmount += Time.deltaTime * 0.35f;

            else if (imageOfRadial.fillAmount < 1 && imageOfRadial.fillAmount > 0)
                imageOfRadial.fillAmount -= Time.deltaTime * 0.2f;

            if (imageOfRadial.fillAmount >= 1)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}