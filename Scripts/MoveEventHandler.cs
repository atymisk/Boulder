using UnityEngine;
using System.Collections;

public class MoveEventHandler : MonoBehaviour 
{


	//Private members
	bool onPunch = true;
	Animator anim;

	void Start () 
	{
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Punch");
			onPunch = true;
			anim.SetBool("OnPunch", true);
			//anim.Play("Punch");
			//anim.wrapMode = WrapMode.Once;
		}
	}

	public void OnPunch()
	{
		onPunch = false;
		anim.SetBool("OnPunch", false);
	}
}
