using UnityEngine;
using System.Collections;

public abstract class Part : MonoBehaviour
{
    public bool active = true;

    protected Robot owner;
    protected BoxCollider2D collider;

    public abstract void Attack();
    public abstract void CancelAttack();
    public abstract string GetTrigger();
    public abstract void OnHitConnected(Robot enemy);

    protected void Initialize()
    {
        owner = this.transform.parent.parent.gameObject.GetComponent<Robot>();
        collider = gameObject.GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit enemy");
        RobotHurtBox hurtBox = other.gameObject.GetComponent<RobotHurtBox>();
        if (hurtBox)
        {
            Robot enemy = hurtBox.GetRobot();

            if (enemy != owner)
            {
                OnHitConnected(enemy);
                GameObject sparks = (GameObject)Resources.Load("Particles/HitEffect");
                var clone = Instantiate(sparks, enemy.transform.position, Quaternion.identity);
                Destroy(clone, sparks.GetComponent<ParticleSystem>().startLifetime);
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
