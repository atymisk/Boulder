using UnityEngine;
using System.Collections;

public class RobotRocket : MonoBehaviour {
    public Robot owner;

    private Rigidbody2D rigidBodyTwoD;
    private IEnumerator rotateTimeRoutine;
    // Use this for initialization
    void Start()
    {
        rigidBodyTwoD = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetOwner(Robot owner)
    {
        this.owner = owner;
    }

    //Collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            if(rotateTimeRoutine != null)
            {
                StopCoroutine(rotateTimeRoutine);
            }
        }

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
                float damage = 25;
                float speed = 100;
                float direction = owner.IsFacingLeft() ? -1 : 1;
                Vector2 pushVelocity = new Vector2(direction * speed, 50);
                enemy.HeavyHitStun(damage, pushVelocity, 0.2f);

                GameObject sparks = (GameObject)Resources.Load("Particles/HitEffect");
                var clone = Instantiate(sparks, enemy.transform.position, Quaternion.identity);
                Destroy(clone, sparks.GetComponent<ParticleSystem>().startLifetime);

                rigidBodyTwoD.velocity = new Vector2(-10, 50);
                rotateTimeRoutine = RotateOverTime(-1500, 3);
                StartCoroutine(rotateTimeRoutine);

                //Instantiate(Resources.Load("RocketParts/TigerLeftHandRocket"));

                SpecialEffects.instance.SlowMo(0.1f, 0.1f);
                SpecialEffects.instance.ShakeScreen(0.1f);

                //Destroy(this.gameObject);
                this.gameObject.SetActive(false);
            }
        }

    }

    //Coroutines
    IEnumerator RotateOverTime(float angleDelta, float duration)
    {
        yield return new WaitForEndOfFrame();

        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime = currentTime + Time.deltaTime;
            this.transform.Rotate(new Vector3(0, 0, angleDelta * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
    }
}
