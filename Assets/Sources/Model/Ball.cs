using UnityEngine;
using Balthazariy.Settings;

namespace Balthazariy.Model
{
    public class Ball
    {
        private int _id;

        private Sprite _sprite;

        private float _mass;

        private BallTypeEnumerators _type;

        public Ball(int id, Sprite sprite, float mass, BallTypeEnumerators type)
        {
            _id = id;
            _sprite = sprite;
            _mass = mass;
            _type = type;
        }
    }
}