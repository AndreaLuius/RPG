using UnityEngine;

namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private SavingSystem savingSystem;

        private void Start()
        {
            savingSystem = GetComponent<SavingSystem>();   
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                savingSystem.save(defaultSaveFile);
            }else if(Input.GetKeyDown(KeyCode.L))
            {
                savingSystem.load(defaultSaveFile);
            }    
        }
    }
}