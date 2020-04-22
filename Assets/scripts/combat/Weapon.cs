using System;
using UnityEngine;

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
        
        public void spawn(Transform whereToSpawn, Animator animator)
        {
            if (weaponPrefab != null && animatorOverrideController != null)
            {
                Instantiate(weaponPrefab, whereToSpawn);
                animator.runtimeAnimatorController = animatorOverrideController;
            }
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