using UnityEngine;
using System.Collections;

public class MoveEventHandler : MonoBehaviour 
{


	//Private members
	bool onPunch = true;
	bool onFlyingKick = true;
	Animator anim;

	void Start () 
	{
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetKeyDown(KeyCode.U))
		{
			OnPunchStart();
		}
		else if(Input.GetKeyDown (KeyCode.I))
		{
			OnFlyingKickStart();
		}
	}
	
	//Events
	public void OnPunchStart()
	{
		Debug.Log("Punch");
		onPunch = true;
		anim.SetBool("OnPunch", onPunch);
	}
	
	public void OnPunchEnd()
	{
		Debug.Log("OnPunchEnd");
		onPunch = false;
		anim.SetBool("OnPunch", onPunch);
	}
	
	public void OnFlyingKickStart()
	{
		Debug.Log ("FlyingKick");
		onFlyingKick = true;
		anim.SetBool("OnFlyingKick", onFlyingKick);
		Vector2 speed = new Vector2(-30, 0);
		StartCoroutine(MoveOverTime(speed));
	}
	
	public void OnFlyingKickEnd()
	{
		onFlyingKick = false;
		Debug.Log("OnFlyingKickEnd " + onFlyingKick.ToString());
		anim.SetBool("OnFlyingKick", onFlyingKick);
	}
	
	//Coroutines
	IEnumerator MoveOverTime(Vector2 speed)
	{
		while(onFlyingKick)
		{
			float xDisplacement = speed.x * Time.deltaTime;
			float yDisplacement = speed.y * Time.deltaTime;
			this.transform.position = new Vector3(xDisplacement + this.transform.position.x, 
												  yDisplacement + this.transform.position.y, 
												  this.transform.position.z);
												  
			Debug.Log("moving " + this.transform.position.ToString());
			yield return new WaitForEndOfFrame();
		}
	}
}
