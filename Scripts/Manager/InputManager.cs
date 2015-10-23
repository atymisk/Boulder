using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	public Character playerOne;
	public Character playerTwo;

	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Temporary implementation used for testing-------------------------------------

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
            playerOne.NormalMoveBeta();
            
		}
		else if(Input.GetButtonDown("Y_P1"))
		{
            playerOne.SpecialMoveAlpha();
        }

		///--------------------------------------------------------------------------
		if(Input.GetKeyDown(KeyCode.L))
		{
			playerTwo.FaceRight();
		}
		else if(Input.GetKeyDown(KeyCode.J))
		{
			playerTwo.FaceLeft();
		}

		if(Input.GetKey(KeyCode.J))
		{
            playerTwo.MoveLeft();
		}
		else if(Input.GetKey(KeyCode.L))
		{
            playerTwo.MoveRight();
		}
		else
		{
            playerTwo.StayStill();
		}
		
		if(Input.GetKeyDown(KeyCode.I))
		{
            playerTwo.Jump();
		}

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			playerTwo.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha8))
		{
            playerTwo.NormalMoveBeta();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha9))
		{
            playerTwo.SpecialMoveAlpha();
        }
	}
}
