using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterLove.Collections
{
    public class ObjectPoolWithRandomness<T> : ObjectPool<T>
    {
        public long randomness;
        public ObjectPoolWithRandomness(Func<T> factoryFunc, long randomness, int initialSize) : base(factoryFunc, initialSize)
        {
            this.randomness = randomness;
        }
    }
}
