using RPG.Core;
using UnityEngine;
using System;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/CreateNewWeapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private GameObject weaponPrefab = null;
        [SerializeField] private AnimatorOverrideController animatorOverrideController = null;
        [SerializeField] private float range = 1f;
        [SerializeField] private float damage = 10f;
        [SerializeField] private float timeBetweenAttacks = .8f;
        [SerializeField] private bool isRrightHanded;
        [SerializeField] private Projectiles projectiles = null;
        private const string weaponName = "weapon";
        
        public void spawn(Transform rightTransform,Transform leftTransform, Animator animator)
        {
            destroyOldWeapon(rightTransform,leftTransform);
            
            if (weaponPrefab != null && animatorOverrideController != null)
            {
                GameObject old = Instantiate(weaponPrefab, checkHand(
                        rightTransform,leftTransform));
                
                old.name = weaponName;
                animator.runtimeAnimatorController = animatorOverrideController;
            }
        }

        private void destroyOldWeapon(Transform rightTransform,Transform leftTransform)
        {
            Transform oldWep = rightTransform.Find(weaponName);
            
            if (oldWep == null) oldWep = leftTransform.Find(weaponName);
            
            if (oldWep == null) return;

            oldWep.name = "destroyed";
            Destroy(oldWep.gameObject);
        }
        
        /*used to instantiate the projectiles*/
        public void shootProjectiles(Transform rightTransform,
                Transform leftTransform,Healt target)
        {
            Projectiles projectilesInstance = Instantiate(projectiles, 
                 checkHand(rightTransform, leftTransform)
                .position,Quaternion.identity);
            if(target != null)
                projectilesInstance.setTarget(target,damage);
        }

        private Transform checkHand(Transform rightTransform,Transform leftTransform)
        {
            Transform tmp = rightTransform;
            /*If the weapon is not right handed it is left.*/
            if (!isRrightHanded) 
                tmp = leftTransform;

            return tmp;
        }

        public bool hasProjectile()
        {
            return projectiles != null;
        }

        #region Getter&Setter

        public float Range
        {
            get => range;
            set => range = value;
        }
        public float Damage
        {
            get => damage;
            set => damage = value;
        }
        public float TimeBetweenAttacks
        {
            get => timeBetweenAttacks;
            set => timeBetweenAttacks = value;
        }

        #endregion
    }
}





























