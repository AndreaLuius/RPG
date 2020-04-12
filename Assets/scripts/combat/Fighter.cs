using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float range = 2f;
        private Transform target;
        private Mover mover;
        private ActionScheduler scheduler;
        

        private void Start()
        {
            mover = GetComponent<Mover>();
            scheduler = GetComponent<ActionScheduler>();
        }

        private void Update()
        {
            if(target != null && !rangeControl())
            {
                mover.startMoving(target.position, range);
            }
        }

        private bool rangeControl()
        {
            return Vector3.Distance(transform.position,target.position) < range;
        }

        public void startAttack(CombatTarget combatTarget)
        {
            scheduler.startAction(this);
            target = combatTarget.transform;
        }

        public void stopExecution() 
        {
            target = null;
        }
    }
}