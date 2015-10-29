using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {
	
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
//         	if(Input.GetAxis("LeftJoystickX_P1") < 0)
//		    {
//			    playerOne.FaceLeft();
//			    playerOne.MoveLeft();
//		    }
//		    else if(Input.GetAxis("LeftJoystickX_P1") > 0)
//		    {
//			    playerOne.FaceRight();
//			    playerOne.MoveRight();
//		    }
//		    else
//		    {
//                playerOne.StayStill();
//		    }
//		    if(Input.GetAxis("LeftJoystickY_P1") < 0)
//		    {
//                playerOne.Jump();
//		    }
//			if (Input.GetButtonDown("A_P1"))
//			{
//				playerOne.NormalMoveAlpha();
//			}
//			else if(Input.GetButtonDown("B_P1"))
//			{
//				playerOne.SpecialMoveAlpha();
//			}
//			else if(Input.GetButtonDown("Y_P1"))
//			{
//				playerOne.NormalMoveBeta();
//			}
//			else if (Input.GetButtonDown ("X_P1"))
//			{
//				playerOne.RocketPunch();
//			}

			//Keyboard Player 1
			if (Input.GetKeyDown(KeyCode.D))
			{
				playerOne.FaceRight();
			}
			else if(Input.GetKeyDown(KeyCode.A))
			{
				playerOne.FaceLeft();
			}
			
			if(Input.GetKey(KeyCode.D))
			{
				playerOne.rigidbodyTwoD.velocity = new Vector2(50,playerOne.rigidbodyTwoD.velocity.y);
			}
			else if(Input.GetKey(KeyCode.A))
			{
				playerOne.rigidbodyTwoD.velocity = new Vector2(-50,playerOne.rigidbodyTwoD.velocity.y);
			}
			else
			{
				playerOne.rigidbodyTwoD.velocity = new Vector2(0,playerOne.rigidbodyTwoD.velocity.y);
			}
			
			if(Input.GetKeyDown(KeyCode.E))
			{
				playerOne.rigidbodyTwoD.velocity = new Vector2(playerOne.rigidbodyTwoD.velocity.x, 100);
			}
			
			
			if (Input.GetKeyDown(KeyCode.Alpha1))// || Input.GetButtonDown("A_P1"))
			{
				playerOne.NormalMoveAlpha();
			}
			else if(Input.GetKeyDown (KeyCode.Alpha2))// || Input.GetButtonDown("B_P1"))
			{
				playerOne.SpecialMoveAlpha();
			}
			else if(Input.GetKeyDown (KeyCode.Alpha3))// || Input.GetButtonDown("Y_P1"))
			{
				playerOne.NormalMoveBeta();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				playerOne.RocketPunch();
			}


		}
		
		///--------------------------------------------------------------------------
		if(!p2lock)
		{
			//Controller P2
//         	if(Input.GetAxis("LeftJoystickX_P2") < 0)
//		    {
//			    playerTwo.FaceLeft();
//			    playerTwo.MoveLeft();
//		    }
//		    else if(Input.GetAxis("LeftJoystickX_P2") > 0)
//		    {
//			    playerTwo.FaceRight();
//			    playerTwo.MoveRight();
//		    }
//		    else
//		    {
//                playerTwo.StayStill();
//		    }
//		    if(Input.GetAxis("LeftJoystickY_P2") < 0)
//		    {
//                playerTwo.Jump();
//		    }
//			if (Input.GetButtonDown("A_P2"))
//			{
//				playerTwo.NormalMoveAlpha();
//			}
//			else if(Input.GetButtonDown("B_P2"))
//			{
//				playerTwo.SpecialMoveAlpha();
//			}
//			else if(Input.GetButtonDown("Y_P2"))
//			{
//				playerTwo.NormalMoveBeta();
//			}
//			else if (Input.GetButtonDown ("X_P2"))
//			{
//				playerTwo.RocketPunch();
//			}

			//Keyboard P2
			if (Input.GetKeyDown(KeyCode.L))
			{
				playerTwo.FaceRight();
			}
			else if (Input.GetKeyDown(KeyCode.J))
			{
				playerTwo.FaceLeft();
			}
			
			if (Input.GetKey(KeyCode.J))
			{
				playerTwo.MoveLeft();
			}
			else if (Input.GetKey(KeyCode.L))
			{
				playerTwo.MoveRight();
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
				playerTwo.NormalMoveAlpha();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha8))
			{
				playerTwo.NormalMoveBeta();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha9))
			{
				playerTwo.SpecialMoveAlpha();
			}


		}
	}
}