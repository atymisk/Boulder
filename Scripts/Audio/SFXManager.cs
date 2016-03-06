using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{
    public Transform p1pos;
    public Transform p2pos;

    static AudioSource p1;
    static AudioSource p1extra;
    static AudioSource p2;
    static AudioSource p2extra;

    public bool p1thrust_on = false;
    public bool p2thrust_on = false;
    static AudioSource p1Thruster;
    static AudioSource p2Thruster;

    public bool notrespawning1 = false;
    public bool notrespawning2 = false;

    private static AudioClip MissingLimb;

    private static AudioClip pickup1;
    private static AudioClip pickup2;
    private static AudioClip pickup2_long;

    private static AudioClip limbflying1;
    private static AudioClip limbflying2;
    private static AudioClip limbflying3;
    private static AudioClip limbflying4;
    private static AudioClip limbflying5;
    private static AudioClip limbflying6;
    private static AudioClip limbflying7;

    private static AudioClip MetalHit1;
    private static AudioClip MetalHit2;
    private static AudioClip MetalHit3;
    private static AudioClip MetalHitGlass;
    private static AudioClip MetalHitGlassBig;

    private static AudioClip MetalScrape1;
    private static AudioClip MetalScrape2;
    private static AudioClip MetalScrape3;
    private static AudioClip MetalScrapeH1;
    private static AudioClip MetalScrapeH2;
    private static AudioClip MetalScrapePass;

    private static AudioClip StartEndFanFare1;
    private static AudioClip StartEndFanFare2;

    private static AudioClip Thruster1;
    private static AudioClip Thruster2;
    private static AudioClip Thruster3;
    private static AudioClip Thruster4;
    private static AudioClip ThrusterALL;
    private static AudioClip ThrusterALL_V;

    //AudioSource.timeSamples
    private static void Hit(AudioSource ac)
    {
 //       if(ac.clip != null)
//            ac.Stop();
        int r = Random.Range(0, 5);
        switch(r)
        {
            case 0:
                ac.clip = MetalHit1;
                break;
            case 1:
                ac.clip = MetalHit2;
                break;
            case 2:
                ac.clip = MetalHit3;
                break;
            case 3:
                ac.clip = MetalHitGlass;
                break;
            case 4:
                ac.clip = MetalHitGlassBig;
                break;
            default:
                break;
        }
        ac.Play();
    }

    private static void LostLimb(AudioSource ac)
    {
        //if(ac.clip != null)
        //    ac.Stop();
        ac.clip = limbflying7;
        ac.Play();
    }

    private static void LimbPickup(AudioSource ac)
    {
        //if(ac.clip != null)
        //    ac.Stop();
        ac.clip = pickup2;
        ac.Play();
    }

    private static void Whiff(AudioSource ac)
    {
        //if(ac.clip != null)
        //    ac.Stop();
        int r = Random.Range(0, 6);
        switch(r)
        {
            case 0:
                ac.clip = MetalScrape1;
                break;
            case 1:
                ac.clip = MetalScrape2;
                break;
            case 2:
                ac.clip = MetalScrape3;
                break;
            case 3:
                ac.clip = MetalScrapeH1;
                break;
            case 4:
                ac.clip = MetalScrapeH2;
                break;
            case 5:
                ac.clip = MetalScrapePass;
                break;
            default:
                break;
        }
        ac.Play();
    }

    private static void NoLimb(AudioSource ac)
    {
        ac.clip = MissingLimb;
        ac.Play();
    }

    public static void cueFX(string player, string fxtype)
    {
        AudioSource s;
        AudioSource extra;
        if(player == "P1")
        {
            s = p1;
            extra = p1extra;
        }
        else
        {
            s = p2;
            extra = p2extra;
        }

        switch(fxtype)
        {
            case "Hit":
                Hit(s);
                break;
            case "LimbLost":
                LostLimb(extra);
                break;
            case "LimbGained":
                LimbPickup(s);
                break;
            case "Whiff":
                Whiff(s);
                break;
            case "NoLimb":
                NoLimb(s);
                break;
            default:
                break;
        }
    }

    public static void ThrusterOn(string player)
    {
        AudioSource s = (player == "P1" ? p1Thruster : p2Thruster);
        if (s.isPlaying)
            return;
        s.clip = Thruster4;
        s.Play();
    }

    public static void ThrusterOff(string player)
    {
        AudioSource s = (player == "P1" ? p1Thruster : p2Thruster);
        if(s.isPlaying)
            s.Stop();
    }

    // Use this for initialization
    void Awake ()
    {
        p1 = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1.transform.position = p1pos.position;

        p2 = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2.transform.position = p2pos.position;

        p1Thruster = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1Thruster.transform.position = p1pos.position;

        p2Thruster = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2Thruster.transform.position = p2pos.position;

        p1extra = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1extra.transform.position = p1pos.position;

        p2extra = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2extra.transform.position = p2pos.position;

        p1Thruster.loop = true;
        p2Thruster.loop = true;

        MissingLimb = (AudioClip)Resources.Load("Audio/FX/Denial No Limb Game Worthy");
        pickup1 = (AudioClip)Resources.Load("Audio/FX/Got Your Limb 1 Short GW");
        pickup2 = (AudioClip)Resources.Load("Audio/FX/Got Your Limb 2 GW");
        pickup2_long = (AudioClip)Resources.Load("Audio/FX/Got Your Limb 2 Long GW");

        limbflying1 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 1 GW");
        limbflying2 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 2 GW");
        limbflying3 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 3 GW");
        limbflying4 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 4 GW");
        limbflying5 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 5 GW");
        limbflying6 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 6 GW");
        limbflying7 = (AudioClip)Resources.Load("Audio/FX/Limb Flying Through Air 7 GW");

        MetalHit1 = (AudioClip)Resources.Load("Audio/FX/Metal Hits and Explosion 1 Game Worthy");
        MetalHit2 = (AudioClip)Resources.Load("Audio/FX/Metal Hits and Explosion 2 Less BOOM Game Worthy");
        MetalHit3 = (AudioClip)Resources.Load("Audio/FX/Metal Hit and Explosion 3 Verb Out GW");
        MetalHitGlass = (AudioClip)Resources.Load("Audio/FX/Metal Hits and Explosion 2 Less BOOM Game Worthy");
        MetalHitGlassBig = (AudioClip)Resources.Load("Audio/FX/Metal Hit and Explosion Huge with Glass GW");

        MetalScrape1 = (AudioClip)Resources.Load("Audio/FX/Metal Scrape 1 Game Worthy");
        MetalScrape2 = (AudioClip)Resources.Load("Audio/FX/Metal Scrape 2 Game Worthy");
        MetalScrape3 = (AudioClip)Resources.Load("Audio/FX/Metal Scrape 3 Game Worthy");
        MetalScrapeH1 = (AudioClip)Resources.Load("Audio/FX/Metal Scrape HIGH 1 Game Worthy");
        MetalScrapeH2 = (AudioClip)Resources.Load("Audio/FX/Metal Scrape HIGH 2 Game Worthy");
        MetalScrapePass = (AudioClip)Resources.Load("Audio/FX/Metal Scrape PASSING Game Worthy");

        StartEndFanFare1 = (AudioClip)Resources.Load("Audio/FX/START OR END OF GAME MAYBE_");
        StartEndFanFare2 = (AudioClip)Resources.Load("Audio/FX/START OR END OF GAME MAYBE_2");

        Thruster1 = (AudioClip)Resources.Load("Audio/FX/Thrusters 1 Verb ");
        Thruster2 = (AudioClip)Resources.Load("Audio/FX/Thrusters 2 Verb ");
        Thruster3 = (AudioClip)Resources.Load("Audio/FX/Thrusters 3 Verb ");
        Thruster4 = (AudioClip)Resources.Load("Audio/FX/Thrusters 4");
        ThrusterALL = (AudioClip)Resources.Load("Audio/FX/Thrusters all 3 GW");
        ThrusterALL_V = (AudioClip)Resources.Load("Audio/FX/Thrusters all 3 GW Verbed out");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!notrespawning1)
        {
            p1.transform.position = p1pos.position;
            p1Thruster.transform.position = p1pos.position;
            p1extra.transform.position = p1pos.position;
        }
        if(!notrespawning2)
        {
            p2.transform.position = p2pos.position;
            p2Thruster.transform.position = p2pos.position;
            p2extra.transform.position = p2pos.position;
        }
    }
}
