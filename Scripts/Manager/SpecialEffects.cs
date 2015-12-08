using UnityEngine;
using System.Collections;

public class SpecialEffects : MonoBehaviour
{
    public static SpecialEffects instance;

    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

	public void SlowMo(float scale, float duration)
    {
        StartCoroutine(SlowEveryThing(scale, duration));
    }

    public void ShakeScreen(float s)
    {
        float duration = s;
        StartCoroutine(ShakeRoutine(duration));
    }

    //Coroutines
    IEnumerator SlowEveryThing(float scale, float duration)
    {
        Time.timeScale = scale;
        yield return new WaitForSeconds(duration*scale);
        Time.timeScale = 1.0f;

    }

    IEnumerator ShakeRoutine(float duration)
    {
        Vector3 originalPos = Camera.main.transform.position;
        System.Random rnd = new System.Random();
        

        float currentTime = 0;
        while (currentTime < duration)
        {

            double randNum = rnd.NextDouble();
            
            double xMin = originalPos.x - Camera.main.pixelWidth * 0.005;
            double xMax = originalPos.x + Camera.main.pixelWidth * 0.005;
            double xDiff = xMax - xMin;
            float xRand = (float)(xMin + xDiff * randNum);

            randNum = rnd.NextDouble();

            double yMin = originalPos.y - Camera.main.pixelWidth * 0.005;
            double yMax = originalPos.y + Camera.main.pixelWidth * 0.005;
            double yDiff = yMax - yMin;
            float yRand = (float)(yMin + yDiff * randNum);

            Vector2 currentPos = Camera.main.transform.position;
            Vector2 startPos = currentPos;
            Vector2 endPos = new Vector2(xRand, yRand);
            Vector2 direction = (endPos - startPos);
            direction.Normalize();

            while(currentPos != endPos && currentTime < duration)
            {
                currentPos = Vector2.MoveTowards(currentPos, endPos, 3.0f);
                Camera.main.transform.position = new Vector3(currentPos.x, currentPos.y, originalPos.z);
                currentTime = currentTime + Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            
            yield return new WaitForEndOfFrame();
        }

        Camera.main.transform.position = originalPos;
    }

    void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
