using UnityEngine;
using System.Collections;
public class EnemyDetectAndCastSpell : MonoBehaviour {
	public LayerMask NotHit;
	public Transform FirePrefab;
	public float spellDistance = 15;
	GameObject character;
	private float cooldown = 3;
	private bool IsCasting = false;

	private float TotalCastingTime = 1f;
	private float castingTime = 1f;

    private Transform cleric;
    private Animator clericAnim;

	private Transform LeftFireTrail;
	private Transform RightFireTrail;

	private Transform Wizard;
	private Animator WizardAnimator;
    private EnemyCasterBehavior enemyBehavior;

	private Quaternion preCastingRotation;
	private Vector3 preCastingPosition;
	private bool trailsActive = false;
    // Use this for initialization
    void Awake () {
		character = GameObject.FindGameObjectWithTag("Player");
        cleric = transform.parent.FindChild("Cleric");
        clericAnim = cleric.GetComponent<Animator>();
        enemyBehavior = transform.parent.GetComponent<EnemyCasterBehavior>();

        //get Wizard animator component 
        Wizard = transform.parent.FindChild ("Eva_Full_Animated");
		WizardAnimator = Wizard.GetComponent <Animator> ();

		Transform[] children = transform.parent.GetComponentsInChildren<Transform>();
		foreach (Transform child in children) {
			if (child.name == "LeftHandFireTrail") {
				LeftFireTrail = child;
			}
			if (child.name == "RightHandFireTrail") {
				RightFireTrail = child;
			}
		}
		LeftFireTrail.gameObject.SetActive (false);
		RightFireTrail.gameObject.SetActive (false);


    }
	// Update is called once per frame


//	Quaternion preCastingQuaternion = transform.rotation;
//	Vector3 preCastingPosition = transform.position;

	void Update()
	{

        /* checking if spell has been cast, if so - cancel animation and disable root motion */
        if (!transform.parent.GetComponent<EnemyCasterBehavior>().frozen)
        {
            if (IsCasting == true)
            {

                WizardAnimator.SetBool("CastSpell",true);
               //LeftFireTrail.gameObject.SetActive(true);
                //RightFireTrail.gameObject.SetActive(true);
                trailsActive = true;
//<<<<<<< HEAD
          
                castingTime = castingTime - Time.deltaTime;
/*=======
               // Debug.Log("hadsuhasduha");
                if(castingTime >= 0)
                {
                    castingTime = castingTime - Time.deltaTime;
                }

>>>>>>> 4311f0def91cfb12d051371cef1036b763e9cce7*/
                if (castingTime < 0)
                { //finished channeling cast
                    IsCasting = false;


                    enemyBehavior.castingSpell = false;
                    WizardAnimator.SetBool("IsCasting", IsCasting);
                  
                    castingTime = TotalCastingTime;
                    CastFireBall();
                    /* This code handles root motion displacement during casting */
                    //WizardAnimator.applyRootMotion = false;
                    transform.parent.FindChild("Eva_Full_Animated").position = preCastingPosition;
                    transform.parent.FindChild("Eva_Full_Animated").rotation = preCastingRotation;

                }
            }
            else
            {
                if (trailsActive == true)
                {
                    trailsActive = false;
                    LeftFireTrail.gameObject.SetActive(false);
                    RightFireTrail.gameObject.SetActive(false);

                }
            }

            /* HORIZONTAL Raycast that attempts to detect the player */
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(character.transform.position.x - transform.position.x, 0, character.transform.position.z - transform.position.z), spellDistance, NotHit);
            //Debug.DrawLine(transform.position, new Vector3(transform.position.x - spellDistance, character.transform.position.y), Color.red);


            /* If a player is detected, attempt to cast spell and play the spellcast animation*/
            if (hit.collider != null && cooldown < 0)
            {
               // Debug.Log(hit.transform.tag);
                if (hit.transform.tag == "Player")
                {
//<<<<<<< HEAD
                    IsCasting = true;
                    this.transform.parent.GetComponent<EnemyCasterBehavior>().castingSpell = true;

                    /* This code handles root motion displacement during casting */
                    preCastingPosition = transform.parent.FindChild("Eva_Full_Animated").position;
                    preCastingRotation = transform.parent.FindChild("Eva_Full_Animated").rotation;
                    WizardAnimator.SetBool("IsCasting", IsCasting);
                   // WizardAnimator.applyRootMotion = true;
                    cooldown = 3;

/*=======
                    if (!IsCasting)
                    {
                        IsCasting = true;
                        enemyBehavior.castingSpell = true;

                        /* This code handles root motion displacement during casting 
                        preCastingPosition = transform.parent.FindChild("Eva_Full_Animated").position;
                        preCastingRotation = transform.parent.FindChild("Eva_Full_Animated").rotation;
                        //   WizardAnimator.SetBool("IsCasting", IsCasting);
                        // WizardAnimator.applyRootMotion = true;
                        cooldown = 3;
                    }
//>>>>>>> 4311f0def91cfb12d051371cef1036b763e9cce7*/
                }
            }
            cooldown -= Time.deltaTime;
        }
		
	}


	/* Instantiates a fireball with relatie position to the caster, fireball logic is in MoveEnemyFireball */
	void CastFireBall()
	{
		Vector2 SpellPosition = new Vector2(transform.position.x, transform.position.y);
		Vector2 playerPos = character.transform.position;
		//RaycastHit2D hit = Physics2D.Raycast(SpellPosition, new Vector2(SpellPosition.x - playerPos.x, SpellPosition.y - playerPos.y), spellDistance, NotHit);


		if (SpellPosition.x < playerPos.x)
		{
			Instantiate(FirePrefab, new Vector3 (transform.parent.position.x,transform.parent.position.y+0.5f, transform.parent.position.z), transform.rotation * Quaternion.Euler(Vector3.up * 180));
		}
		else
		{
			Instantiate(FirePrefab, new Vector3 (transform.parent.position.x,transform.parent.position.y+0.5f, transform.parent.position.z), transform.rotation );
		}
	}
}