using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ShowSpell : MonoBehaviour {

    private Spell playerSpell;
    public GameObject FelBall,Mend,FrostRay,FireRay;
	public Text spellinfo;
	// Use this for initialization
	void Start () {
        playerSpell = GameObject.Find("Player").GetComponent<Spell>();
        FelBall.SetActive(true);
        Mend.SetActive(false);
        FrostRay.SetActive(false);
        FireRay.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (playerSpell.currentSpell == 1)
        {
            if (!FelBall.activeSelf)
            {
                FelBall.SetActive(true);
                Mend.SetActive(false);
                FrostRay.SetActive(false);
                FireRay.SetActive(false);
				spellinfo.text = "Felball";
            }
        }
        else if(playerSpell.currentSpell ==2)
        {
            if (!Mend.activeSelf)
            {
                FelBall.SetActive(false);
                Mend.SetActive(true);
                FrostRay.SetActive(false);
                FireRay.SetActive(false);
				spellinfo.text = "Mend";

            }
        }
        else if (playerSpell.currentSpell == 3)
        {
            if (!FrostRay.activeSelf)
            {
                FelBall.SetActive(false);
                Mend.SetActive(false);
                FrostRay.SetActive(true);
                FireRay.SetActive(false);
				spellinfo.text = "Frost Ray";
            }
        }
        else if (playerSpell.currentSpell == 4)
        {
            if (!FireRay.activeSelf)
            {
                FelBall.SetActive(false);
                Mend.SetActive(false);
                FrostRay.SetActive(false);
                FireRay.SetActive(true);
				spellinfo.text = "Fire Ray";
            }
        }
    }
}
