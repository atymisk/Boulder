using UnityEngine;
using System.Collections;

public class PartPickup : MonoBehaviour {

    public Rigidbody2D rigidBodyTwoD;
    private IEnumerator rotateTimeRoutine;
    // Use this for initialization
    
    //Collisions
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform")
        {
            if (rotateTimeRoutine != null)
            {
                StopCoroutine(rotateTimeRoutine);
            }
        }

    }

    public void SpinBounce(int direction)
    {
        rigidBodyTwoD = this.GetComponent<Rigidbody2D>();
        rigidBodyTwoD.velocity = new Vector2(-10, 50);
        rotateTimeRoutine = RotateOverTime(-1500, 3);
        StartCoroutine(rotateTimeRoutine);
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
