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
				previousP1Input = "LeftJoystickX_Left";
			}
			else if(Input.GetAxis("LeftJoystickX_P1") > 0)
			{
				playerOne.FaceRight();
				playerOne.MoveRight();
				previousP1Input = "LeftJoystickX_Right";
			}
			else
			{
				playerOne.StayStill();
				previousP1Input = "";
			}
			
			if(Input.GetAxis("LeftJoystickY_P1") < 0)
			{
				playerOne.Jump();
				previousP1Input = "LeftJoystickY_Up";
			}
			
			
			if (Input.GetButtonDown("A_P1"))
			{
                playerOne.LeftPunch();
				previousP1Input = "A";
            }
			else if(Input.GetButtonDown("B_P1"))
			{
                playerOne.RightKick();
				previousP1Input = "B";
            }
			else if(Input.GetButtonDown("Y_P1"))
			{
                playerOne.RocketLeftArm();
				previousP1Input = "Y";
            }
			
			
		}
		
		///--------------------------------------------------------------------------
		if(!p2lock)
		{
			//Controller P2
			if(Input.GetAxis("LeftJoystickX_P2") < 0)
			{
				playerTwo.FaceLeft();
				playerTwo.MoveLeft();
				previousP2Input = "LeftJoystickX_Left";
			}
			else if(Input.GetAxis("LeftJoystickX_P2") > 0)
			{
				playerTwo.FaceRight();
				playerTwo.MoveRight();
				previousP2Input = "LeftJoystickX_Right";
			}
			else
			{
				playerTwo.StayStill();
				previousP2Input = "";
			}
			
			if(Input.GetAxis("LeftJoystickY_P2") < 0)
			{
				playerTwo.Jump();
				previousP2Input = "LeftJoystickX_Up";
			}
			
			if (Input.GetButtonDown("A_P2"))
			{
                playerTwo.LeftPunch();
				previousP2Input = "A";
            }
            else if(Input.GetButtonDown("B_P2"))
			{
                playerTwo.RightKick();
				previousP2Input = "B";
            }
			else if(Input.GetButtonDown("Y_P2"))
			{
				playerTwo.RocketLeftArm();
				previousP2Input = "Y";
			}
			
		}
	}
}