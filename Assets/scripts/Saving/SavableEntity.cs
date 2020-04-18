using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Saving
{
    [ExecuteAlways]
    public class SavableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueIdentifier = "";

        public string getUniqueIdentifier()
        {
            return uniqueIdentifier;
        }

        /*just taking the position*/
        public object captureState()
        {
            return new SerializableVector3(transform.position);
        }

        /*just putting the value sto the saved position*/
        public void restoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = position.toVector();
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<ActionScheduler>().straigthStopAction();
        }
        
    #if UNITY_EDITOR
        /*
         * Basically we just create a class that works like a filter
         * this filter will be called something like (savable).
         * Savable obviously we have methods to restore some state of an entity
         * (taking the right value of it) and a method for capturing its state
         * (basically take the value that has to be serialized and saved in a file)
         * this has to be done obviously for every entity in the game, so we just
         * create a collection where to store all the savable occurrences
         * and then we just loop through them to and for any on em we
         * just save its ID and value in the list so that we can have
         * access to them easily and when we want to restore
         * we just recreate a loop where we restore all of em by key
         */
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            print("editing");

            /*if the path is not showing means that we are in the prefab
            so we return to avoid the unique identifier to be duplicated*/
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            /*used to serialize an object*/
            SerializedObject serializedObject = new SerializedObject(this);
            /*thanks to that we can have access to all the serialized element
            by using the property methods*/
            SerializedProperty property = serializedObject.FindProperty("uniqueIdentifier");

            /*we just set an id to a property that doesnt have one already*/
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
                /*we apply the modification so that unity knows about those*/
                serializedObject.ApplyModifiedProperties();
            }
        }
    #endif
        
    }
}