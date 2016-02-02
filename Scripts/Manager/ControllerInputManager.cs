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
		if (!gameManager.isPaused) {
			if (!p1lock) {
//				if (controller1 != "Wireless Controller"){
					Debug.Log (Input.GetAxis ("LeftJoystickY_P1"));
					
					//Controller Player 1
					if (Input.GetAxis ("LeftJoystickX_P1") < 0) {
						playerOne.FaceLeft ();
						playerOne.MoveLeft ();
					} else if (Input.GetAxis ("LeftJoystickX_P1") > 0) {
						playerOne.FaceRight ();
						playerOne.MoveRight ();
					} else {
						playerOne.StayStill ();
					}
					
					if (Input.GetAxis ("LeftJoystickY_P1") < 0 && previousP1Y == 0) {
						Debug.Log (Input.GetAxis ("LeftJoystickY_P1"));
						playerOne.Jump ();
					}
					
					//					if(Input.GetAxis ("LeftJoystickY_P1") > 0 && previ
					/*(Input.GetButton ("RightBumper_P1") ||*/
					if (Input.GetButtonDown ("X_P1") && Input.GetAxis("RightTrigger_P1") > 0) {
						playerOne.RocketLeftArm ();
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
					//if the right trigger is on hold
					//if (Input.GetButton ("RightTrigger_P1")) {
					//		playerOne.rocketPrepare ();
					//} else if (Input.GetButtonUp ("RightTrigger_P1")) {
					//		playerOne.rocketUnPre ();
					//}

					previousP1X = Input.GetAxis ("LeftJoystickX_P1");
					previousP1Y = Input.GetAxis ("LeftJoystickY_P1");
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
					Debug.Log(Input.GetAxis("RightTrigger_P2"));
					if (Input.GetAxis ("LeftJoystickX_P2") < 0) {
						playerTwo.FaceLeft ();
						playerTwo.MoveLeft ();
					} else if (Input.GetAxis ("LeftJoystickX_P2") > 0) {
						playerTwo.FaceRight ();
						playerTwo.MoveRight ();
					} else {
						playerTwo.StayStill ();
					}
					
					if (Input.GetAxis ("LeftJoystickY_P2") < 0 && previousP2Y == 0) {
						playerTwo.Jump ();
					}
					/*(Input.GetButton ("RightBumper_P2") ||*/
					if (Input.GetButtonDown ("X_P2") && Input.GetAxis("RightTrigger_P2") > 0) {
						playerTwo.RocketLeftArm ();
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
					//if the right trigger is on hold
					//if (Input.GetButton ("RightTrigger_P2")) {
					//		playerTwo.rocketPrepare ();
					//} else if (Input.GetButtonUp ("RightTrigger_P2")) {
					//		playerTwo.rocketUnPre ();
					//}
					
					previousP2X = Input.GetAxis ("LeftJoystickX_P2");
					previousP2Y = Input.GetAxis ("LeftJoystickY_P2");
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