using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeScene : MonoBehaviour {
    public static ChangeScene instance = null;

	public GameObject screen;
	public GameObject controller;
	public Robot player;
	public Text skipText;
	private string sceneN;

    void Start()
    {
        instance = this;
    }

	void Update()
	{
		skip ();
	}

	public void skip()
	{
		//to skip the tutorial
		//if (Input.GetKeyDown(KeyCode.C)|| Input.GetButton("A_P1")|| Input.GetButton ("A_P2"))
		if (Input.GetKeyDown (KeyCode.C))
		{
			Application.LoadLevel (sceneN);
		}
	}

	public void ChangetoScene (string sceneName) {
		sceneN = sceneName;
		screen.SetActive (false);
		skipText.text = "C(keyboard) or A(controller) to skip";
		StartCoroutine(tutorial ());
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

	IEnumerator tutorial()
	{   
		//the block
		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild("LB").GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(1.0f);
		player.Block ();
		yield return new WaitForSeconds(2.0f);
		controller.transform.FindChild("LB").GetComponent<Renderer>().material.color = Color.white;
		yield return new WaitForSeconds(0.5f);
		player.UnBlock ();

		//the rocket
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RT").GetComponent<Renderer>().material.color = Color.red;
		//controller.transform.FindChild("LT").GetComponent<Renderer>().material.color = Color.red;
		controller.transform.FindChild("FB").GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(1.0f);
		player.RocketLeftArm ();
		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild("RT").GetComponent<Renderer>().material.color = Color.white;
		//controller.transform.FindChild("LT").GetComponent<Renderer>().material.color = Color.white;
		controller.transform.FindChild("FB").GetComponent<Renderer>().material.color = Color.white;
		//the pick up
		yield return new WaitForSeconds(1.5f);
		GameObject pickObj = null;
		pickObj = Instantiate(Resources.Load("ItemParts/TigerLeftHandPickup")) as GameObject;
		pickObj.transform.position = new Vector2 (player.transform.position.x + 5, player.transform.position.y);
		yield return new WaitForSeconds(1.0f);
		controller.transform.FindChild("RB").GetComponent<Renderer>().material.color = Color.red;
		yield return new WaitForSeconds(1.0f);
		player.Pickup ();
		yield return new WaitForSeconds(0.5f);
		controller.transform.FindChild ("RB").GetComponent<Renderer> ().material.color = Color.white;
		yield return new WaitForSeconds(1.0f);
		Application.LoadLevel (sceneN);
	}
}
