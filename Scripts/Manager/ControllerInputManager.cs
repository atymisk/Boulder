using UnityEngine;
using System.Collections;

public class ControllerInputManager : InputManager {
	
	// Use this for initialization
	
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Temporary implementation used for testing-------------------------------------
		if(!p1lock)
		{
			//Controller Player 1

			if(Input.GetAxis("LeftJoystickX_P1") < 0)
			{
				playerOne.FaceLeft();
				playerOne.MoveLeft();
			}
			else if(Input.GetAxis("LeftJoystickX_P1") > 0)
			{
				playerOne.FaceRight();
				playerOne.MoveRight();
			}
			else
			{
				playerOne.StayStill();
			}
			
			if(Input.GetAxis("LeftJoystickY_P1") < 0 && previousP1Y == 0)
			{
				playerOne.Jump();
			}
			
			
			if (Input.GetButtonDown("A_P1"))
			{
                playerOne.LeftPunch();
            }
			else if(Input.GetButtonDown("B_P1"))
			{
                playerOne.RightKick();
            }
			else if(Input.GetButtonDown ("X_P1")){
				playerOne.LeftKick();
			}
			else if(Input.GetButtonDown("Y_P1"))
			{
                playerOne.RocketLeftArm();
            }

			if(Input.GetButton("LeftBumper_P1")){
				playerOne.Block();
			}
			else if(Input.GetButtonUp("LeftBumper_P1")){
				playerOne.UnBlock();
			}
			previousP1X = Input.GetAxis("LeftJoystickX_P1");
			previousP1Y = Input.GetAxis("LeftJoystickY_P1");
			
		}
		
		///--------------------------------------------------------------------------
		if(!p2lock)
		{
			//Controller P2
			if(Input.GetAxis("LeftJoystickX_P2") < 0)
			{
				playerTwo.FaceLeft();
				playerTwo.MoveLeft();
			}
			else if(Input.GetAxis("LeftJoystickX_P2") > 0)
			{
				playerTwo.FaceRight();
				playerTwo.MoveRight();
			}
			else
			{
				playerTwo.StayStill();
			}
			
			if(Input.GetAxis("LeftJoystickY_P2") < 0 && previousP2Y == 0)
			{
				playerTwo.Jump();
			}
			
			if (Input.GetButtonDown("A_P2"))
			{
                playerTwo.LeftPunch();
            }
            else if(Input.GetButtonDown("B_P2"))
			{
                playerTwo.RightKick();
            }
			else if(Input.GetButtonDown("X_P2")){
				playerTwo.LeftKick();
			}
			else if(Input.GetButtonDown("Y_P2"))
			{
				playerTwo.RocketLeftArm();
			}

			if(Input.GetButton("LeftBumper_P2")){
				playerTwo.Block();
			}
			else if(Input.GetButtonUp("LeftBumper_P2")){
				playerTwo.UnBlock();
			}
			previousP2X = Input.GetAxis("LeftJoystickX_P2");
			previousP2Y = Input.GetAxis("LeftJoystickY_P2");
		}
	}
}