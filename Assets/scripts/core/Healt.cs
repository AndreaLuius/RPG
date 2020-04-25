using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Healt : MonoBehaviour,ISaveable 
    {

        [SerializeField] float healt = 100f;
        public bool isDead { get; set; }

        public void takeDamage(float hitDamage)
        {
            /*the maximum value between 2 number
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
                GetComponent<ActionScheduler>().straigthStopAction();
                isDead = true;
            } 
        }
        
        #region ISavableImplements
        
        public object captureState()
        {
            return healt;
        }

        public void restoreState(object state)
        {
            healt = (float) state;
            if (healt <= 0)
            {
                death();
            }
        }
        
        #endregion
    }
}