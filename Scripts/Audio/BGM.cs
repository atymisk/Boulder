using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour
{
    private static BGM instance = null;
    public static BGM Instance { get { return instance; } }
    private static AudioSource bgm;
    private static AudioSource wind;
    private static AudioClip ac;
    private static AudioClip mainbgm;
    private static int previous = 0;

    private float mainvolume = 0.6f;
    private float uivolume = 0.7f;

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
        wind = (AudioSource)gameObject.AddComponent<AudioSource>();
        wind.loop = true;
        wind.volume = 1;
        wind.priority = 1;
        wind.clip = (AudioClip)Resources.Load("Audio/FX/BB Wind");
        ac = (AudioClip)Resources.Load("Audio/Music/LEVEL BGM FINAL");
        mainbgm = (AudioClip)Resources.Load("Audio/Music/Breaking Bots BGM Final");
        bgm.clip = ac;
        bgm.loop = true;
        bgm.volume = uivolume;
        bgm.Play();
    }

    void OnLevelWasLoaded(int level)
    {
        if(level > 3)
        {
            bgm.Stop();
            bgm.clip = mainbgm;
            wind.Play();
            bgm.volume = mainvolume;
            bgm.Play();
        }
        else if(level < 4 && previous > 3)
        {
            bgm.Stop();
            wind.Stop();
            bgm.clip = ac;
            bgm.volume = uivolume;
            bgm.Play();
        }
        else if(!bgm.isPlaying)
        {
            bgm.Play();
        }
        previous = level;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
