using UnityEngine;
using System.Collections;

public class TigerLeftKick : Part {

    private IEnumerator flashKickRoutine;

    void Start()
    {
        base.Initialize();
    }

    public override void Attack()
    {
        if (!owner.IsBusy())
        {
            flashKickRoutine = FlashKickRoutine();
            StartCoroutine(flashKickRoutine);
        }
    }

    public override void CancelAttack()
    {
        DisableHitBox();

        if (flashKickRoutine != null)
        {
            StopCoroutine(flashKickRoutine);
        }
    }

    override public void OnHitConnected(Robot enemy)
    {
        float damage = 10;
        float speed = 80;
        float duration = 0.1f;
        float facing = owner.IsFacingLeft() ? -1 : 1;
        Vector2 direction = new Vector2(facing, 3);
        direction.Normalize();
        Vector2 pushVelocity = direction * speed;
        enemy.HeavyHitStun(damage, pushVelocity, duration);
        collider.enabled = false;
    }

    public override string GetTrigger()
    {
        return "TigerLeftKick";
    }

    //Coroutines
    IEnumerator FlashKickRoutine()
    {
        yield return new WaitForFixedUpdate();

        float elapseTime = 0;
        while(elapseTime < 0.05f)
        {
            elapseTime = elapseTime + Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        float speed = 75;
        float facing = owner.IsFacingLeft() ? -1 : 1;
        Vector2 direction = new Vector2(facing, 3);
        direction.Normalize();
        Vector2 velocity = direction * speed;

        elapseTime = 0;
        while (elapseTime < 0.02f)
        {
            Debug.Log("step");
            float xDisplacement = velocity.x * Time.deltaTime;
            float yDisplacement = velocity.y * Time.deltaTime;
            float zPosition = this.transform.position.z;

            Vector3 displacement = new Vector3(xDisplacement, yDisplacement, zPosition);
            /*
            owner.transform.position = new Vector3(xDisplacement + owner.transform.position.x,
                                                  yDisplacement + owner.transform.position.y,
                                                  owner.transform.position.z);
            */
            owner.rigidbodyTwoD.MovePosition(owner.transform.position + displacement);

            elapseTime = elapseTime + Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        elapseTime = 0;
        while (elapseTime < 0.25f)
        {
            elapseTime = elapseTime + Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

        elapseTime = 0;
        while (elapseTime < 0.02)
        {
            float xDisplacement = velocity.x * Time.deltaTime;
            float yDisplacement = velocity.y * Time.deltaTime;
            float zPosition = this.transform.position.z;

            Vector3 displacement = new Vector3(xDisplacement, yDisplacement, zPosition);
            /*
            owner.transform.position = new Vector3(xDisplacement + owner.transform.position.x,
                                                  yDisplacement + owner.transform.position.y,
                                                  owner.transform.position.z);
            */
            owner.rigidbodyTwoD.MovePosition(owner.transform.position + displacement);

            elapseTime = elapseTime + Time.deltaTime;

            yield return new WaitForFixedUpdate();
        }

    }
}
