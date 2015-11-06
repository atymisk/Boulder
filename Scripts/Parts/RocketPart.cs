using UnityEngine;
using System.Collections;

public class RocketPart : MonoBehaviour
{
    public Character owner;

    private Rigidbody2D rigidBodyTwoD;
	// Use this for initialization
	void Start () {
        rigidBodyTwoD = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnROcketHit");
        HurtBox hurtBox = other.gameObject.GetComponent<HurtBox>();
        if (hurtBox)
        {
            Character enemy = hurtBox.GetCharacter();
            Debug.Log("hit " + enemy);
            if (enemy != owner)
            {
                this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                float damage = 50;
                float speed = 200;
                float direction = owner.IsFacingLeft() ? -1 : 1;
                Vector2 pushVelocity = new Vector2(direction * speed, 0);
                enemy.HeavyHitStun(damage, pushVelocity, 2);
                rigidBodyTwoD.velocity = new Vector2(0, 0);

                SpecialEffects.instance.SlowMo(0.1f, 0.1f);
                SpecialEffects.instance.ShakeScreen(0.1f);
                this.gameObject.SetActive(false);
            }
        }

    }

    void PushEnemy()
    {

    }
}
