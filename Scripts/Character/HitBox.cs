using UnityEngine;
using System.Collections;

public abstract class HitBox : MonoBehaviour 
{

	protected Character owner;

	abstract public void OnHitConnected(Character enemy);

	protected void Initialize()
	{
		owner = this.transform.parent.parent.gameObject.GetComponent<Character>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		HurtBox hurtBox = other.gameObject.GetComponent<HurtBox> ();
		if (hurtBox) 
		{
			Character enemy = hurtBox.GetCharacter();

			if(enemy != owner)
			{
				Debug.Log(enemy.ToString());
				OnHitConnected(enemy);
			}
		}

	}
}
