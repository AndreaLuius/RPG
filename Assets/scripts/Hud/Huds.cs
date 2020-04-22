using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Hud
{
    public class Huds : MonoBehaviour
    {
        [SerializeField] GameObject panel;


        public void closePanel()
        {
            panel.SetActive(false);
        }

        public void openPanel()
        {
            panel.SetActive(true);
        }
    }

}