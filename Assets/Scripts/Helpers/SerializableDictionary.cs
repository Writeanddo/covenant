using UnityEngine;
using System;
using System.Collections.Generic;

namespace RE
{
    public class SoulDictionary<TKey, TValue>
    { 
        [SerializeField] List<TKey> _keys;
        [SerializeField] List<TValue> _values;

        public TValue this[TKey key]
        {
            get
            {
                int index = _keys.IndexOf(key);
                return _values[index];
            }
        }
    }
}
