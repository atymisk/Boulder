using UnityEngine;
using System.Collections;

public class SFXManager : MonoBehaviour
{
    public Transform p1pos;
    public Transform p2pos;

    public AudioSource mng;

    static AudioSource p1hit;
    static AudioSource p1swing;
    static AudioSource p1swingalt;
    static AudioSource p1extra;
    static AudioSource p1explosion;
    static AudioSource p1footsteps;

    static AudioSource p2hit;
    static AudioSource p2swing;
    static AudioSource p2swingalt;
    static AudioSource p2extra;
    static AudioSource p2explosion;
    static AudioSource p2footsteps;

    private static bool p1alt = false;
    private static bool p2alt = false;

    public bool p1thrust_on = false;
    public bool p2thrust_on = false;
    static AudioSource p1Thruster;
    static AudioSource p2Thruster;

    public bool notrespawning1 = false;
    public bool notrespawning2 = false;

    private AudioClip countdown;

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

    private static AudioClip ledgegrab;
    private static AudioClip land1;
    private static AudioClip land2;

    private static AudioClip step1;
    private static AudioClip step2;
    private static AudioClip step3;
    private static AudioClip step4;
    private static AudioClip step5;
    private static AudioClip step6;
    private static AudioClip step7;
    private static AudioClip step8;

    private static AudioClip swing1;
    private static AudioClip swing2;
    private static AudioClip swing3;
    private static AudioClip swing4;
    private static AudioClip swing5;
    private static AudioClip swing6;
    private static AudioClip swing7;
    private static AudioClip swing8;

    private static AudioClip explosion1;
    private static AudioClip explosion2;
    private static AudioClip explosion3;
    private static AudioClip explosion4;
    private static AudioClip explosion5;

