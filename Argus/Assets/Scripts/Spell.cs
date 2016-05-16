using UnityEngine;
using System.Collections;

    public class Spell : MonoBehaviour
    {

        //  public float damage = 1;    //damage of a fireball spell.
        public LayerMask NotHit;
        //private float spellDistance = 5;
        Transform spellPoint;
        //public Transform HealParticle;
        private static readonly System.Random randomAttackGenerator = new System.Random();

        private Transform skeleton2;
        private Animator skelAnim2;

        private Stats player;
        private GameObject character;
		private Transform skeletonFootman;
		private Animator skeletonAnimator; 
        public Transform FirePrefab;
        public Transform FrostRay;
        public Transform FireRay;
        private float channelDelay = 0.5f;


        public bool LearnedHeal = false;
        public bool LearnedFrost = false;
        public bool LearnedFire = false;


        int NumberOfSpells = 4;// Nuber of spells that the player has learned
        private int currentSpell = 1;

        void Awake()
        {
			spellPoint = transform.FindChild("ToFlip").FindChild("SpellCast");
            if (spellPoint == null)
            {
                Debug.LogError("No FirePoint?");
            }
            skeleton2 = transform.FindChild("Skeleton_warlord");
            skelAnim2 = skeleton2.GetComponent<Animator>();

            character = GameObject.FindGameObjectWithTag("Player");
            player = character.GetComponent<Stats>();
            //FrostRay = spellPoint.FindChild("FrostRay");

			skeletonFootman = transform.FindChild ("ToFlip").FindChild ("Skeleton_footman");
			skeletonAnimator = skeletonFootman.GetComponent <Animator> ();

        }

        // Use this for initialization
        void Start()
        {

        }
        void incrementSpell()
        {
            if(currentSpell == 1) // FIRE
            {
                if (LearnedHeal)
                {
                    currentSpell = 2;
                    return;
                }
                else if (LearnedFrost)
                {
                    currentSpell = 3;
                    return;
                }
                else if (LearnedFire)
                {
                    currentSpell = 4;
                }
                else
                {
                    return;
                }
            }
            else if (currentSpell == 2)//HEAL
            {
                if (LearnedFrost)
                {
                    currentSpell = 3;
                    return;
                }
                else if (LearnedFire)
                {
                    currentSpell = 4;
                    return;
                }
                else
                {
                    currentSpell = 1;
                    return;
                }
            }
            else if(currentSpell == 3)//FROST
            {
                if (LearnedFire)
                {
                    currentSpell = 4;
                    return;
                }
                else
                {
                    currentSpell = 1;
                    return;
                }
            }
            else                       //FIRERAY
            {
                currentSpell = 1;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("SpellCycle"))
            {
                incrementSpell();
            }
            if (Input.GetButtonDown("UseSpell"))
            {
                if (currentSpell == 1)
                {
                    skeletonAnimator.SetTrigger("CastProjectileSpell");
                    CastFireBall();
                    //skelAnim2.SetTrigger("CastSpell");
                }
                if (currentSpell == 2 && GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_Grounded) //Cast a heal spell when player is on the ground and presses r.
                {
                    skeletonAnimator.SetTrigger("CastHealingSpell");
                    CastHeal();
                    //skelAnim2.SetTrigger("CastSpell");
                }
                if (currentSpell == 3 && GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_Grounded)
                {
                    channelDelay = 0.5f;
                    skeletonAnimator.SetBool("isChanneling", true);
                    ChannelFrostRay();
                    //skelAnim2.SetTrigger("CastSpell");
                }
                if (currentSpell == 4 && GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_Grounded)
                {
                    channelDelay = 0.5f;
                    skeletonAnimator.SetBool("isChanneling", true);
                    ChannelFireRay();
                    //skelAnim2.SetTrigger("CastSpell");
                }
            }
                
			//Debug.Log (transform.GetComponent<PlatformerCharacter2D> ().isChanneling );
			channelDelay -= Time.deltaTime;
			if(transform.GetComponent<PlatformerCharacter2D> ().isChanneling == false && channelDelay < 0){ //this currently always triggers --> channeling wont work
				skeletonAnimator.SetBool ("isChanneling", false);
			}
        }

		void LateUpdate(){
			
		}
        void ChannelFireRay()
        {
            //FrostRay.gameObject.SetActive(true);
            Instantiate(FireRay, spellPoint.position, spellPoint.rotation * Quaternion.Euler(new Vector3(0, 0, 1) * 1)); // instantate a straight ray

        }
        void ChannelFrostRay()
        {
            //FrostRay.gameObject.SetActive(true);
            Instantiate(FrostRay,spellPoint.position, spellPoint.rotation * Quaternion.Euler(new Vector3(0,0,1) * 1)); // instantate a straight ray
            
        }
        void CastHeal()
        {
            GameObject character = GameObject.FindGameObjectWithTag("Player");
            Stats player = character.GetComponent<Stats>();
            if (player.status.currentMana >= 4)
            {

                int randomDmg = randomAttackGenerator.Next(player.status.minSpellPower, player.status.maxSpellPower);
                player.restoreHealth(randomDmg);
                //Instantiate(HealParticle, transform.position, transform.rotation);//The aoe Spell script is called HealAreaOfEffectScript and is located under the Spells folder
                transform.FindChild("SpellParticles").FindChild("HealParticles").gameObject.SetActive(true);
                player.spendMana(30);
            }
            else
            {
                //Debug.Log("Not Enough Mana!"); //Give player feedback istead of this!
            }
        }

        void CastFireBall()
        {
            Vector2 SpellPosition = new Vector2(spellPoint.position.x, spellPoint.position.y);

            GameObject character = GameObject.FindGameObjectWithTag("Player");
            Vector2 playerPos = character.transform.position;
            //RaycastHit2D hit = Physics2D.Raycast(SpellPosition, new Vector2(SpellPosition.x - playerPos.x, SpellPosition.y - playerPos.y), spellDistance, NotHit);
            if (SpellPosition.x > playerPos.x)
            {
                //Player facing to the right.

                Stats player = character.GetComponent<Stats>();
                if (player.status.currentMana >= 2)
                {
                    Instantiate(FirePrefab, spellPoint.position, spellPoint.rotation);
                    player.spendMana(20);
                }
                else
                {
                   // Debug.Log("Not Enough Mana!");
                }
            }
            else
            {
                Stats player = character.GetComponent<Stats>();
                if (player.status.currentMana >= 2)
                {
                    Instantiate(FirePrefab, spellPoint.position, spellPoint.rotation);
                    player.spendMana(20);
                }
                else
                {
                    //Debug.Log("Not Enough Mana!");
                }
            }
        }
    }