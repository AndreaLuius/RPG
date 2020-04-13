using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            for (int i = 0; i < transform.childCount; i++)
            {   //circolar line... if last one then get to 0
                int tmp = (i + 1 < transform.childCount ? i + 1 : 0) ;
                Gizmos.DrawSphere(transform.GetChild(i).position,.3f);
                Gizmos.DrawLine(
                transform.GetChild(i).position,
                transform.GetChild(tmp).position);
            }
        }

    }
}