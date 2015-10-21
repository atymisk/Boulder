using UnityEngine;
using System.Collections;

public class MoveEventHandler : MonoBehaviour 
{


	//Private members
	private bool onNormalAlpha = false;
	private bool onNormalBeta = false;
	private bool onSpecialAlpha = false;
	private bool onLightHit = false;
	private bool onHeavyHit = false;
	private Animator anim;

	void Start () 
	{
		anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{	

	}
	
	//Events
	public void OnNormalAlphaStart()
	{
		onNormalAlpha = true;
		anim.SetBool("OnNormalAlpha", onNormalAlpha);
	}

	public void OnNormalAlphaEnd()
	{
		onNormalAlpha = false;
		anim.SetBool("OnNormalAlpha", onNormalAlpha);
	}
	
	public void OnNormalBetaStart()
	{
		onNormalBeta = true;
		anim.SetBool("OnNormalBeta", onNormalBeta);
	}
	
	public void OnNormalBetaEnd()
	{
		onNormalBeta = false;
		anim.SetBool("OnNormalBeta", onNormalBeta);
	}

	public void OnSpecialAlphaStart()
	{
		onSpecialAlpha = true;
		anim.SetBool("OnSpecialAlpha", onSpecialAlpha);
		Vector2 speed = new Vector2(-30, 0);
	}

	public void OnSpecialAlphaEnd()
	{
		onSpecialAlpha = false;
		anim.SetBool("OnSpecialAlpha", onSpecialAlpha);
	}

	public void OnLightHitStart()
	{
		onLightHit = true;
		onHeavyHit = false;
		onNormalAlpha = false;
		onSpecialAlpha = false;
		
		anim.SetBool ("OnLightHit", onLightHit);
		anim.SetBool("OnHeavyHit", onHeavyHit);
		anim.SetBool ("OnNormalAlpha", onNormalAlpha);
		anim.SetBool ("OnSpecialAlpha", onSpecialAlpha);
		anim.SetTrigger("OnTriggerLightHit");
	}

	public void OnLightHitEnd()
	{
		onLightHit = false;
		Debug.Log ("OnLightHitEnd");
		anim.SetBool ("OnLightHit", onLightHit);
	}
	
	public void OnHeavyHitStart()
	{
		onHeavyHit = true;
		onLightHit = false;
		onNormalAlpha = false;
		onSpecialAlpha = false;
		
		anim.SetBool ("OnLightHit", onLightHit);
		anim.SetBool("OnHeavyHit", onHeavyHit);
		anim.SetBool ("OnNormalAlpha", onNormalAlpha);
		anim.SetBool ("OnSpecialAlpha", onSpecialAlpha);
		anim.SetTrigger("OnTriggerHeavyHit");

	}
	
	public void OnHeavyHitEnd()
	{
		onHeavyHit = false;
		Debug.Log ("OnHeavyHitEnd");
		anim.SetBool ("OnHeavyHit", onLightHit);
	}

	public bool IsBusy()
	{
		return onNormalAlpha || onNormalBeta || onSpecialAlpha || onLightHit;
	}
}
