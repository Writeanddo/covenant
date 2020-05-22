using System;
using System.Collections.Generic;
using UnityEngine;

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

    [Serializable]
    public class SoulAttributeSprite
    {
        public Sprite _sprite;
    }

    [Serializable]
    public class SoulAttributeSpriteAndGameObject
    {
        public Sprite _sprite;
        public GameObject _gameObject;
    }

    public enum SoulPlanet
    {
        Mercury, Venus, Mars, Jupiter, Saturn, Moon, Sun
    }

    public enum SoulElement
    {
        Earth, Water, Fire, Air
    }

    public enum SoulTitle
    {
        King, Duke, Marquis
    }

    public enum SoulType
    {
        Default, Punk1, Punk2, Fat, Girl, Bald
    }

    [Serializable] public class SoulPlanetDictionary : SoulDictionary<SoulPlanet, SoulAttributeSprite> { }
    [Serializable] public class SoulTitleDictionary : SoulDictionary<SoulTitle, SoulAttributeSprite> { }
    [Serializable] public class SoulElementDictionary : SoulDictionary<SoulElement, SoulAttributeSpriteAndGameObject> { }
    [Serializable] public class SoulTypeDictionary : SoulDictionary<SoulType, SoulAttributeSpriteAndGameObject> { }
}
