using UnityEngine;
using System.Collections;
public class EnemyDetectAndCastSpell : MonoBehaviour {
	public LayerMask NotHit;
	public Transform FirePrefab;
	public float spellDistance = 1000;
	GameObject character;
	private float cooldown = 3;
	// Use this for initialization
	void Start () {
		character = GameObject.FindGameObjectWithTag("Player");
	}
	// Update is called once per frame
	void Update()
	{
		if (character == null)
		{
			character = GameObject.FindGameObjectWithTag("Player");
		}
		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(character.transform.position.x - transform.position.x, 0), Mathf.Infinity, NotHit);
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x - spellDistance, character.transform.position.y), Color.red);
		if (hit.collider != null && cooldown < 0)
		{
			//Debug.Log(hit.transform.tag + " detected");
			if (hit.transform.tag == "Player") {
				//Debug.Log("SJDAKLDJALSKDJALSKDJALKSD");
				//Debug.DrawLine(transform.position, new Vector2(transform.position.x - spellDistance, character.transform.position.y), Color.red);
				CastFireBall();
				cooldown = 3;
			}
		}
		cooldown -= Time.deltaTime;
	}
	void CastFireBall()
	{
		Vector2 SpellPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 playerPos = character.transform.position;
		//RaycastHit2D hit = Physics2D.Raycast(SpellPosition, new Vector2(SpellPosition.x - playerPos.x, SpellPosition.y - playerPos.y), spellDistance, NotHit);
		if (SpellPosition.x < playerPos.x)
		{
			//Stats player = character.GetComponent<Stats>();

			Instantiate(FirePrefab, transform.position, transform.rotation);
			//player.spendMana(2);
		}
		else
		{
			Instantiate(FirePrefab, transform.position, transform.rotation * Quaternion.Euler(Vector3.up * 180));
		}
	}
}