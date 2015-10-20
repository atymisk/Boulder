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

		if(Input.GetKeyDown(KeyCode.E))
		{
			playerOne.rigidbodyTwoD.velocity = new Vector2(playerOne.rigidbodyTwoD.velocity.x, 100);
		}



		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			playerOne.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			playerOne.SpecialMoveAlpha();
		}

		///--------------------------------------------------------------------------
		if(Input.GetKeyDown(KeyCode.K))
		{
			playerTwo.FaceRight();
		}
		else if(Input.GetKeyDown(KeyCode.H))
		{
			playerTwo.FaceLeft();
		}

		if(Input.GetKey(KeyCode.H))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(50,playerTwo.rigidbodyTwoD.velocity.y);
		}
		else if(Input.GetKey(KeyCode.K))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(-50,playerTwo.rigidbodyTwoD.velocity.y);
		}
		else
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(0,playerTwo.rigidbodyTwoD.velocity.y);
		}
		
		if(Input.GetKeyDown(KeyCode.I))
		{
			playerTwo.rigidbodyTwoD.velocity = new Vector2(playerTwo.rigidbodyTwoD.velocity.x, 100);
		}

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			playerTwo.NormalMoveAlpha();
		}
		else if(Input.GetKeyDown (KeyCode.Alpha8))
		{
			playerTwo.SpecialMoveAlpha();
		}
	}
}
