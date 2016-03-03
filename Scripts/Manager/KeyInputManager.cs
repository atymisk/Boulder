using UnityEngine;
using System.Collections;

public class KeyInputManager : InputManager {

    // Use this for initialization;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Temporary implementation used for testing-------------------------------------
		if (Input.GetKeyDown(KeyCode.P)){
			gameManager.togglePause();
		}
		if (!gameManager.isPaused) {
			if (!p1lock) {
				if (Input.GetKey (KeyCode.LeftShift)) {
					playerOne.Block ();
				} else {
					playerOne.UnBlock ();
				}

				if (Input.GetKey (KeyCode.C)) {
					playerOne.rocketPrepare();
				} else {
					playerOne.rocketUnPre();
				}
				if (Input.GetKeyDown(KeyCode.E))
				{
					playerOne.Pickup();
				}

				if (Input.GetKeyDown (KeyCode.D)) {
					playerOne.FaceRight ();
				} else if (Input.GetKeyDown (KeyCode.A)) {
					playerOne.FaceLeft ();
				}

				if (Input.GetKey (KeyCode.D)) {
					playerOne.MoveRight ();
				} else if (Input.GetKey (KeyCode.A)) {
					playerOne.MoveLeft ();
				} else {
					playerOne.StayStill ();
				}

				if (Input.GetKeyDown (KeyCode.W)) {
					playerOne.Jump ();
				}

				if (Input.GetKey (KeyCode.S)){
					playerOne.Drop();
				} else {
					playerOne.UnDrop ();
				}

				if (Input.GetKeyDown (KeyCode.Alpha1)) {
					playerOne.LeftPunch ();
				} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
					playerOne.LeftKick ();
				} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
					playerOne.RightKick ();
				} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
					playerOne.RightPunch ();
				} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
					playerOne.RocketLeftArm ();
				} else if (Input.GetKeyDown (KeyCode.R)) {
					playerOne.RocketRightArm ();
				} else if (Input.GetKeyDown (KeyCode.T)) {
					playerOne.RocketLeftLeg ();
				} else if (Input.GetKeyDown (KeyCode.Y)) {
					playerOne.RocketRightLeg ();
				}
			}
			///--------------------------------------------------------------------------
			if (!p2lock) {
				if (Input.GetKey (KeyCode.RightShift)) {
	               
					playerTwo.Block ();
				} else {
					playerTwo.UnBlock ();
				}

				if (Input.GetKey (KeyCode.M)) {
					playerTwo.rocketPrepare();
				} else {
					playerTwo.rocketUnPre();
				}

				if (Input.GetKeyDown(KeyCode.U))
				{
					playerTwo.Pickup();
				}
				if (Input.GetKeyDown (KeyCode.L)) {
					playerTwo.FaceRight ();
				} else if (Input.GetKeyDown (KeyCode.J)) {
					playerTwo.FaceLeft ();
				}

				if (Input.GetKey (KeyCode.J)) {
					playerTwo.MoveLeft ();
				} else if (Input.GetKey (KeyCode.L)) {
					playerTwo.MoveRight ();
				} else {
					playerTwo.StayStill ();
				}

				if (Input.GetKeyDown (KeyCode.I)) {
					playerTwo.Jump ();
				}
				if (Input.GetKey (KeyCode.K)){
					playerTwo.Drop();
				} else {
					playerTwo.UnDrop ();
				}

				if (Input.GetKeyDown (KeyCode.Alpha7)) {
					playerTwo.LeftPunch ();
				} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
					playerTwo.LeftKick ();
				} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
					playerTwo.RightKick ();
				} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
					playerTwo.RightPunch();
				}else if (Input.GetKeyDown (KeyCode.Equals)) {
					playerTwo.RocketLeftArm();
				} else if (Input.GetKeyDown (KeyCode.O)) {
					playerTwo.RocketRightArm ();
				} else if (Input.GetKeyDown (KeyCode.P)) {
					playerTwo.RocketLeftLeg ();
				} else if (Input.GetKeyDown (KeyCode.Semicolon)) {
					playerTwo.RocketRightLeg ();
				}
			}
		}
    }

}
