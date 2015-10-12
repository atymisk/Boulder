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
		//Temporary implementation used for testing
		if(Input.GetKeyDown(KeyCode.D))
		{
			playerOne.FaceRight();
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			playerOne.FaceLeft();
		}

		if (Input.GetKeyDown(KeyCode.U))
		{
			playerOne.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.I))
		{
			playerOne.SpecialMoveAlpha();
		}

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			playerTwo.FaceRight();
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			playerTwo.FaceLeft();
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
		{
			playerTwo.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.Keypad2))
		{
			playerTwo.SpecialMoveAlpha();
		}
	}
}
