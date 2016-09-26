using UnityEngine;
using System.Collections;

public class DeveloperCommands : MonoBehaviour {

    public Transform player;
    private Spell spell;
    private Stats stats;
    
	// Use this for initialization
	void Start () {
        stats =  player.GetComponent<Stats>();
        spell = player.GetComponent<Spell>();
    }

    // Update is called once per frame
    //currency, maxhealth+/-, maxMana +/-, Blades, spells
    void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha0))//respawn
        {
            game.gm.respawn();
            print("res");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))//health +
        {
            stats.increaseMaxHealth(10);
            print("health +");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))//health -
        {
            stats.increaseMaxHealth(-10);
            print("health -");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))//mana +
        {
            stats.increaseMaxMana(10);
            print("mana +");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))//mana -
        {
            stats.increaseMaxMana(-10);
            print("mana -");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))//manablade
        {
            stats.AddManaBlade();
            print("manablade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))//fireblade
        {
            stats.AddFireBlade();
            print("fireblade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))//shadowblade
        {
            stats.AddShadowBlade();
            print("shadowblade");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))//currency +
        {
            stats.increaseScore(100);
            print("currency increase");
        }

        if (Input.GetKeyDown(KeyCode.I))//Mend learn/unlearn
        {
            spell.LearnedHeal = !spell.LearnedHeal;
            print("Mend" + spell.LearnedHeal);
        }
        if (Input.GetKeyDown(KeyCode.O))//fireRay learn/unlearn
        {
            spell.LearnedFire = !spell.LearnedFire;
            print("fireray" + spell.LearnedFire);
        }
        if (Input.GetKeyDown(KeyCode.P))//iceRay learn/unlearn
        {
            spell.LearnedFrost = !spell.LearnedFrost;
            print("frostray" + spell.LearnedFrost);
        }

    }
}
