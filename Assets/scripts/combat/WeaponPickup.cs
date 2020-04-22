using RPG.Hud;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup: MonoBehaviour
    {
        [SerializeField] private Weapon weapon;
        [SerializeField] private Huds hud;
        private Fighter fighter = null;
        public bool isCollectible = false;
        
        private void Update()
        {
            equipEnabling();
        }
        
        private void equipEnabling()
        {
            if (isCollectible && Input.GetKeyDown(KeyCode.F))
            {
                fighter.equipWeapon(weapon);
                hud.closePanel();
                Destroy(gameObject);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                hud.openPanel();
                isCollectible = true;
                fighter = other.GetComponent<Fighter>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            hud.closePanel();
            isCollectible = false;
            fighter = null;
        }
    }
}