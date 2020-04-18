using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private BinaryFormatter formatter;
        private Dictionary<string, object> states;//states are the savable entity
        
        private void Start()
        {
            formatter = new BinaryFormatter();
            states = new Dictionary<string, object>();
        }
        
        public void save(string fileName)
        {
            string path = getDynamicPath(fileName);
            savingFileOperation(path);
        }
        
        public void load(string fileName)
        {
            string path = getDynamicPath(fileName);
            readLoad(path);
        }
        
        #region fileOperation
        private void savingFileOperation(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Create))
            {//using because will close the stream when it ends
                formatter.Serialize(stream, captureState());
            }
        }
        private void readLoad(string path)
        {
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                restoreState(formatter.Deserialize(stream));
            }
        }
        #endregion
        
        /*taking all the entity that has a savable and putting them inside a map*/
        private Dictionary<string,object> captureState()
        {
            foreach (var savable in FindObjectsOfType<SavableEntity>())
            {
                states[savable.getUniqueIdentifier()] = savable.captureState();
            }
            return states;
        }
        
        /*just taking the value from the dictionary by key
        and then just putting it at the saved position */
        private void restoreState(object state)
        {
            /*filled by the file*/
            Dictionary<string, object> deserializedState = (Dictionary<string, object>) state; 
            
            foreach (var savable in FindObjectsOfType<SavableEntity>())
            {
                savable.restoreState(deserializedState[savable.getUniqueIdentifier()]);
            }

        }
        
        private string getDynamicPath(string fileName)
        {
            /*Application.persistentDataPath takes the dynamically the absolute path
            the combine just combines 2 strings (with dynamic slash based on OS wheres running)*/
            return Path.Combine(Application.persistentDataPath, fileName + ".sav");
        }
    }
}