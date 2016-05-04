using UnityEngine;
using System.Collections;
public class MoveEnemySpell : MonoBehaviour {
	public int speed = 20;
	public int duration = 2;
	public int damage = 5;    //damage of an attack.
	public int range = 2;
	private GameObject player;
	// Update is called once per frame
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () {
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
		if(Vector2.Distance(transform.position, player.transform.position) < range)
		{
			Stats playerStat = player.GetComponent<Stats>();
			playerStat.damagePlayer(damage);
			Destroy(this.gameObject);
		}
		transform.Translate(Vector3.right * Time.deltaTime * speed);
		Destroy(this.gameObject, duration);
	}
}