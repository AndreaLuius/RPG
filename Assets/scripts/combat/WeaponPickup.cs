using System;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup: MonoBehaviour
    {
        [SerializeField] private Weapon weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Player"))
            {
                print("here");
                other.GetComponent<Fighter>().equipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}