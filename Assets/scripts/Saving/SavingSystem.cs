using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        private const string lastSceneBuildIndex = "lastSceneBuildIndex";

        public IEnumerator loadLastScene(string fileName)
        {
            Dictionary<string, object> state = loadFile(fileName);
            int buildIndex = SceneManager.GetActiveScene().buildIndex;

            if (state.ContainsKey(lastSceneBuildIndex))
                buildIndex = (int)state[lastSceneBuildIndex];

            yield return SceneManager.LoadSceneAsync(buildIndex);
            restoreState(state);
        }

        public void save(string saveFile)
        {
            Dictionary<string, object> state = loadFile(saveFile);
            captureState(state);
            this.saveFile(saveFile, state);
        }

        public void load(string fineName)
        {
            restoreState(loadFile(fineName));
        }

        #region FileOperation

        public void delete(string saveFile)
        {
            File.Delete(getDynamicPath(saveFile));
        }

        private void saveFile(string fileName, object state)
        {
            string path = getDynamicPath(fileName);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> loadFile(string saveFile)
        {
            string path = getDynamicPath(saveFile);
            if (!File.Exists(path))
                return new Dictionary<string, object>();

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
        #endregion

        private void captureState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
                state[saveable.getUniqueIdentifier()] = saveable.captureState();

            state[lastSceneBuildIndex] = SceneManager.GetActiveScene().buildIndex;
        }

        private void restoreState(Dictionary<string, object> state)
        {
            string id = "";
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                id = saveable.getUniqueIdentifier();
                if (state.ContainsKey(id))
                    saveable.restoreState(state[id]);
            }
        }

        private string getDynamicPath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".sav");
        }
    }
}