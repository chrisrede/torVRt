/*
 * Project: TorVRt
 * Authors: Philipp Bzdok and Christian Redekop
 * Date:    27.08.2018
 * 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallTrajectory_Barrier_Left : MonoBehaviour {

    AudioSource source;
    public AudioClip abschuss;

    public Rigidbody ball;

    private float xForce, yForce, zForce;
    private float xtarget, ytarget;
    private List<Vector3> positionsOfBall;
    private Vector3 currentPosition, lastPosition, velocity, currentDirection, startPosBall;
    bool isLeft;

    private float distanceFromStartPos;

    public GameObject goalTriggerCube;
    private GameObject rotatingBall;

    private int goalCount = 0;
    private int tmpGoalCount = 0;
    private int catchCount = 0;

    public Text caughtText, timeText;

    private bool isBallCaught, isBallLanded;

    private Color dynamicColor;

    public Text progressText;
    public Image progressImage;

    // Debug trajectory view
    private void OnDrawGizmos () {
        Gizmos.color = dynamicColor;
        if (positionsOfBall != null) {
            foreach (Vector3 v in positionsOfBall) {
                Gizmos.DrawSphere (v, 0.1f);
            }
        }
    }

    void Start () {
        source = GetComponent<AudioSource> ();
        source.clip = abschuss;

        BallTimer.roundTime = 120;
        BallTimer.totalTime = 0;
        BallTimer.timeLeft = BallTimer.roundTime;
        BallTimer.timePerPeriod = 0;
        BallTimer.period = 3.9f;

        caughtText.text = "Caught: " + catchCount.ToString ();
        timeText.text = "Time left: " + BallTimer.timeLeft.ToString ();
        goalTriggerCube = GameObject.Find ("GoalTriggerCube");
        rotatingBall = GameObject.Find ("Ball");

        goalCount = goalTriggerCube.GetComponent<GoalCounter> ().getCount ();
        isBallLanded = true;

        dynamicColor = Color.magenta;
        startPosBall = new Vector3 (-1.64f, 0.01f, -15.34f);
        resetBall ();

        positionsOfBall = new List<Vector3> ();

        xForce = 0.75f;
        yForce = 1.5f;
        zForce = 2.8f;

        tmpGoalCount = goalCount;
        isBallCaught = false;
        BallTriggers.isBallCaught = false;
        BallTriggers.sceneName = "Barrier_Training";
    }

    void shootBall () {
        ball.AddForce (xForce + (xtarget / 100), yForce + (ytarget / 100), zForce, ForceMode.Impulse);
    }

    public void FixedUpdate () {
        BallTimer.totalTime += Time.deltaTime;
        BallTimer.timeLeft = BallTimer.roundTime - BallTimer.totalTime;
        if (BallTimer.timeLeft > 0) {
            timeText.text = "Time left: " + BallTimer.timeLeft.ToString("0");
        }
        else {
            timeText.text = "Time's up!";

            progressImage.color = new Color(0, 0, 0, 0.7f);
            progressText.text = "Touch red post on the right with your left hand";
        }

        goalCount = goalTriggerCube.GetComponent<GoalCounter> ().getCount ();

        System.Random rndx = new System.Random ();
        System.Random rndy = new System.Random ();
        xtarget = rndx.Next (30, 60);
        ytarget = rndy.Next (-20, 10);

        if (goalCount > tmpGoalCount) {
            isBallCaught = false;
        }

        lastPosition = ball.position;
        BallTimer.timePerPeriod += Time.deltaTime;

        if (BallTimer.timePerPeriod < BallTimer.period && BallTimer.timePerPeriod > BallTimer.period - 0.5f) {
            resetBall ();
        }

        if (BallTimer.timePerPeriod >= BallTimer.period && BallTimer.timeLeft > 0) {

            dynamicColor = Color.magenta;
            positionsOfBall.Clear ();
            isBallLanded = false;

            if (isBallCaught == true) {
                catchCount++;
                BallTriggers.isBallCaught = true;
                caughtText.text = "Caught: " + catchCount.ToString ();
            }

            BallTriggers.isNextShot = true;

            isBallCaught = true;

            tmpGoalCount = goalCount;

            resetBall ();

            if (ball.velocity == Vector3.zero) {
                shootBall ();
                source.clip = abschuss;
                source.volume = 0.3f;
                source.Play ();
                currentPosition = ball.position;
            }
            BallTimer.timePerPeriod = 0;
        }

        positionsOfBall.Add (currentPosition);

        Vector3 orthogonalToDirection = new Vector3 (0, 0, 0);
        Vector3 upVecY = new Vector3 (0, 1, 0);
        Vector3 downVecY = new Vector3 (0, -1, 0);

        if (!isBallLanded) {
            
            rotatingBall.transform.Rotate (upVecY, -1000 * Time.deltaTime);

            velocity = currentPosition - lastPosition;
            currentDirection = velocity.normalized;
            
            orthogonalToDirection = Vector3.Cross (upVecY, currentDirection);
           
            System.Random randomCurve = new System.Random ();
            ball.AddForce (randomCurve.Next (1, 3) * orthogonalToDirection * distanceFromStartPos*0.9f, ForceMode.Acceleration);

        }

        currentPosition = ball.position;
        distanceFromStartPos = Vector3.Distance (startPosBall, currentPosition);
    }

    void resetBall () {
        ball.velocity = Vector3.zero;
        ball.angularVelocity = Vector3.zero;
        ball.transform.rotation = Quaternion.identity;
        ball.transform.position = startPosBall;
    }

    void OnCollisionEnter (UnityEngine.Collision col) {
        if (distanceFromStartPos >= 2) {
            dynamicColor = Color.cyan;
            isBallLanded = true;
        }

        if (col.gameObject.tag == "TopPost") BallTriggers.collisionTopPost_Ball = true;
        if (col.gameObject.tag == "LeftPost") BallTriggers.collisionLeftPost_Ball = true;
        if (col.gameObject.tag == "RightPost") BallTriggers.collisionRightPost_Ball = true;

        if (col.gameObject.tag == "LeftHand") BallTriggers.collisionLeftHand_Ball = true;
        if (col.gameObject.tag == "RightHand") BallTriggers.collisionRightHand_Ball = true;

        if (col.gameObject.tag == "LeftTracker") BallTriggers.collisionLeftFoot_Ball = true;
        if (col.gameObject.tag == "RightTracker") BallTriggers.collisionRightFoot_Ball = true;

        if (col.gameObject.tag == "MainCamera") BallTriggers.collisionHead_Ball = true;
    }

}