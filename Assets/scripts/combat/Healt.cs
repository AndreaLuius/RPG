using UnityEngine;

namespace RPG.Combat
    {
    public class Healt : MonoBehaviour {

        [SerializeField] float healt = 100f;

        public void takeDamage(float hitDamage)
        {
            /*the maximun value between 2 number
            we have 0 so i cannot go under 0 */
            healt = Mathf.Max(healt - hitDamage,0f);
        }
    }
}