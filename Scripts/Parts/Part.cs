using UnityEngine;
using System.Collections;

public abstract class Part : MonoBehaviour
{
    public bool active = true;

    protected Robot owner;
    protected Collider2D collider;

    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract string GetTrigger();

    protected void Initialize()
    {
        owner = this.transform.parent.parent.gameObject.GetComponent<Robot>();
        collider = gameObject.GetComponent<Collider2D>();
    }

    public virtual void OnHitConnected(Robot enemy)
    {
        float damage = 10;
        float speed = 50;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.HeavyHitStun(damage, pushVelocity, 0.2f);
        collider.enabled = false;
    }

    public virtual void OnHitBlocked(Robot enemy)
    {
        float speed = 30;
        float duration = 0.2f;
        float direction = owner.IsFacingLeft() ? -1 : 1;
        Vector2 pushVelocity = new Vector2(direction * speed, 0);
        enemy.BlockStun(pushVelocity, duration);
        collider.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        RobotHurtBox hurtBox = other.gameObject.GetComponent<RobotHurtBox>();
        if (hurtBox)
        {
            Robot enemy = hurtBox.GetRobot();

            if (enemy != owner)
            {
                if(enemy.GetCurrentState() != Robot.CharacterState.Blocking)
                {
                    Debug.Log("hitconnected");
                    OnHitConnected(enemy);
                    GameObject sparks = (GameObject)Resources.Load("Particles/HitEffect");
                    var clone = Instantiate(sparks, enemy.transform.position, Quaternion.identity);
                    Destroy(clone, sparks.GetComponent<ParticleSystem>().startLifetime);
                }
                else
                {
                    OnHitBlocked(enemy);
                }

                SpecialEffects.instance.SlowMo(0.1f, 0.1f);
            }
            
        }

    }

    public void EnableHitBox()
    {
        collider.enabled = true;
    }

    public void DisableHitBox()
    {
        collider.enabled = false;
    }
}
