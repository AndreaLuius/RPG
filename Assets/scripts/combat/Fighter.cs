using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float range = 2f;
        [SerializeField][Range(0.5f,4f)]float timeBetweenAttacks = 1f;
        private Transform target;
        private Mover mover;
        private ActionScheduler scheduler;
        private bool atThePosition = false;
        private Animator animator;
        private float lastAttackTime = 0;
    

        private void Start()
        {
            mover = GetComponent<Mover>();
            scheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            lastAttackTime += Time.deltaTime;

            if(target != null && !rangeControl())
            {
                mover.startMoving(target.position, range);
                atThePosition = true;
            }
            else if(target != null && rangeControl())
                attackBehavior();
            
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

        private void attackBehavior()
        {
            //this will trigger the Hit() event
            if(lastAttackTime >= timeBetweenAttacks)
            {
                animator.SetTrigger("attack");
                lastAttackTime = 0;
            }
        }

        public void stopExecution() 
        {
            target = null;
        }

        #region AnimationEvents
        private void Hit()
        {
            Healt healtTmp = target.GetComponent<Healt>();
            healtTmp.takeDamage(10f);
        }
        #endregion
    }
}