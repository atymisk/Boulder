using UnityEngine;
using System.Collections;

public class HeadHitBox : MonoBehaviour {
	Robot owner;


	void Start()
	{
		owner = this.transform.parent.GetComponent<Robot> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		RobotHurtBox hurtBox = other.gameObject.GetComponent<RobotHurtBox>();
		if (hurtBox)
		{
			Robot enemy = hurtBox.GetRobot();
			if (enemy != owner && enemy.GetCurrentState() != Robot.CharacterState.RightKick)
			{
				//this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
				float damage = 25;
				float speed = 90;
				float direction = owner.IsFacingLeft() ? -1 : 1;
				Vector2 pushVelocity = new Vector2(direction * speed, 50);
				enemy.HeavyHitStun(damage, pushVelocity, 0.2f);

				GameObject sparks = (GameObject)Resources.Load("Particles/HitEffect");
				var clone = Instantiate(sparks, enemy.transform.position, Quaternion.identity);
				Destroy(clone, sparks.GetComponent<ParticleSystem>().startLifetime);

				enemy.BreakRandomPart();

				SpecialEffects.instance.SlowMo(0.1f, 0.1f);
				SpecialEffects.instance.ShakeScreen(0.1f);
				
				// this.gameObject.SetActive(false);
			}
		}
	}
}
