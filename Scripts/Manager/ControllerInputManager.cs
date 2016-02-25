using UnityEngine;
using System.Collections;

public class ControllerInputManager : InputManager
{
	// Use this for initialization
	
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
//		string controller1 = Input.GetJoystickNames () [0];
//		string controller2 = Input.GetJoystickNames () [1];
		//Temporary implementation used for testing-------------------------------------
		//if (controller1 == "Wireless Controller") {
		//	if (Input.GetButtonDown ("RightStickClick_P1")) {
		//		gameManager.togglePause();
        //    }
        //} else {
		if (Input.GetButtonDown ("Start_P1"))
        {
            gameManager.togglePause();
        }
    //    }
		//if (controller2 == "Wireless Controller") {
		//	if (Input.GetButtonDown ("RightStickClick_P2")) {
        //        gameManager.togglePause();
        //    }
		//} else {
		if (Input.GetButtonDown ("Start_P2")) 
        {
            gameManager.togglePause();
        }
//		}
		//Player Suicide
		if (Input.GetButtonDown ("Back_P1") && Input.GetAxis ("RightTrigger_P1") > 0 && Input.GetAxis ("LeftTrigger_P1") > 0) {
			gameManager.thisPlayerDied("P1");
		}
		if (Input.GetButtonDown ("Back_P2") && Input.GetAxis ("RightTrigger_P2") > 0 && Input.GetAxis ("LeftTrigger_P2") > 0) {
			gameManager.thisPlayerDied("P2");
		}
		if (!gameManager.isPaused) {
			if (!p1lock) {
//				if (controller1 != "Wireless Controller"){
					
					//Controller Player 1
					if (Input.GetAxis ("LeftJoystickX_P1") < 0 || Input.GetAxis ("DPadX_P1") < 0 && previousP1X == 0) {
						playerOne.FaceLeft ();
						playerOne.MoveLeft ();
					} else if (Input.GetAxis ("LeftJoystickX_P1") > 0 || Input.GetAxis("DPadX_P1") > 0 && previousP1X == 0) {
						playerOne.FaceRight ();
						playerOne.MoveRight ();
					} else {
						playerOne.StayStill ();
					}

//					if ((Input.GetAxis ("LeftJoystickY_P1") < 0  && previousP1Y == 0)  || (Input.GetAxis("DPadY_P1") > 0 && previousP1DPadY == 0)) {
					if(Input.GetAxis ("LeftJoystickX_P1") < 0.3  || Input.GetAxis ("LeftJoystickX_P1") > -0.3){
						if ((Input.GetAxis ("LeftJoystickY_P1") < 0  && previousP1Y == 0)  || (Input.GetAxis("DPadY_P1") > 0 && previousP1DPadY == 0)) {
							playerOne.Jump ();
						}
					}

					if (Input.GetAxis ("DPadY_P1") < 0 || Input.GetAxis("LeftJoystickY_P1") > 0){
						playerOne.Drop();
					} else {
						playerOne.UnDrop();
					}


					if (Input.GetButton("LeftStickClick_P1") || Input.GetButton ("RightStickClick_P1")) {
						playerOne.Jump ();
					}
					
					if (Input.GetButtonDown ("X_P1") && (Input.GetAxis("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0)) {
						playerOne.RocketLeftArm ();
					}
					else if (Input.GetButtonDown ("Y_P1") && (Input.GetAxis("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0)) {
						playerOne.RocketRightArm ();
					}else if (Input.GetButtonDown ("A_P1") && (Input.GetAxis("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0)) {
						playerOne.RocketLeftLeg ();
					}else if (Input.GetButtonDown ("B_P1") && (Input.GetAxis("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0)) {
						playerOne.RocketRightLeg ();
					} else if (Input.GetButtonDown ("X_P1")) {
						playerOne.LeftPunch ();
					} else if (Input.GetButtonDown("Y_P1")) {
                        playerOne.RightPunch();
                    } else if (Input.GetButtonDown ("B_P1")) {
						playerOne.RightKick ();
					} else if (Input.GetButtonDown ("A_P1")) {
						playerOne.LeftKick ();
					} else if(Input.GetButton ("RightBumper_P1")){
						playerOne.Pickup();
					}
					
					
					if (Input.GetButton ("LeftBumper_P1")) {
						playerOne.Block ();
					} else if (Input.GetButtonUp ("LeftBumper_P1")) {
						playerOne.UnBlock ();
					}

					//change the top corner UI for the ready of rocket moves
//					if the right or left trigger is on hold
					if (Input.GetAxis ("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0) {
							playerOne.rocketPrepare ();
					} else{
							playerOne.rocketUnPre ();
					}

					previousP1X = Input.GetAxis ("LeftJoystickX_P1");
					previousP1Y = Input.GetAxis ("LeftJoystickY_P1");
					previousP1DPadY = Input.GetAxis("DPadY_P1");
//				}
//				else{
//					if (Input.GetAxis ("LeftJoystickX_P1") < 0) {
//						playerOne.FaceLeft ();
//						playerOne.MoveLeft ();
//					} else if (Input.GetAxis ("LeftJoystickX_P1") > 0) {
//						playerOne.FaceRight ();
//						playerOne.MoveRight ();
//					} else {
//						playerOne.StayStill ();
//					}
//					
//					if (Input.GetAxis ("LeftJoystickY_P1") < 0 && previousP1Y == 0) {
//						playerOne.Jump ();
//					}
//					
//					if (Input.GetButtonDown ("B_P1") && (Input.GetButton ("RightBumper_P1") || Input.GetButton("Start_P1"))) {
//						playerOne.RocketLeftArm ();
//					} else if (Input.GetButtonDown ("B_P1")) {
//						playerOne.LeftPunch ();
//					} else if (Input.GetButtonDown ("X_P1")) {
//						playerOne.RightKick ();
//					} else if (Input.GetButtonDown ("A_P1")) {
//						playerOne.LeftKick ();
//					} 
//					
//					if (Input.GetButton ("LeftBumper_P1")) {
//						playerOne.Block ();
//					} else if (Input.GetButtonUp ("LeftBumper_P1")) {
//						playerOne.UnBlock ();
//					}
//					previousP1X = Input.GetAxis ("LeftJoystickX_P1");
//					previousP1Y = Input.GetAxis ("LeftJoystickY_P1");
//					
//				}
				
			}
			
			///--------------------------------------------------------------------------
			if (!p2lock) {
//				if(controller2 != "Wireless Controller"){
					//Controller P2
					if (Input.GetAxis ("LeftJoystickX_P2") < 0 || Input.GetAxis("DPadX_P2") < 0) {
						playerTwo.FaceLeft ();
						playerTwo.MoveLeft ();
					} else if (Input.GetAxis ("LeftJoystickX_P2") > 0 || Input.GetAxis("DPadX_P2") > 0) {
						playerTwo.FaceRight ();
						playerTwo.MoveRight ();
					} else {
						playerTwo.StayStill ();
					}
					
//					if ((Input.GetAxis ("LeftJoystickY_P2") < 0  && previousP2Y == 0)  || (Input.GetAxis("DPadY_P2") > 0 && previousP2DPadY == 0)) {
					if(Input.GetAxis ("LeftJoystickX_P2") < 0.3  || Input.GetAxis ("LeftJoystickX_P2") > -0.3){
						if ((Input.GetAxis ("LeftJoystickY_P2") < 0  && previousP2Y == 0)  || (Input.GetAxis("DPadY_P2") > 0 && previousP2DPadY == 0)) {
							playerTwo.Jump ();
						}
					}

					if (Input.GetAxis ("DPadY_P2") < 0 || Input.GetAxis("LeftJoystickY_P2") > 0){
						playerTwo.Drop();
					} else {
						playerTwo.UnDrop();
					}

					if (Input.GetButton("LeftStickClick_P2") || Input.GetButton ("RightStickClick_P2")) {
						playerTwo.Jump ();
					}

					if (Input.GetButtonDown ("X_P2") && (Input.GetAxis("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0)) {
						playerTwo.RocketLeftArm ();
					}
					else if (Input.GetButtonDown ("Y_P2") && (Input.GetAxis("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0)) {
						playerTwo.RocketRightArm ();
					}else if (Input.GetButtonDown ("A_P2") && (Input.GetAxis("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0)) {
						playerTwo.RocketLeftLeg ();
					}else if (Input.GetButtonDown ("B_P2") && (Input.GetAxis("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0)) {
						playerTwo.RocketRightLeg ();
					}else if (Input.GetButtonDown ("X_P2")) {
						playerTwo.LeftPunch ();
					}else if (Input.GetButtonDown("Y_P2")) {
                        playerTwo.RightPunch();
                    }else if (Input.GetButtonDown ("B_P2")) {
						playerTwo.RightKick ();
					}else if (Input.GetButtonDown ("A_P2")) {
						playerTwo.LeftKick ();
					}else if(Input.GetButton ("RightBumper_P2")){
						playerTwo.Pickup();
					}
					
					
					if (Input.GetButton ("LeftBumper_P2")) {
						playerTwo.Block ();
					} else if (Input.GetButtonUp ("LeftBumper_P2")) {
						playerTwo.UnBlock ();
					}
					
					//change the top corner UI for the ready of rocket moves
					//if the right or left trigger is on hold
					if (Input.GetAxis ("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0) {
						playerTwo.rocketPrepare ();
					} else {
						playerTwo.rocketUnPre ();
					}
					
					previousP2X = Input.GetAxis ("LeftJoystickX_P2");
					previousP2Y = Input.GetAxis ("LeftJoystickY_P2");
					previousP2DPadY = Input.GetAxis("DPadY_P2");
//				}
//				else{
//					//Controller P2
//					if (Input.GetAxis ("LeftJoystickX_P2") < 0) {
//						playerTwo.FaceLeft ();
//						playerTwo.MoveLeft ();
//					} else if (Input.GetAxis ("LeftJoystickX_P2") > 0) {
//						playerTwo.FaceRight ();
//						playerTwo.MoveRight ();
//					} else {
//						playerTwo.StayStill ();
//					}
//					
//					if (Input.GetAxis ("LeftJoystickY_P2") < 0 && previousP2Y == 0) {
//						playerTwo.Jump ();
//					}
//					
//					if (Input.GetButtonDown ("B_P2") && (Input.GetButton ("RightBumper_P2") || Input.GetButton("Start_P2"))) {
//						playerTwo.RocketLeftArm ();
//					}
//					else if (Input.GetButtonDown ("B_P2")) {
//						playerTwo.LeftPunch ();
//					} else if (Input.GetButtonDown ("X_P2")) {
//						playerTwo.RightKick ();
//					} else if (Input.GetButtonDown ("A_P2")) {
//						playerTwo.LeftKick ();
//					}
//					
//					if (Input.GetButton ("LeftBumper_P2")) {
//						playerTwo.Block ();
//					} else if (Input.GetButtonUp ("LeftBumper_P2")) {
//						playerTwo.UnBlock ();
//					}
//					previousP2X = Input.GetAxis ("LeftJoystickX_P2");
//					previousP2Y = Input.GetAxis ("LeftJoystickY_P2");
//				}
			}
		}
	}
}