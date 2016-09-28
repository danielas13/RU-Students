using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonIndicatorController : MonoBehaviour {

    [SerializeField]
    private Text InterractRB, ToggleSpellsLB, JumpA, AttackX, CastB, PowerAttackY, BlockLT;

    Color baseColor, fadedColor;

    public bool DoorDefault = true;
    public bool blocking = false, channeling = false;

	// Use this for initialization
	void Start () {
        baseColor = InterractRB.color;
        fadedColor = baseColor;
        fadedColor.a = fadedColor.a / 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeInterract(string newValue)
    {
        InterractRB.text = newValue;
        DoorDefault = false;
    }

    public void ResetInterract()
    {
        InterractRB.text = "Interract [Press]";
        DoorDefault = true;
    }

    public void Blocking(bool Blocking)
    {
        if (Blocking)
        {
            AttackX.color = fadedColor;
            JumpA.color = fadedColor;
            CastB.color = fadedColor;
            PowerAttackY.color = fadedColor;
            blocking = true;
        }
        else
        {
            AttackX.color = baseColor;
            JumpA.color = baseColor;
            CastB.color = baseColor;
            PowerAttackY.color = baseColor;
            blocking = false;
        }
    }

    public void Channeling(bool casting)
    {
        if (casting)
        {
            AttackX.color = fadedColor;
            JumpA.color = fadedColor;
            CastB.color = fadedColor;
            BlockLT.color = fadedColor;
            PowerAttackY.color = fadedColor;
            channeling = true;
        }
        else
        {
            AttackX.color = baseColor;
            JumpA.color = baseColor;
            BlockLT.color = baseColor;
            CastB.color = baseColor;
            PowerAttackY.color = baseColor;
            channeling = false;
        }
    }

    public void changeSpell(int spellID)
    {
        if (spellID == 3 || spellID == 4)
        {
            CastB.text = "Cast Spell [Hold]";
        }
        else
        {
            CastB.text = "Cast Spell [Press]";
        }
    }


}
