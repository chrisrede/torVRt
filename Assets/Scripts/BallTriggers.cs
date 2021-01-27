/*
 * Project: TorVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTriggers : MonoBehaviour {

    public static bool isBallCaught;
    public static bool isNextShot;

    public static bool collisionLeftHand_Ball, collisionRightHand_Ball,
                       collisionLeftFoot_Ball, collisionRightFoot_Ball,
                       collisionHead_Ball,
                       collisionLeftPost_Ball, collisionRightPost_Ball, collisionTopPost_Ball;

    public static string sceneName;

    public static void setAllFalse()
    {
        isBallCaught = false;
        isNextShot = false;

        collisionLeftHand_Ball = false;
        collisionRightHand_Ball = false;
        collisionLeftFoot_Ball = false;
        collisionRightFoot_Ball = false;
        collisionHead_Ball = false;
        collisionLeftPost_Ball = false;
        collisionRightPost_Ball = false;
        collisionTopPost_Ball = false;
    }


}