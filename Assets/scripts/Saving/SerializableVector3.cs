using System;
using UnityEngine;

namespace RPG.Saving
{
    [Serializable]
    public class SerializableVector3
    {
        private float x, y, z;

        public SerializableVector3(Vector3 vector)
        {
            this.x = vector.x;
            this.y = vector.y;
            this.z = vector.z;
        }

        public Vector3 toVector()
        {
            return new Vector3(x, y, z);
        }
    }
}