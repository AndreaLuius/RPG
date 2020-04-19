using System;
using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagment
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private SavingSystem savingSystem;

        private void Awake()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        private IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();

            fader.fadeOutImmediatly();
            yield return savingSystem.loadLastScene(defaultSaveFile);
            yield return fader.fadeIn(1f);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
                save();
            else if (Input.GetKeyDown(KeyCode.L))
                load();
        }

        public void save()
        {
            savingSystem.save(defaultSaveFile);
        }

        public void load()
        {               
            savingSystem.load(defaultSaveFile);
        }
    }
}