    private static AudioClip RocketLauncher;
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
        ac.volume = 1;
        ac.clip = limbflying7;
        ac.Play();
    }

    private static void LimbPickup(AudioSource ac)
    {
        //if(ac.clip != null)
        //    ac.Stop();
        ac.volume = 1;
        ac.clip = pickup2;
        ac.Play();
    }

    private static void launch(AudioSource ac)
    {
        ac.clip = RocketLauncher;
        ac.Play();
    }

    private static void Whiff(AudioSource ac)
    {
        //cut the clip shorter?
       // if(ac.isPlaying)
        //    return;
        int r = Random.Range(0, 8);
        switch(r)
        {
            case 0:
                ac.clip = swing1;
                break;
            case 1:
                ac.clip = swing2;
                break;
            case 2:
                ac.clip = swing3;
                break;
            case 3:
                ac.clip = swing4;
                break;
            case 4:
                ac.clip = swing5;
                break;
            case 5:
                ac.clip = swing6;
                break;
            case 6:
                ac.clip = swing7;
                break;
            case 7:
                ac.clip = swing8;
                break;
            default:
                break;
        }
        ac.Play();
    }

    private static void Explosion(AudioSource ac)
    {
        int r = Random.Range(0, 5);
        switch (r)
        {
            case 0:
                ac.clip = explosion1;
                break;
            case 1:
                ac.clip = explosion2;
                break;
            case 2:
                ac.clip = explosion3;
                break;
            case 3:
                ac.clip = explosion4;
                break;
            case 4:
                ac.clip = explosion5;
                break;
            default:
                break;
        }
        ac.Play();
    }

    private static void footsteps(AudioSource ac)
    {
        if (ac.isPlaying && (ac.clip != land1 || ac.clip != land2))
            return;
        int r = Random.Range(0, 8);
        switch (r)
        {
            case 0:
                ac.clip = step1;
                break;
            case 1:
                ac.clip = step2;
                break;
            case 2:
                ac.clip = step3;
                break;
            case 3:
                ac.clip = step4;
                break;
            case 4:
                ac.clip = step5;
                break;
            case 5:
                ac.clip = step6;
                break;
            case 6:
                ac.clip = step7;
                break;
            case 7:
                ac.clip = step8;
                break;
            default:
                break;
        }
        ac.Play();
    }

    private static void grabledge(AudioSource ac)
    {
        ac.volume = 1;
        ac.clip = ledgegrab;
        ac.Play();
    }

    private static void landing(AudioSource ac)
    {
        int r = Random.Range(0, 2);
        if (r == 0)
            ac.clip = land1;
        else
            ac.clip = land2;
        ac.Play();
    }

    private static void NoLimb(AudioSource ac)
    {
        ac.volume = 0.5f;
        ac.clip = MissingLimb;
        ac.Play();
    }

    public static void cueFX(string player, string fxtype)
    {
        AudioSource s;
        AudioSource extra;
        AudioSource swing;
        AudioSource step;
        AudioSource explode;
        if(player == "P1")
        {
            s = p1hit;
            extra = p1extra;
            swing = (p1alt ? p1swingalt : p1swing);
            step = p1footsteps;
            explode = p1explosion;
        }
        else
        {
            s = p2hit;
            extra = p2extra;
            swing = (p2alt ? p2swingalt : p2swing);
            step = p2footsteps;
            explode = p2explosion;
        }

        switch(fxtype)
        {
            case "Step":
                footsteps(step);
                break;
            case "Hit":
                Hit(s);
                break;
            case "Land":
                landing(step);
                break;
            case "Explode":
                Explosion(explode);
                break;
            case "Launch":
                launch(explode);
                break;
            case "LimbLost":
                LostLimb(extra);
                break;
            case "LimbGained":
                LimbPickup(extra);
                break;
            case "Whiff":
                Whiff(swing);
                if (player == "P1")
                    p1alt = !p1alt;
                else
                    p2alt = !p2alt;
                break;
            case "Ledge":
                grabledge(extra);
                break;
            case "NoLimb":
                NoLimb(extra);
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
        if (p1Thruster == null || p2Thruster == null)
            return;
        AudioSource s = (player == "P1" ? p1Thruster : p2Thruster);
        if(s.isPlaying)
            s.Stop();
    }

    public IEnumerator CueCountdown()
    {
        if (!mng.isPlaying)
            mng.Play();
        yield return new WaitForSeconds(1);
        //mng.Stop();
    }

    // Use this for initialization
    void Awake ()
    {
        p1hit = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1hit.transform.position = p1pos.position;
        p1Thruster = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1Thruster.transform.position = p1pos.position;
        p1extra = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1extra.transform.position = p1pos.position;
        p1explosion = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1explosion.transform.position = p1pos.position;
        p1swing = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1swing.transform.position = p1pos.position;
        p1footsteps = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1footsteps.transform.position = p1pos.position;
        p1swingalt = (AudioSource)gameObject.AddComponent<AudioSource>();
        p1swingalt.transform.position = p1pos.position;

        p2hit = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2hit.transform.position = p2pos.position;
        p2Thruster = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2Thruster.transform.position = p2pos.position;
        p2extra = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2extra.transform.position = p2pos.position;
        p2explosion = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2explosion.transform.position = p2pos.position;
        p2swing = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2swing.transform.position = p2pos.position;
        p2footsteps = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2footsteps.transform.position = p2pos.position;
        p2swingalt = (AudioSource)gameObject.AddComponent<AudioSource>();
        p2swingalt.transform.position = p2pos.position;

        mng = (AudioSource)gameObject.AddComponent<AudioSource>();
        mng.volume = 1;
        mng.priority = 0;

        p1footsteps.volume = 0.3f;
        p1footsteps.priority = 170;
        p2footsteps.volume = 0.3f;
        p2footsteps.priority = 170;

        p1swing.priority = 0; p1swingalt.priority = 0;
        p1swing.volume = 1; p1swingalt.volume = 1;
        p2swing.priority = 0; p2swingalt.priority = 0;
        p2swing.volume = 1; p2swingalt.volume = 1;

        p1Thruster.loop = true;
        p2Thruster.loop = true;

        countdown = (AudioClip)Resources.Load("Audio/FX/menu/Select Button No verb");
        MissingLimb = (AudioClip)Resources.Load("Audio/FX/Denial No Limb Game Worthy");
        pickup1 = (AudioClip)Resources.Load("Audio/FX/grablimbs/Got Your Limb 1 Short GW");
        pickup2 = (AudioClip)Resources.Load("Audio/FX/grablimbs/Got Your Limb 2 GW");
        pickup2_long = (AudioClip)Resources.Load("Audio/FX/grablimbs/Got Your Limb 2 Long GW");

        limbflying1 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 1 GW");
        limbflying2 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 2 GW");
        limbflying3 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 3 GW");
        limbflying4 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 4 GW");
        limbflying5 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 5 GW");
        limbflying6 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 6 GW");
        limbflying7 = (AudioClip)Resources.Load("Audio/FX/limbsflying/Limb Flying Through Air 7 GW");

        MetalHit1 = (AudioClip)Resources.Load("Audio/FX/hits/Metal Hits and Explosion 1 Game Worthy");
        MetalHit2 = (AudioClip)Resources.Load("Audio/FX/hits/Metal Hits and Explosion 2 Less BOOM Game Worthy");
        MetalHit3 = (AudioClip)Resources.Load("Audio/FX/hits/Metal Hit and Explosion 3 Verb Out GW");
        MetalHitGlass = (AudioClip)Resources.Load("Audio/FX/hits/Metal Hits and Explosion 2 Less BOOM Game Worthy");
        MetalHitGlassBig = (AudioClip)Resources.Load("Audio/FX/hits/Metal Hit and Explosion Huge with Glass GW");

        MetalScrape1 = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape 1 Game Worthy");
        MetalScrape2 = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape 2 Game Worthy");
        MetalScrape3 = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape 3 Game Worthy");
        MetalScrapeH1 = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape HIGH 1 Game Worthy");
        MetalScrapeH2 = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape HIGH 2 Game Worthy");
        MetalScrapePass = (AudioClip)Resources.Load("Audio/FX/scrapes/Metal Scrape PASSING Game Worthy");

        StartEndFanFare1 = (AudioClip)Resources.Load("Audio/FX/START OR END OF GAME MAYBE_");
        StartEndFanFare2 = (AudioClip)Resources.Load("Audio/FX/START OR END OF GAME MAYBE_2");

        Thruster1 = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters 1 Verb ");
        Thruster2 = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters 2 Verb ");
        Thruster3 = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters 3 Verb ");
        Thruster4 = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters 4");
        ThrusterALL = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters all 3 GW");
        ThrusterALL_V = (AudioClip)Resources.Load("Audio/FX/thrusters/Thrusters all 3 GW Verbed out");

        ledgegrab = (AudioClip)Resources.Load("Audio/FX/ledge/Metal Glove Grabs Ledge");
        land1 = (AudioClip)Resources.Load("Audio/FX/landing/Landing on the ground 1");
        land2 = (AudioClip)Resources.Load("Audio/FX/landing/Landing on the ground 2");

        step1 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 1");
        step2 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 2");
        step3 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 3");
        step4 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 4");
        step5 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 5");
        step6 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 6");
        step7 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 7");
        step8 = (AudioClip)Resources.Load("Audio/FX/steps/Gravel step 8");

        swing1 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 1");
        swing2 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 2");
        swing3 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 3");
        swing4 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 4");
        swing5 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 5");
        swing6 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 6");
        swing7 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 7");
        swing8 = (AudioClip)Resources.Load("Audio/FX/swings/Leg and Body Swings 8");

        explosion1 = (AudioClip)Resources.Load("Audio/FX/explosions/Huge Explosion When they get knocked off");
        explosion2 = (AudioClip)Resources.Load("Audio/FX/explosions/Medium Explosion ");
        explosion3 = (AudioClip)Resources.Load("Audio/FX/explosions/Robot Explosion");
        explosion4 = (AudioClip)Resources.Load("Audio/FX/explosions/Robot Explosion and Crash");
        explosion5 = (AudioClip)Resources.Load("Audio/FX/explosions/Robot Explosion and Crash 2");

        RocketLauncher = (AudioClip)Resources.Load("Audio/FX/explosions/Medium Explosion 2");

        mng.clip = countdown;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!notrespawning1)
        {
            p1hit.transform.position = p1pos.position;
            p1Thruster.transform.position = p1pos.position;
            p1extra.transform.position = p1pos.position;
            p1footsteps.transform.position = p1pos.position;
            p1swing.transform.position = p1pos.position;
            p1swingalt.transform.position = p1pos.position;
        }
        if(!notrespawning2)
        {
            p2hit.transform.position = p2pos.position;
            p2Thruster.transform.position = p2pos.position;
            p2extra.transform.position = p2pos.position;
            p2footsteps.transform.position = p2pos.position;
            p2swing.transform.position = p2pos.position;
            p2swingalt.transform.position = p2pos.position;
        }
    }
}
