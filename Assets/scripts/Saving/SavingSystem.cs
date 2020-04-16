using UnityEngine;
using System.IO;
using System.Text;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        public void save(string fileName)
        {
            var path = getDynamicPath(fileName);
            Debug.Log($"saving {getDynamicPath(fileName)}");
            FileStream stream = File.Open(path,FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes("hello world");
            stream.Write(bytes,0,bytes.Length);
            stream.Close();
        }


        public void load(string fileName)
        {
            Debug.Log($"load {getDynamicPath(fileName)}");
        }

        private string getDynamicPath(string fileName)
        {
            /*Application.persistentDataPath takes the dinamycally the absolute path
            the combine just combines 2 strings (with dinamyc slash based on OS wheres running)*/
            return Path.Combine(Application.persistentDataPath,fileName + ".sav");
        }
    }
}