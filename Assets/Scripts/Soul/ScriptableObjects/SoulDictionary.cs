using System;
using UnityEngine;

namespace RE.Soul
{
    [CreateAssetMenu(fileName = "SoulDictionary", menuName = "States/SoulDictionary")][Serializable]
    public class SoulDictionary : ScriptableObject
    {
        public SoulTypeDictionary soulTypeDictionary;
        public SoulPlanetDictionary soulPlanetDictionary;
        public SoulTitleDictionary soulTitleDictionary;
        public SoulElementDictionary soulElementDictionary;

    }
    
}