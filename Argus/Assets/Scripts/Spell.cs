using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
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


        public Transform FirePrefab;
        public Transform FrostRay;

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

        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CastFireBall();
                skelAnim2.SetTrigger("CastSpell");
            }
            if (Input.GetKeyDown(KeyCode.R) && GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_Grounded) //Cast a heal spell when player is on the ground and presses r.
            {
                CastHeal();
                skelAnim2.SetTrigger("CastSpell");
            }
            if (Input.GetKeyDown(KeyCode.T) && GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().m_Grounded)
            {
                ChannelFrostRay();
                //skelAnim2.SetTrigger("CastSpell");
                
            }
        }
        void ChannelFrostRay()
        {
            //FrostRay.gameObject.SetActive(true);
            Instantiate(FrostRay, spellPoint.position, spellPoint.rotation);
            
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
                player.spendMana(4);
            }
            else
            {
                Debug.Log("Not Enough Mana!"); //Give player feedback istead of this!
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
                    player.spendMana(2);
                }
                else
                {
                    Debug.Log("Not Enough Mana!");
                }
            }
            else
            {
                Stats player = character.GetComponent<Stats>();
                if (player.status.currentMana >= 2)
                {
                    Instantiate(FirePrefab, spellPoint.position, spellPoint.rotation);
                    player.spendMana(2);
                }
                else
                {
                    Debug.Log("Not Enough Mana!");
                }
            }
        }
    }
}