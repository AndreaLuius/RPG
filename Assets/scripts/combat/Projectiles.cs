using System;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectiles : MonoBehaviour
    {
        [SerializeField] private float speed = 1f;
        [SerializeField] private GameObject hitEffect = null;
        [SerializeField] private float maxLifeTime = 10f;
        private Healt target = null;
        private CapsuleCollider targetCollider;
        private float damage = 0;

        private void Start()
        {
            targetCollider = target.GetComponent<CapsuleCollider>();
            transform.LookAt(getAimLocation());
        }

        void Update()
        {
            /*used to make something move to a specified direction*/
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (target == other.tag.Equals("dynamic") ||
                    target == other.tag.Equals("Player"))
            {
                if (!target.isDead)
                {
                    if (hitEffect != null)
                        Instantiate(hitEffect, getAimLocation(), transform.rotation);

                    target.takeDamage(damage);
                    Destroy(gameObject);
                }
            }
        }


        #region Getter&Setter

        private Vector3 getAimLocation()
        {
            if (targetCollider == null)
                return target.transform.position;

            return target.transform.position + (targetCollider.height * Vector3.up) / 2;
        }

        public void setTarget(Healt target, float damage)
        {
            this.target = target;
            this.damage = damage;
            Destroy(gameObject, maxLifeTime);
        }

        #endregion
    }
}
