using Balthazariy.Settings;
using UnityEngine;

namespace Balthazariy.Model
{
    internal class Rubber
    {
        private int _id;

        private float _swiffness;

        private Material _material;

        private RubberTypeEnumerators _type;

        public Rubber(int id, float swiffness, Material material, RubberTypeEnumerators type)
        {
            _id = id;
            _swiffness = swiffness;
            _material = material;
            _type = type;
        }
    }
}
