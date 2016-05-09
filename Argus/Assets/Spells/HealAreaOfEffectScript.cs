using UnityEngine;
using System.Collections;

namespace UnityStandardAssets._2D
{
    public class HealAreaOfEffectScript : MonoBehaviour {

        public float maxAOESize;//Maximum size of the AOE effect
        public float channelTime = 5; //How long you need to channel to get to the max Size

        private float currAOESize;//Current size of AOE
        private float damage;//The damage it will inflict on enemies
        public float Speed;//Speed of the AOE expansion
        private float rateOfExpansion;
        private bool stoppedChannel = false;//Check to make sure that the player can not rechannel the already in effect spell

        void OnTriggerEnter2D(Collider2D col)
        {


            if (col.isTrigger != true && col.gameObject.CompareTag("enemy"))//Check to see if the spell collides with a enemy and if it does then it takes damage.
            {
                col.gameObject.SendMessageUpwards("damageEnemy", damage);
            }
        }

        // Use this for initialization
        void Start() {

            damage = GameObject.Find("Player").GetComponent<Stats>().status.spellpower / 2;
            currAOESize = 1;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.R) && !stoppedChannel)//While Channeling increase the size of the AOE effect untill it reaches a ceiling
            {
                GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = true;//Disable movement for the player
                
                if (currAOESize < maxAOESize)
                {
                    currAOESize += (Time.deltaTime * maxAOESize) / channelTime;
                }
            }
            else
            {
                stoppedChannel = true;
                GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = false;//Re enable player movement
                if (transform.localScale.x < currAOESize)
                {
                   
                    rateOfExpansion = transform.localScale.x * Time.deltaTime * Speed;//Calculation on the rate that the object will expand at
                    transform.localScale = new Vector3(transform.localScale.x + rateOfExpansion, transform.localScale.y + rateOfExpansion, transform.localScale.z + rateOfExpansion); //Expand the AOE effect untill it reaches the channeled size
                }
                else
                {
                    Destroy(this.gameObject);//Destroy the object once fina size has been reached
                }
            }
        }
    }
}
