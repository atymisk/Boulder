using UnityEngine;
using System.Collections;

public class RightPunchExplosion : MonoBehaviour {
    private Robot owner;
    private float hitboxLifeTime = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        hitboxLifeTime = hitboxLifeTime + Time.deltaTime;

        if (hitboxLifeTime >= 0.25f)
        {
            this.GetComponent<CircleCollider2D>().enabled = false;
        }
	}

    public void SetOwner(Robot robot)
    {
        owner = robot;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RobotHurtBox hurtBox = other.gameObject.GetComponent<RobotHurtBox>();
        if (hurtBox)
        {
            Robot enemy = hurtBox.GetRobot();
            if (enemy != owner)
            {
                //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                float damage = 15;
                float speed = 100;
                float direction = owner.IsFacingLeft() ? -1 : 1;
                Vector2 pushVelocity = new Vector2(direction * speed, -25);
                enemy.HeavyHitStun(damage, pushVelocity, 0.2f);
            }
        }
    }
}
