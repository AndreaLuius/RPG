using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        [SerializeField] float maxSpeed = 5.6f;
        public NavMeshAgent navMesh;
        private Animator animator;
        private Healt healt;
        private bool canceler = true;
    

        private void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            healt = GetComponent<Healt>();
        }

        private void Update()
        {
            if (healt.isDead)
                navMesh.enabled = false;
            
            movementAnimation();
        }

        private void movementAnimation()
        {
            Vector3 velocity = navMesh.velocity;
            /*transform a passed velocity from global to local
            and its useful using the animator because it just cares
            about the local velocity to work*/
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            /*we use the z axis base on movement animation*/
            float speed = localVelocity.z;
            animator.SetFloat("speed", speed);
        }

        public void startMoving(Vector3 destination, float range,float speed)
        {
            GetComponent<Animator>().SetTrigger("stopAttacking");

            navMesh.speed = maxSpeed * Mathf.Clamp01(speed);
            navMesh.SetDestination(destination);
            navMesh.stoppingDistance = range;
        }

        public void startMoveAction(Vector3 destination,float range,float speed)
        {
            GetComponent<ActionScheduler>().startAction(this);
            startMoving(destination,range,speed);
        }

        public void stopExecution()
        {
            navMesh.ResetPath();
        }
        
        
        #region ISavableImplements
        
        public object captureState()
        {
            return new SerializableVector3(transform.position);
        }
        
        public void restoreState(object state)
        { 
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().straigthStopAction();
        }
        
        #endregion
    }
}
