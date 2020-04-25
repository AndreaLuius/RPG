using UnityEngine;

namespace RPG.Core
{
    public class EffectDestroyer : MonoBehaviour
    {
        /*
         Remember you cannot destroy something that is
         serialized (if you are not destrying the entire
         gameobject it self) that would create some data
         loss
        */
        private ParticleSystem particle;

        private void Start()
        {
            particle = GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (!particle.IsAlive())
                Destroy(gameObject);
        }
    }
}