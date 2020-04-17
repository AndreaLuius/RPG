using UnityEngine;
using System;
using System.IO;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void save(string fileName)
        {
            var path = getDynamicPath(fileName);
            Debug.Log($"saving {getDynamicPath(fileName)}");
            using(FileStream stream = File.Open(path,FileMode.Create))
            {
                Transform playerTransform = getPlayerPosition();
                byte[] buffer = serializeVector(playerTransform.position);
                stream.Write(buffer, 0, buffer.Length);
            }
            
        }

        public void load(string fileName)
        {
            var path = getDynamicPath(fileName);
            Debug.Log($"load {getDynamicPath(fileName)}");
            using(FileStream stream = File.Open(path,FileMode.Open))
            {
                byte [] buffer = new byte[stream.Length];
                stream.Read(buffer,0,buffer.Length);

               Transform transform = getPlayerPosition();
               transform.position = deserializeVector(buffer);
            }
        }

        private byte[] serializeVector(Vector3 vector3)
        {
            byte[] converted = new byte[12];//every float is 4 we have xzy
            
            BitConverter.GetBytes(vector3.x).CopyTo(converted,0);//every flaot takes 4 byte
            BitConverter.GetBytes(vector3.y).CopyTo(converted, 4);//so we let all that space
            BitConverter.GetBytes(vector3.z).CopyTo(converted, 8);//for every byte 
            
            return converted;
        }

        private Vector3 deserializeVector(byte[] buffer)
        {
            Vector3 vector3 = new Vector3();
            vector3.x = BitConverter.ToSingle(buffer,0);
            vector3.y = BitConverter.ToSingle(buffer, 4);
            vector3.z = BitConverter.ToSingle(buffer, 8);

            return vector3;
        }

        private Transform getPlayerPosition()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        private string getDynamicPath(string fileName)
        {
            /*Application.persistentDataPath takes the dinamycally the absolute path
            the combine just combines 2 strings (with dinamyc slash based on OS wheres running)*/
            return Path.Combine(Application.persistentDataPath,fileName + ".sav");
        }
    }
}