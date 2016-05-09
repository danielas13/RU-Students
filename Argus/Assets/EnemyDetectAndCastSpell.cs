using UnityEngine;
using System.Collections;
public class EnemyDetectAndCastSpell : MonoBehaviour {
	public LayerMask NotHit;
	public Transform FirePrefab;
	public float spellDistance = 15;
	GameObject character;
	private float cooldown = 3;
	private bool IsCasting = false;

	private float TotalCastingTime = 1.6f;
	private float castingTime = 1.6f;

    private Transform cleric;
    private Animator clericAnim;

	private Transform Wizard;
	private Animator WizardAnimator;
    // Use this for initialization
    void Awake () {
		character = GameObject.FindGameObjectWithTag("Player");
        cleric = transform.parent.FindChild("Cleric");
        clericAnim = cleric.GetComponent<Animator>();

		//get Wizard animator component 
		Wizard = transform.parent.FindChild ("Eva_Full_Animated");
		WizardAnimator = Wizard.GetComponent <Animator> ();
    }
	// Update is called once per frame
	void Update()
	{

		if(IsCasting == true){
			castingTime = castingTime - Time.deltaTime;
			if(castingTime < 0){
				IsCasting = false;
				this.transform.parent.GetComponent <EnemyCasterBehavior> ().castingSpell = false;
				WizardAnimator.SetBool ("IsCasting",IsCasting);
				castingTime = TotalCastingTime;

			}
		}
		if (character == null)
		{
			character = GameObject.FindGameObjectWithTag("Player");
		}
			


		RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(character.transform.position.x - transform.position.x, 0), spellDistance, NotHit);
		//Debug.DrawLine(transform.position, new Vector2(transform.position.x - spellDistance, character.transform.position.y), Color.red);
		if (hit.collider != null && cooldown < 0)
		{
			//Debug.Log(hit.transform.tag + " detected");
			if (hit.transform.tag == "Player") {
               // clericAnim.SetBool("cast", true);
				IsCasting = true;
				this.transform.parent.GetComponent <EnemyCasterBehavior> ().castingSpell = true;
				WizardAnimator.SetBool ("IsCasting",IsCasting);
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

			Instantiate(FirePrefab, new Vector2 (transform.parent.transform.position.x,transform.parent.transform.position.y+0.5f), transform.rotation);
			//player.spendMana(2);
		}
		else
		{
			Instantiate(FirePrefab, new Vector2 (transform.parent.transform.position.x,transform.parent.transform.position.y+0.5f), transform.rotation * Quaternion.Euler(Vector3.up * 180));
		}
	}
}