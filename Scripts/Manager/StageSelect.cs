using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelect : InputManager {
	public static StageSelect instance = null;
	
	public GameObject screen;
	public GameObject controller;
	public Robot player1;
	public Robot player2;
	public Text skipText;
	private string sceneN;
	private bool keyPressed = false;
	private bool keyPressed2 = false;

	private bool blocking = false;
	private bool blocking2 = false;

	private bool p1skip = false;
	private bool p2skip = false;
	
	void Start()
	{
		controller.SetActive (false);
		instance = this;
	}
	
	void Update()
	{
		skip ();
		if (Input.GetAxis ("RightTrigger_P1") > 0 || Input.GetAxis ("LeftTrigger_P1") > 0) {
			player1.rocketPrepare ();
		} else{
			player1.rocketUnPre ();
		}

		if (Input.GetAxis ("RightTrigger_P2") > 0 || Input.GetAxis ("LeftTrigger_P2") > 0) {
			player2.rocketPrepare ();
		} else{
			player2.rocketUnPre ();
		}
	}
	
	public void skip()
	{
		//to skip the tutorial
		//if (Input.GetKeyDown(KeyCode.Space)|| Input.GetButton("Start_P1")|| Input.GetButton ("Start_P2"))
		if ((Input.GetKeyDown (KeyCode.Space)) || (p1skip && p2skip)) {
			controller.transform.FindChild ("title").GetComponent<Text> ().text = "Game Loading...";
			Application.LoadLevel (sceneN);
		} 
		else if (Input.GetKeyDown (KeyCode.C) || Input.GetButton("Start_P1")) {
			p1skip = true;
			controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Waiting for Player2 to skip...";
		} else if (Input.GetKeyDown (KeyCode.N) || Input.GetButton ("Start_P2")) {
			p2skip = true;
			controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Waiting for Player1 to skip...";
		}
	}
	
	public void ChangetoScene (string sceneName) {
		sceneN = sceneName;
		if (sceneName == "MainUI")
			Application.LoadLevel (sceneName);
		else {
			screen.SetActive (false);
			controller.SetActive(true);
			skipText.text = "Space(keyboard) or Start(controller) to skip";
			StartCoroutine (tutorial ());
		}
	}
	
	public void ChangeAfterDelay(string sceneName, float delay)
	{
		
		StartCoroutine(DelayRoutine(sceneName, delay));
	}
	
	public void Exit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		Application.OpenURL(webplayerQuitURL);
		#else
		Application.Quit();
		#endif
	}
	
	IEnumerator DelayRoutine(string sceneName, float delay)
	{
		yield return new WaitForSeconds(delay);
		ChangetoScene(sceneName);
	}

	IEnumerator waitForKey(KeyCode key, KeyCode key2, string button, string button2,string theMove)
	{
		while(!keyPressed || !keyPressed2){
			if(p1skip)
			{
				keyPressed = true;
			}
			else{
				if (theMove == "RocketLeftArm" || theMove == "RocketLeftLeg" || theMove == "RocketRightArm" || theMove == "RockeRightLeg")
				{
					if(Input.GetKeyDown(key) || (Input.GetButtonDown (button) && (Input.GetAxis("RightTrigger_P1") > 0)))
					{
						doMove(player1, theMove);
						keyPressed = true;
						controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Excellent!";
						yield return new WaitForSeconds(0.5f);
						controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Please wait for the other player...";
					}
				}
				else
				{
					if(Input.GetKeyDown(key) || Input.GetButtonDown(button))
					{
						doMove(player1, theMove);
						keyPressed = true;
						controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Excellent!";
						yield return new WaitForSeconds(0.5f);
						controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Please wait for the other player...";
					}
				}
			}
			
			if(p2skip)
			{
				keyPressed2 = true;
			}
			else{
				if (theMove == "RocketLeftArm" || theMove == "RocketLeftLeg" || theMove == "RocketRightArm" || theMove == "RockeRightLeg")
				{
					if(Input.GetKeyDown(key2) || (Input.GetButtonDown (button2) && (Input.GetAxis("RightTrigger_P2") > 0)))
					{
						doMove(player2, theMove);
						keyPressed2 = true;
						controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Excellent!";
						yield return new WaitForSeconds(0.5f);
						controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Please wait for the other player...";
					}
				}
				else
				{
					if(Input.GetKeyDown(key2) || Input.GetButtonDown(button2))
					{
						doMove(player2, theMove);
						keyPressed2 = true;
						controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Excellent!";
						yield return new WaitForSeconds(0.5f);
						controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Please wait for the other player...";
					}
				}
			}

			yield return null;
		}
	}

	IEnumerator waitForHoldKey(KeyCode key, KeyCode key2, string button, string button2, string theMove, string theUndo)
	{
		while(!keyPressed || !keyPressed2){
			if(p1skip)
			{
				keyPressed = true;
			}
			else if((Input.GetKey(key) || Input.GetButton (button)) && blocking == false)
			{
				doMove(player1, theMove);
				blocking = true;
			}
			else if((!Input.GetKey(key) || Input.GetButtonUp (button)) && blocking == true)
			{
				doMove(player1, theUndo);
				keyPressed = true;
				controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "Excellent!";
			}

			if(p2skip)
			{
				keyPressed2 = true;
			}
			else if((Input.GetKey(key2) || Input.GetButton (button2)) && blocking2 == false)
			{
				doMove(player2, theMove);
				blocking2 = true;
			}
			else if((!Input.GetKey(key2) || Input.GetButtonUp (button2)) && blocking2 == true)
			{
				doMove(player2,theUndo);
				keyPressed2 = true;
				controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "Excellent!";
			}

			yield return null;
		}
	}


	public void doMove(Robot player, string theMove)
	{
		if (theMove == "block")
			player.Block ();
		else if (theMove == "unblock")
			player.UnBlock ();
		else if (theMove == "leftPunch")
			player.LeftPunch ();
		else if (theMove == "rightPunch")
			player.RightPunch ();
		else if (theMove == "leftKick")
			player.LeftKick ();
		else if (theMove == "rightKick")
			player.RightKick ();
		else if (theMove == "RocketLeftArm")
			player.RocketLeftArm ();
		else if (theMove == "RocketRightArm")
			player.RocketRightArm ();
		else if (theMove == "RocketLeftLeg")
			player.RocketLeftLeg ();
		else if (theMove == "RocketRightLeg")
			player.RocketRightLeg ();
		else if (theMove == "pickUp")
			player.Pickup ();

	}

	public void reSet(int flag, string lastMove)
	{
		if (flag == 1) {
			player1.transform.position = new Vector2 (-17f, -12.51906f);
			player2.transform.position = new Vector2 (64f, -12.51906f);
		} else if (flag == 2) {
			player1.transform.position = new Vector2 (-30f, -12.51906f);
			player2.transform.position = new Vector2 (78f, -12.51906f);
		}
		if(!p1skip)
			controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = "";
		controller.transform.FindChild ("p1keyInput").GetComponent<Text> ().text = "";
		if(!p2skip)
			controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = "";
		controller.transform.FindChild ("p2keyInput").GetComponent<Text> ().text = "";

		if (lastMove == "block") {
			controller.transform.FindChild ("LB").GetComponent<Image> ().color = Color.white;
			controller.transform.FindChild ("LBt").GetComponent<Text> ().text = "";
		} else if (lastMove == "rockets") {
			controller.transform.FindChild ("RT").GetComponent<Image> ().color = Color.white;
			controller.transform.FindChild ("FB").GetComponent<Image> ().color = Color.white;
			controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
			controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "";
		} else if (lastMove == "pickUp") {
			controller.transform.FindChild ("RB").GetComponent<Image> ().color = Color.white;
			controller.transform.FindChild ("RBt").GetComponent<Text> ().text = "";
		} else if (lastMove == "normal") {
			controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
			controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		}

		keyPressed = false;
		keyPressed2 = false;
	}

	public void thePrompt(string theMove, string p1key, string p2key)
	{
		if (!p1skip) {
			controller.transform.FindChild ("p1Prompt").GetComponent<Text> ().text = theMove;
			controller.transform.FindChild ("p1keyInput").GetComponent<Text> ().text = p1key;
		}
	
		if (!p2skip) {
			controller.transform.FindChild ("p2Prompt").GetComponent<Text> ().text = theMove;
			controller.transform.FindChild ("p2keyInput").GetComponent<Text> ().text = p2key;
		}

	}

	IEnumerator controllerPrompt(string moveType, string keyText, Color theColor)
	{
		 if (moveType == "rockets") {
			controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.red;
			controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Hold Down";
			yield return new WaitForSeconds(1.0f);
			controller.transform.FindChild("FB").GetComponent<Image> ().color = theColor;
			controller.transform.FindChild ("FBt").GetComponent<Text> ().text = keyText;
		} else if (moveType == "normal") {
			controller.transform.FindChild("FB").GetComponent<Image> ().color = theColor;
			controller.transform.FindChild ("FBt").GetComponent<Text> ().text = keyText;
		}
		yield return null;
	}
	
	IEnumerator tutorial()
	{   
		controller.transform.FindChild ("title").GetComponent<Text> ().text = "Tutorial Time!";
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild ("title").GetComponent<Text> ().text = "Follow The Instructions";
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild ("title").GetComponent<Text> ().text = "";

		//the block
		yield return new WaitForSeconds(0.5f);
		thePrompt ("Blocking", "Left Shift", "Right Shift");
		controller.transform.FindChild ("LB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("LBt").GetComponent<Text> ().text = "Hold Down";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForHoldKey (KeyCode.LeftShift, KeyCode.RightShift, "LeftBumper_P1", "LeftBumper_P2", "block", "unblock"));
		
		//the RocketLeftArm
		reSet (1, "block");
		yield return new WaitForSeconds(1.0f);
		thePrompt ("Rocket Left Arm!", "5", "=(Equals)");
		yield return StartCoroutine(controllerPrompt ("rockets", "X", Color.blue));
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha5, KeyCode.Equals, "X_P1", "X_P2", "RocketLeftArm"));
		yield return new WaitForSeconds(2.5f);

		//the RocketLeftLeg
		reSet (2, "rockets");
		yield return new WaitForSeconds(1.0f);
		thePrompt ("Rocket Left Leg!", "T", "P");
		yield return StartCoroutine(controllerPrompt ("rockets", "A", Color.green));
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.T, KeyCode.P, "A_P1", "A_P2", "RocketLeftLeg"));
		yield return new WaitForSeconds(1.5f);

		//the RocketRightArm
		reSet (2, "rockets");
		yield return new WaitForSeconds(1.0f);
		thePrompt ("Rocket Right Arm!", "R", "O");
		yield return StartCoroutine(controllerPrompt ("rockets", "Y", Color.yellow));
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.R, KeyCode.O, "Y_P1", "Y_P2", "RocketRightArm"));
		yield return new WaitForSeconds(1.5f);

		//the RocketRightLeg
		reSet (1, "rockets");
		yield return new WaitForSeconds(1.0f);

		thePrompt ("Rocket Right Leg!", "Y", ":(Semicolon)");

		yield return StartCoroutine(controllerPrompt ("rockets", "B", Color.red));
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.Y, KeyCode.Semicolon, "B_P1", "B_P2", "RocketRightLeg"));
		yield return new WaitForSeconds(1.5f);

		//the pick up
		reSet (1, "rockets");
		yield return new WaitForSeconds(1.5f);
		GameObject pickObj1 = null;
		GameObject pickObj5 = null;
		if (!p1skip) {
			player1.robotParts [1].Attach ();
			player1.robotParts [2].Attach ();
			player1.robotParts [3].Attach ();
			pickObj1 = Instantiate (Resources.Load ("ItemParts/TigerLeftHandPickup")) as GameObject;
			pickObj1.transform.position = new Vector2 (player1.transform.position.x + 5, player1.transform.position.y);
		}
		if (!p2skip) {
			player2.robotParts [1].Attach ();
			player2.robotParts [2].Attach ();
			player2.robotParts [3].Attach ();
			pickObj5 = Instantiate (Resources.Load ("ItemParts/BunnyLeftHandPickup")) as GameObject;
			pickObj5.transform.position = new Vector2 (player2.transform.position.x - 5, player2.transform.position.y);
			pickObj5.transform.Rotate(new Vector3(0, 0, 180));
		}
		yield return new WaitForSeconds(1.0f);

		thePrompt ("Pick Up Your losing Part", "E", "U");
		controller.transform.FindChild("RB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("RBt").GetComponent<Text> ().text = "Press";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.E, KeyCode.U, "RightBumper_P1", "RightBumper_P2", "pickUp"));

		yield return new WaitForSeconds(0.5f);


		//left punch
		reSet (1,"pickUp");
		thePrompt ("Left Punch", "1", "7");
		yield return StartCoroutine(controllerPrompt ("normal", "X", Color.blue));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha1, KeyCode.Alpha7, "X_P1", "X_P2","leftPunch"));
		yield return new WaitForSeconds(0.5f);


		//right punch
		reSet (1,"normal");
		yield return new WaitForSeconds(1.0f);

		thePrompt ("Right Punch", "2", "8");
		yield return StartCoroutine(controllerPrompt ("normal", "Y", Color.yellow));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha2, KeyCode.Alpha8, "Y_P1", "Y_P2","rightPunch"));
		yield return new WaitForSeconds(1.5f);


		//left kick
		reSet (1,"normal");
		yield return new WaitForSeconds(1.0f);
		thePrompt ("Left Kick", "3", "9");
		yield return StartCoroutine(controllerPrompt ("normal", "A", Color.green));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha3, KeyCode.Alpha9, "A_P1", "A_P2", "leftKick"));
		yield return new WaitForSeconds(2.0f);


		//right kick
		reSet (1,"normal");
		yield return new WaitForSeconds(1.0f);
		thePrompt ("Right Kick", "4", "0");
		yield return StartCoroutine(controllerPrompt ("normal", "B", Color.red));
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha4, KeyCode.Alpha0, "B_P1", "B_P2", "rightKick"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		yield return new WaitForSeconds(1.0f);

		controller.transform.FindChild ("title").GetComponent<Text> ().text = "Fantastic!";
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild ("title").GetComponent<Text> ().text = "Game Loading...";
		Application.LoadLevel (sceneN);
	}
}
