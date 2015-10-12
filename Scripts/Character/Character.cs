using UnityEngine;
using System.Collections;

public abstract class Character : MonoBehaviour 
{
	private bool isFacingLeft = true;

	protected float hitPoints = 100;
	protected MoveEventHandler moveHandler;

	protected void Initialize(float hitPoints)
	{
		this.hitPoints = hitPoints;
		moveHandler = this.gameObject.GetComponent<MoveEventHandler>();

		//This is backwards since our prefabis facing left by default
		if(this.transform.right.x > 0)
		{
			isFacingLeft = true;
		}
		else
		{
			isFacingLeft = false;
		}
	}

	virtual public void NormalMoveAlpha()
	{
		moveHandler.OnNormalAlphaStart();
	}

	virtual public void SpecialMoveAlpha()
	{
		moveHandler.OnSpecialAlphaStart();
	}

	virtual public void LightHitStun()
	{
		moveHandler.OnLightHitStart();
	}

	public float GetHitPoints()
	{
		return hitPoints;
	}

	public void FaceLeft()
	{
		if(!isFacingLeft && !moveHandler.IsBusy())
		{
			this.transform.Rotate(new Vector3(0,180,0));
			isFacingLeft = true;
		}
	}

	public void FaceRight()
	{
		if(isFacingLeft && !moveHandler.IsBusy())
		{
			this.transform.Rotate(new Vector3(0,-180,0));
			isFacingLeft = false;
		}
	}

	public bool IsFacingLeft()
	{
		return isFacingLeft;
	}
}
