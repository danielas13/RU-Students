using UnityEngine;
using System.Collections;
namespace UnityStandardAssets._2D
{
    public class InstantDisable : MonoBehaviour
    {
        private Stats stats;
        private float spendMana = 1;
        private bool stoppedChannel = false;//Check to make sure that the player can not rechannel the already in effect spell
        void Update()
        {
            if(spendMana < 0)
            {
                stats.spendMana(1);
                spendMana = 1;
            }
            spendMana -= Time.deltaTime;
            if (!Input.GetKey(KeyCode.T))
            {
                GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = false;//Re-enable movement for the player
                Destroy(this.gameObject);
            }
        }
        void Start()
        {
            GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = true ;//Disable movement for the player
            stats = GameObject.Find("Player").GetComponent<Stats>();
        }

        /* // Update is called once per frame

         void OnDisable()
         {
             //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = false;//Disable movement for the player
         }
         void OnEnable()
         {
             //GameObject.Find("Player").GetComponent<PlatformerCharacter2D>().isChanneling = true ;//Re-enable movement for the player
         }*/
    }
}

