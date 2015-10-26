using UnityEngine;
using System.Collections;

public abstract class HitBox : MonoBehaviour 
{

	protected Character owner;
    protected BoxCollider2D collider;

	abstract public void OnHitConnected(Character enemy);

	protected void Initialize()
	{
		owner = this.transform.parent.parent.gameObject.GetComponent<Character>();
        collider = this.gameObject.GetComponent<BoxCollider2D>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		HurtBox hurtBox = other.gameObject.GetComponent<HurtBox> ();
		if (hurtBox) 
		{
			Character enemy = hurtBox.GetCharacter();

			if(enemy != owner)
			{
				OnHitConnected(enemy);
			}
		}

	}
}
