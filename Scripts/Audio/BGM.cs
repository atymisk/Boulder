using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    private static BGM instance = null;
    public static BGM Instance { get { return instance; } }
    private static AudioSource bgm;
    private static AudioClip ac;

	// Use this for initialization
	void Awake ()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        bgm = (AudioSource)gameObject.AddComponent<AudioSource>();
        ac = (AudioClip)Resources.Load("Audio/Music/VG Music Demo 1");
        bgm.clip = ac;
        bgm.loop = true;
        bgm.Play();
    }

    void OnLevelWasLoaded(int level)
    {
        print(level);
        if(level > 3)
        {
            bgm.Stop();
        }
        else if(!bgm.isPlaying)
        {
            bgm.Play();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
