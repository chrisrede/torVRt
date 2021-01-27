/*
 * Project: torVRt
 * Author:  Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Timers;
using System;

public interface ICloneable<T>
{
    T Clone();
}


public class SessionDataToJson : MonoBehaviour { 

	private Session session;
	private Shot shot;
	private Frame frame;

    private bool isDataSaved;

	private string currentTime;
    private string startTime;
    
    private int tmpCounter;

	private GameObject leftHand, rightHand, leftFoot, rightFoot, head, ball;

	void Start () {

		session	 = new Session();
		shot	 = new Shot ();
		frame	 = new Frame ();

        isDataSaved = false;

		tmpCounter = 0;

		leftHand	 = GameObject.Find ("Left Hand");
		rightHand	 = GameObject.Find ("Right Hand");
		leftFoot	 = GameObject.Find ("Left Foot");
		rightFoot	 = GameObject.Find ("Right Foot");
		head		 = GameObject.FindGameObjectWithTag ("MainCamera");
		ball		 = GameObject.Find ("SOCCER_Ball");

		session.sessionID = 1;
		session.sessionName = "Session start: " + System.DateTime.Now;

		frame.ballStartPos = ball.transform.position;
        
		startTime = System.DateTime.Now.ToString();
        startTime = startTime.Replace("/", "-");
        startTime = startTime.Replace(" ", "-");
        startTime = startTime.Replace(":", "-");

    }

	void Update () {

		tmpCounter++;

		currentTime = System.DateTime.Now.ToString();
        
		frame.leftHandCoords	 = leftHand.transform.position;
		frame.rightHandCoords	 = rightHand.transform.position;
		frame.leftFootCoords	 = leftFoot.transform.position;
		frame.rightFootCoords	 = rightFoot.transform.position;
        frame.headCoords         = head.transform.position;          
		frame.ballCoords		 = ball.transform.position;

        frame.leftHandRot        = leftHand.transform.rotation;
        frame.rightHandRot       = rightHand.transform.rotation;
        frame.leftFootRot        = leftFoot.transform.rotation;
        frame.rightFootRot       = rightFoot.transform.rotation;
        frame.headRot            = head.transform.rotation;


        frame.frameID = ++frame.frameID;
		frame.timeStampFrame = currentTime;

		shot.framesList.Add (frame.Clone());
        


		if (BallTriggers.isNextShot)
        {
            shot.isBallCaught = BallTriggers.isBallCaught;

            shot.collisionTopPost_Ball = BallTriggers.collisionTopPost_Ball;
            shot.collisionLeftPost_Ball = BallTriggers.collisionLeftPost_Ball;
            shot.collisionRightPost_Ball = BallTriggers.collisionRightPost_Ball;

            shot.collisionLeftHand_Ball = BallTriggers.collisionLeftHand_Ball;
            shot.collisionRightHand_Ball = BallTriggers.collisionRightHand_Ball;

            shot.collisionLeftFoot_Ball = BallTriggers.collisionLeftFoot_Ball;
            shot.collisionRightFoot_Ball = BallTriggers.collisionRightFoot_Ball;

            shot.collisionHead_Ball = BallTriggers.collisionHead_Ball;

            

            session.shotsList.Add(shot.Clone());

            shot.prepForNextShot();
                        
            frame.frameID = 0;

            BallTriggers.setAllFalse();
        }
			
		// END OF COLLECTION 
		if (BallTimer.timeLeft < 0 && !isDataSaved) {

            if (BallTriggers.sceneName != null)
                session.sessionName = BallTriggers.sceneName + "; Session start: " + System.DateTime.Now;

            else
                BallTriggers.sceneName = "Training";

            string jsonData = JsonConvert.SerializeObject(session);

            // File is here -> C:\Users\<user>\AppData\LocalLow\TH Cologne\torVRt\
            File.WriteAllText(Application.persistentDataPath + "/"+BallTriggers.sceneName+"_GoalieData_from_" + startTime + ".json", jsonData);
            
            isDataSaved = true;
        }
        
		System.GC.Collect ();
		System.GC.WaitForPendingFinalizers ();

	}


}

public class Session {
	public int sessionID;
	public string sessionName;
    public int shotsCount;
	public List<Shot> shotsList;

	public Session(){
		shotsList = new List<Shot> ();
	}
}

public class Shot : ICloneable<Shot>
{

    public int shotID;
    public bool isBallCaught,
                collisionLeftHand_Ball, collisionRightHand_Ball,
                collisionLeftFoot_Ball, collisionRightFoot_Ball,
                collisionHead_Ball,
                collisionLeftPost_Ball, collisionRightPost_Ball, collisionTopPost_Ball;

    public List<Frame> framesList;

    public Shot()
    {
        framesList = new List<Frame>();
        shotID = 0;
    }

    public Shot(int shotID, bool isBallCaught,
                bool collisionLeftHand_Ball, bool collisionRightHand_Ball,
                bool collisionLeftFoot_Ball, bool collisionRightFoot_Ball,
                bool collisionHead_Ball,
                bool collisionLeftPost_Ball, bool collisionRightPost_Ball, bool collisionTopPost_Ball, List<Frame> framesList)
    {
        this.shotID = shotID;

        this.isBallCaught = isBallCaught;

        this.collisionLeftHand_Ball = collisionLeftHand_Ball;
        this.collisionRightHand_Ball = collisionRightHand_Ball;
        this.collisionLeftFoot_Ball = collisionLeftFoot_Ball;
        this.collisionRightFoot_Ball = collisionRightFoot_Ball;
        this.collisionHead_Ball = collisionHead_Ball;
        this.collisionLeftPost_Ball = collisionLeftPost_Ball;
        this.collisionRightPost_Ball = collisionRightPost_Ball;
        this.collisionTopPost_Ball = collisionTopPost_Ball;

        this.framesList = framesList;
    }

    public void prepForNextShot()
    {
        framesList.Clear();

        collisionLeftHand_Ball   = false;
        collisionRightHand_Ball  = false;
        collisionLeftFoot_Ball   = false;
        collisionRightFoot_Ball  = false;
        collisionHead_Ball       = false;
        collisionLeftPost_Ball   = false;
        collisionRightPost_Ball  = false;
        collisionTopPost_Ball    = false;

        shotID++;
    }

    public Shot Clone()
    {
        List<Frame> tmpList = new List<Frame>();

        foreach (Frame frame in this.framesList)
            tmpList.Add(frame.Clone());

        return new Shot(shotID, isBallCaught,
                collisionLeftHand_Ball, collisionRightHand_Ball,
                collisionLeftFoot_Ball, collisionRightFoot_Ball,
                collisionHead_Ball,
                collisionLeftPost_Ball, collisionRightPost_Ball, collisionTopPost_Ball, tmpList);
    }
}

public class Frame : ICloneable<Frame>
{

    public int frameID;
    public string timeStampFrame;
    public Vector3 leftHandCoords, rightHandCoords, leftFootCoords, rightFootCoords, headCoords,
                ballCoords, ballStartPos, ballEndPos;

    public Quaternion leftHandRot, rightHandRot, leftFootRot, rightFootRot, headRot;

    public Frame()
    {
        leftHandCoords = Vector3.zero;
        rightHandCoords = Vector3.zero;
        leftFootCoords = Vector3.zero;
        rightFootCoords = Vector3.zero;
        headCoords = Vector3.zero;
        ballCoords = Vector3.zero;
        ballStartPos = Vector3.zero;
        ballEndPos = Vector3.zero;

        leftHandRot = new Quaternion();
        rightHandRot = new Quaternion();
        leftFootRot = new Quaternion();
        rightFootRot = new Quaternion();
        headRot = new Quaternion();
    }

    public Frame(int frameID, string timeStampFrame, Vector3 leftHandCoords, Vector3 rightHandCoords, Vector3 leftFootCoords, Vector3 rightFootCoords, Vector3 headCoords,
                Vector3 ballCoords, Vector3 ballStartPos, Vector3 ballEndPos, Quaternion leftHandRot, Quaternion rightHandRot, Quaternion leftFootRot, Quaternion rightFootRot, Quaternion headRot)
    {
        this.frameID = frameID;
        this.timeStampFrame = timeStampFrame;

        this.leftHandCoords = leftHandCoords;
        this.rightHandCoords = rightHandCoords;
        this.leftFootCoords = leftFootCoords;
        this.rightFootCoords = rightFootCoords;
        this.headCoords = headCoords;
        this.ballCoords = ballCoords;
        this.ballStartPos = ballStartPos;
        this.ballEndPos = ballEndPos;

        this.leftHandRot = leftHandRot;
        this.rightHandRot = rightHandRot;
        this.leftFootRot = leftFootRot;
        this.rightFootRot = rightFootRot;
        this.headRot = headRot;
    }

    public Frame Clone()
    {

        return new Frame(frameID, timeStampFrame,
                         new Vector3(leftHandCoords.x, leftHandCoords.y, leftHandCoords.z),
                         new Vector3(rightHandCoords.x, rightHandCoords.y, rightHandCoords.z),
                         new Vector3(leftFootCoords.x, leftFootCoords.y, leftFootCoords.z),
                         new Vector3(rightFootCoords.x, rightFootCoords.y, rightFootCoords.z),
                         new Vector3(headCoords.x, headCoords.y, headCoords.z),
                         new Vector3(ballCoords.x, ballCoords.y, ballCoords.z),
                         new Vector3(ballStartPos.x, ballStartPos.y, ballStartPos.z),
                         new Vector3(ballEndPos.x, ballEndPos.y, ballEndPos.z),
                         new Quaternion(leftHandRot.x, leftHandRot.y, leftHandRot.z, leftHandRot.w),
                         new Quaternion(rightHandRot.x, rightHandRot.y, rightHandRot.z, rightHandRot.w),
                         new Quaternion(leftFootRot.x, leftFootRot.y, leftFootRot.z, leftFootRot.w),
                         new Quaternion(rightFootRot.x, rightFootRot.y, rightFootRot.z, rightFootRot.w),
                         new Quaternion(headRot.x, headRot.y, headRot.z, headRot.w)
                         );

    }


}