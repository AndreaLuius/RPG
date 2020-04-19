using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";
        static Dictionary<string, SaveableEntity> globalDictionary = 
                   new Dictionary<string, SaveableEntity>();

        public object captureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (ISaveable saveable in GetComponents<ISaveable>())
                state[saveable.GetType().ToString()] = saveable.captureState();
            
            return state;
        }

        public void restoreState(object state)
        {
            Dictionary<string, object> stateDict = (Dictionary<string, object>)state;
            string key = "";
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            { 
                key = saveable.GetType().ToString();
                if (stateDict.ContainsKey(key))
                    saveable.restoreState(stateDict[key]);
            }
        }
        
        public string getUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        #region UnityEditor

#if UNITY_EDITOR
        private void Update() {
            if (Application.IsPlaying(gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");
            
            if (string.IsNullOrEmpty(property.stringValue) || !isUnique(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }

            globalDictionary[property.stringValue] = this;
        }
#endif

        private bool isUnique(string candidate)
        {
            if (!globalDictionary.ContainsKey(candidate)) return true;

            if (globalDictionary[candidate] == this) return true;

            if (globalDictionary[candidate] == null)
            {
                globalDictionary.Remove(candidate);
                return true;
            }

            if (globalDictionary[candidate].getUniqueIdentifier() != candidate)
            {
                globalDictionary.Remove(candidate);
                return true;
            }

            return false;
        }
        #endregion
    }
}