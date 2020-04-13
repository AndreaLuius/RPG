using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        /*basically we are doing theese bool check because
        if we are attack we dont have to move and if we are in 
        a position to move we dont have attack (at the same time)
        later on we will set that as a (attack cursor and move cursor)
        so this differenciation will have sense*/
        private Fighter fighter;
        private Mover mover;
        private Healt healt;

        private void Start()
        {
            fighter = GetComponent<Fighter>();   
            mover = GetComponent<Mover>();
            healt = GetComponent<Healt>();
        }

        void Update()
        { 
            if(combatInteraction() || moveToCursor() || healt.isDead) return;
        }

        private bool moveToCursor()
        {
            RaycastHit raycastHit;

            bool hasHit = Physics.Raycast(getMouseRay(), out raycastHit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                    mover.startMoveAction(raycastHit.point,0f);
                
                return true;
            }
            return false;
        }

        private bool combatInteraction()
        {
            /*returns to rays all the things that it hits as an array*/
           RaycastHit[] rays =  Physics.RaycastAll(getMouseRay());
        
           foreach (RaycastHit ray in rays)
           {
                CombatTarget target = ray.transform.GetComponent<CombatTarget>();
                if(target == null) continue;
                //if the target is dead dont process it in the ray cast
                if(target.transform.GetComponent<Healt>().isDead) continue;
                if(Input.GetMouseButtonDown(0))
                    fighter.startAttack(target.gameObject);

                return true;
           }
           return false;
        }

        private Ray getMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
