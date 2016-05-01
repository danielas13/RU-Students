using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    public float damage = 1;    //damage of a fireball spell.
    public LayerMask NotHit;
    private float spellDistance = 5;

    Transform spellPoint;

    void Awake(){
        spellPoint = transform.FindChild("SpellCast");
        if(spellPoint == null)
        {
            Debug.LogError("No FirePoint?");
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        CastFireBall();
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            CastFireBall();
        }
	}

    void CastFireBall()
    {
        Vector2 SpellPosition = new Vector2(spellPoint.position.x, spellPoint.position.y);

        GameObject character = GameObject.FindGameObjectWithTag("Player");
        Vector2  playerPos = character.transform.position;
        Debug.Log(playerPos);
        Debug.Log(SpellPosition);
        RaycastHit2D hit = Physics2D.Raycast(SpellPosition, playerPos - SpellPosition, spellDistance, NotHit);

        if(SpellPosition.x > playerPos.x)
        {
            //Player facing to the right.
            Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x + spellDistance, playerPos.y), Color.black);
        }
        else
        {
            //player facing to the left.
            Debug.DrawLine(SpellPosition, new Vector2(SpellPosition.x - spellDistance, playerPos.y), Color.black);
        }
    }
}
