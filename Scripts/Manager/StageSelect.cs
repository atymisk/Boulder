using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StageSelect : MonoBehaviour {
	public static StageSelect instance = null;
	
	public GameObject screen;
	public GameObject controller;
	public Robot player1;
	public Robot player2;
	public Text skipText;
	private string sceneN;
	private bool keyPressed = false;
	private bool keyPressed2 = false;
	
	void Start()
	{
		controller.SetActive (false);
		instance = this;
	}
	
	void Update()
	{
		skip ();
	}
	
	public void skip()
	{
		//to skip the tutorial
		//if (Input.GetKeyDown(KeyCode.C)|| Input.GetButton("Start_P1")|| Input.GetButton ("Start_P2"))
		if (Input.GetKeyDown (KeyCode.Space))
		{
			Application.LoadLevel (sceneN);
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

	IEnumerator waitForKey(KeyCode key, KeyCode key2, string theMove)
	{
		while(!keyPressed || !keyPressed2){
			if(Input.GetKeyDown(key))
			{
				doMove(player1, theMove);
				keyPressed = true;
			}
			else if(Input.GetKeyDown(key2))
			{
				doMove(player2, theMove);
				keyPressed2 = true;
			}
			yield return null;
		}
	}

	public void doMove(Robot player, string theMove)
	{
		if (theMove == "block")
			player.Block ();
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

	public void reSet(int flag)
	{
		if (flag == 1) {
			player1.transform.position = new Vector2 (-17f, -12.51906f);
			player2.transform.position = new Vector2 (64f, -12.51906f);
		} else if (flag == 2) {
			player1.transform.position = new Vector2 (-30f, -12.51906f);
			player2.transform.position = new Vector2 (78f, -12.51906f);
		}
		keyPressed = false;
		keyPressed2 = false;
	}
	
	IEnumerator tutorial()
	{   
		//the block
		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild ("LB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("LBt").GetComponent<Text> ().text = "Block";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.LeftShift, KeyCode.RightShift, "block"));
		yield return new WaitForSeconds(2.0f);
		controller.transform.FindChild("LB").GetComponent<Image> ().color = Color.white;
		yield return new WaitForSeconds(0.5f);
		player1.UnBlock ();
		player2.UnBlock ();
		controller.transform.FindChild ("LBt").GetComponent<Text> ().text = "Left bumper";
		
		//the RocketLeftArm
		reSet (1);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.blue;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "X";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Fire Rocket";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha5, KeyCode.Equals, "RocketLeftArm"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Right trigger";

		//the RocketRightArm
		reSet (2);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.yellow;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "Y";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Fire Rocket";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.R, KeyCode.O, "RocketRightArm"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Right trigger";

		//the RocketLeftLeg
		reSet (2);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.green;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "A";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Fire Rocket";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.T, KeyCode.P, "RocketLeftLeg"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Right trigger";

		//the RocketRightLeg
		reSet (1);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "B";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Fire Rocket";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.Y, KeyCode.Semicolon, "RocketRightLeg"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("RT").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		controller.transform.FindChild ("RTt").GetComponent<Text> ().text = "Right trigger";

		//the pick up
		reSet (1);
		yield return new WaitForSeconds(1.5f);
		GameObject pickObj1 = null;
		GameObject pickObj5 = null;

		player1.robotParts [1].Attach ();
		player1.robotParts [2].Attach ();
		player1.robotParts [3].Attach ();
		player2.robotParts [1].Attach ();
		player2.robotParts [2].Attach ();
		player2.robotParts [3].Attach ();

		pickObj1 = Instantiate(Resources.Load("ItemParts/TigerLeftHandPickup")) as GameObject;
		pickObj1.transform.position = new Vector2 (player1.transform.position.x + 5, player1.transform.position.y);
		pickObj5 = Instantiate(Resources.Load("ItemParts/BunnyLeftHandPickup")) as GameObject;
		pickObj5.transform.position = new Vector2 (player2.transform.position.x - 5, player2.transform.position.y);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("RBt").GetComponent<Text> ().text = "Pick Up";
		yield return new WaitForSeconds(1.0f);
		yield return StartCoroutine (waitForKey (KeyCode.E, KeyCode.U, "pickUp"));

		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild ("RB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("RBt").GetComponent<Text> ().text = "Right bumper";
		yield return new WaitForSeconds(1.0f);

		//left punch
		reSet (1);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.blue;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "X";
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha1, KeyCode.Alpha7, "leftPunch"));
		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		//right punch
		reSet (1);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.yellow;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "Y";
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha2, KeyCode.Alpha8, "rightPunch"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		//left kick
		reSet (1);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.green;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "A";
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha3, KeyCode.Alpha9, "leftKick"));
		yield return new WaitForSeconds(2.0f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		//right kick
		reSet (1);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.red;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = "B";
		yield return new WaitForSeconds(0.5f);
		yield return StartCoroutine (waitForKey (KeyCode.Alpha4, KeyCode.Alpha0, "rightKick"));
		yield return new WaitForSeconds(1.5f);
		controller.transform.FindChild("FB").GetComponent<Image> ().color = Color.white;
		controller.transform.FindChild ("FBt").GetComponent<Text> ().text = " ";
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel (sceneN);
	}
}
