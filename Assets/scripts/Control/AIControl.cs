using UnityEngine;

namespace RPG.Control
{
    public class AIControl : MonoBehaviour
    {
        [SerializeField] float chaseTargetRange = 10f;

        private void Update() {

            if(chaseDistanceControl())
                print($"{this.name}should chase jim!");
        }

        private bool chaseDistanceControl()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(transform.position,
                     player.transform.position) < chaseTargetRange;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseTargetRange);
        }
    }
}