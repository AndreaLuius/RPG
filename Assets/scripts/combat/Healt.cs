using UnityEngine;

namespace RPG.Combat
{
    public class Healt : MonoBehaviour {

        [SerializeField] float healt = 100f;
        public bool isDead{get;set;}

        void Start()
        {
            isDead = false;    
        }

        public void takeDamage(float hitDamage)
        {
            /*the maximun value between 2 number
            we have 0 so i cannot go under 0 */
            if(!isDead)
            {
                healt = Mathf.Max(healt - hitDamage,0f);
                death();
            }
        }

        public void death()
        {
            if(healt == 0)
            {
                GetComponent<Animator>().SetTrigger("die");
                isDead = true;
            } 
        }
    }
}