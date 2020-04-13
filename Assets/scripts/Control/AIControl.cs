using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIControl : MonoBehaviour
    {
        [SerializeField] float chaseTargetRange = 10f;
        private GameObject player;
        private Fighter fighter;
        private Healt healt;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            healt = GetComponent<Healt>();
        }

        private void Update() {
            if(healt.isDead) return;

            if(chaseDistanceControl() < chaseTargetRange)
                fighter.startAttack(player);
            else
                fighter.stopExecution();
        }

        private float chaseDistanceControl()
        {
            return Vector3.Distance(transform.position,
                     player.transform.position);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseTargetRange);
        }
    }
}