using UnityEngine;
using System.Collections;

namespace RPG.SceneManagment
{
    public class Fader : MonoBehaviour
    {
        public float fadeOutTime = 3f;
        public float fadeInTime = 3f;

        private CanvasGroup canvasGroup;
        
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator fadeOut(float amountTime)
        {
            while(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime / amountTime;

                yield return null;
            }
        }
        public IEnumerator fadeIn(float amountTime)
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= (Time.deltaTime / amountTime) ;

                yield return null;
            }
        }
    }
}