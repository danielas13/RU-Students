using UnityEngine;
using System.Collections;

public class DeveloperCommands : MonoBehaviour {

    public Transform player;
    private Spell spell;
    private Stats stats;
    public OneToOneDoor Smirnoff;
    public GameObject Boss1, Boss2;
    
	// Use this for initialization
	void Start () {
        stats =  player.GetComponent<Stats>();
        spell = player.GetComponent<Spell>();
    }

    // Update is called once per frame
    //currency, maxhealth+/-, maxMana +/-, Blades, spells
    void Update () {

        if (Input.GetKeyDown(KeyCode.K))
        {
            Smirnoff.unlock();
            Smirnoff.unlock();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))//respawn
        {
            game.KillPlayer();
          //  print("res");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))//health +
        {
            stats.increaseMaxHealth(10);
         //   print("health +");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//health -
        {
            stats.increaseMaxHealth(-10);
           // print("health -");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//mana +
        {
            stats.increaseMaxMana(10);
           // print("mana +");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//mana -
        {
            stats.increaseMaxMana(-10);
           // print("mana -");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))//manablade
        {
            stats.AddManaBlade();
            stats.floatingText("ManaBlade added");
            // print("manablade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))//fireblade
        {
            stats.AddFireBlade();
            stats.floatingText("FireBlade added");
            // print("fireblade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))//shadowblade
        {
            stats.AddShadowBlade();
            stats.floatingText("ShadowBlade added");
            // print("shadowblade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))//currency +
        {
            stats.increaseScore(100);
           // print("currency increase");
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            stats.removeSpecialBlades();
            stats.floatingText("Normal sword restored");

        }

        if (Input.GetKeyDown(KeyCode.I))//Mend learn/unlearn
        {
            spell.LearnedHeal = !spell.LearnedHeal;
            if (spell.LearnedHeal)
            {
                stats.floatingText("Learned Mend");
            }
            else
            {
                stats.floatingText("Unlearned Mend");
            }

            //  print("Mend" + spell.LearnedHeal);
        }
        if (Input.GetKeyDown(KeyCode.O))//fireRay learn/unlearn
        {
            spell.LearnedFire = !spell.LearnedFire;
            if (spell.LearnedFire)
            {
                stats.floatingText("Learned FireRay");
            }
            else
            {
                stats.floatingText("Unlearned FireRay");
            }
            //  print("fireray" + spell.LearnedFire);
        }
        if (Input.GetKeyDown(KeyCode.P))//iceRay learn/unlearn
        {
            spell.LearnedFrost = !spell.LearnedFrost;
            if (spell.LearnedFrost)
            {
                stats.floatingText("Learned FrostRay");
            }
            else
            {
                stats.floatingText("Unlearned FrostRay");
            }
            
           // print("frostray" + spell.LearnedFrost);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            stats.status.armor = 1;
            stats.floatingText("Divine Shield added");

        }

    }
}
