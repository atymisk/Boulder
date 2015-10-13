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

		if(Input.GetKeyDown(KeyCode.D))
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

		if(Input.GetKeyDown(KeyCode.J))
		{
			playerOne.rigidbodyTwoD.velocity = new Vector2(playerOne.rigidbodyTwoD.velocity.x, 100);
		}



		if (Input.GetKeyDown(KeyCode.U))
		{
			playerOne.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.I))
		{
			playerOne.SpecialMoveAlpha();
		}

		///--------------------------------------------------------------------------
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			playerTwo.FaceRight();
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			playerTwo.FaceLeft();
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(50,playerTwo.rigidbodyTwoD.velocity.y);
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(-50,playerTwo.rigidbodyTwoD.velocity.y);
		}
		else
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(0,playerTwo.rigidbodyTwoD.velocity.y);
		}
		
		if(Input.GetKeyDown(KeyCode.Keypad1))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(playerTwo.rigidbodyTwoD.velocity.x, 100);
		}

		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			playerTwo.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.Keypad5))
		{
			playerTwo.SpecialMoveAlpha();
		}
	}
}
