using UnityEngine;
using System.Collections;

public class ControllerInputManager : MonoBehaviour {
	
	// Use this for initialization
	public Character playerOne;
	public Character playerTwo;
	
	private bool p1lock = false;
	private bool p2lock = false;
	//p1 controls need to be disabled temporarily until p1 respawns
	public void lockp1control()
	{
		p1lock = true;//p1 is dead
	}
	
	//p2 controls need to be disabled temporarily until p2 respawns
	public void lockp2control()
	{
		p2lock = true;//p2 is dead
	}
	
	//respawned
	public void unlockp1control(Character newp1)
	{
		playerOne = newp1;
		p1lock = false;
	}
	
	public void unlockp2control(Character newp2)
	{
		playerTwo = newp2;
		p2lock = false;
	}
	
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
			
			if(Input.GetAxis("LeftJoystickY_P1") < 0)
			{
				playerOne.Jump();
			}
			
			
			if (Input.GetButtonDown("A_P1"))
			{
				playerOne.NormalMoveAlpha();
			}
			else if(Input.GetButtonDown("B_P1"))
			{
				playerOne.SpecialMoveAlpha();
			}
			else if(Input.GetButtonDown("Y_P1"))
			{
				playerOne.NormalMoveBeta();
			}
			else if (Input.GetButtonDown ("X_P1"))
			{
				playerOne.RocketPunch();
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
			
			if(Input.GetAxis("LeftJoystickY_P2") < 0)
			{
				playerTwo.Jump();
			}
			
			if (Input.GetButtonDown("A_P2"))
			{
				playerTwo.NormalMoveAlpha();
			}
			else if(Input.GetButtonDown("B_P2"))
			{
				playerTwo.NormalMoveBeta();
			}
			else if(Input.GetButtonDown("Y_P2"))
			{
				playerTwo.SpecialMoveAlpha();
			}
			else if (Input.GetButtonDown ("X_P2"))
			{
				playerTwo.RocketPunch();
			}
			
			
		}
	}
}