using UnityEngine;
using System.Collections;

public class RobotTestInputs : MonoBehaviour {
    public Robot playerOne;
    public Robot playerTwo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerOne.FaceRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            playerOne.FaceLeft();
        }


        if (Input.GetKey(KeyCode.D))
        {
            playerOne.MoveRight();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            playerOne.MoveLeft();
        }
        else
        {
            playerOne.StayStill();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerOne.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerOne.LeftPunch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerOne.RightKick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerOne.RocketLeftArm();
        }

        //Player Two
        if (Input.GetKeyDown(KeyCode.L))
        {
            playerTwo.FaceRight();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            playerTwo.FaceLeft();
        }


        if (Input.GetKey(KeyCode.L))
        {
            playerTwo.MoveRight();
        }
        else if (Input.GetKey(KeyCode.J))
        {
            playerTwo.MoveLeft();
        }
        else
        {
            playerTwo.StayStill();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            playerTwo.Jump();
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            playerTwo.LeftPunch();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            playerTwo.RightKick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            playerTwo.RocketLeftArm();
        }
    }


}
