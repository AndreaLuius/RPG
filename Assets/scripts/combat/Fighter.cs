using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float range = 2f;
        [SerializeField][Range(0.5f,4f)]float timeBetweenAttacks = 1f;
        [SerializeField] float fighterSpeed = .8f;
        private Healt target;
        private Mover mover;
        private Animator animator;
        private ActionScheduler scheduler;
        private bool atThePosition = false;
        private float lastAttackTime = Mathf.Infinity;//the first attack shouldnt wait
    

        public bool canTarget()
        {
            if(target == null) return true;
            if(target.isDead) return true;

            return false;
        }

        private void Start()
        {
            mover = GetComponent<Mover>();
            scheduler = GetComponent<ActionScheduler>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            lastAttackTime += Time.deltaTime;

            if(target == null || target.isDead) return; 

            if(!rangeControl())
            {
                mover.startMoving(target.transform.position, range,fighterSpeed);
                atThePosition = true;
            }
            else
                attackBehavior();
        }

        private bool rangeControl()
        {
            return Vector3.Distance(transform.position,target.transform.position) < range;
        }

        public void startAttack(GameObject combatTarget)
        {
            scheduler.startAction(this);
            target = combatTarget.GetComponent<Healt>();
        }

        private void attackBehavior()
        {
            transform.LookAt(target.transform);
            //this will trigger the Hit() event
            if(lastAttackTime >= timeBetweenAttacks)
            {
                swapTriggerControl("stopAttacking","attack");
                lastAttackTime = 0;
            }
        }

        public void stopExecution() 
        {
            swapTriggerControl("attack","stopAttacking");
            target = null;
            mover.stopExecution();
        }

        private void swapTriggerControl(string triggerToReset,string triggerToStart)
        {
            /*just to be a safe control
            when you have 2 elements that 
            switch 1 to other just reset 
            them so that you dont have any 
            (already on) problem*/
            animator.ResetTrigger(triggerToReset);
            animator.SetTrigger(triggerToStart);
        }

        #region AnimationEvents
        private void Hit()
        {
            if(target != null)
                target.takeDamage(10f);
        }
        #endregion
    }
}