using UnityEngine;
using System;

namespace RE
{
    [CreateAssetMenu(fileName = "SoulDictionary", menuName = "States/SoulDictionary")][Serializable]
    public class SoulDictionary : ScriptableObject
    {
        [Header("Body")]
        public SoulTypeDictionary soulBodyDictionary;
        public SoulElementAuraDictionary soulAuraDictionary;
        public SoulPlanetDictionary soulPlanetDictionary;
        [Header("Paper")]
        public SoulTitleDictionary soulTitleDictionary;
        public SoulTypeFaceDictionary soulFaceDictionary;
        public SoulElementDictionary soulElementDictionary;
        public SoulPlanetPaperDictionary soulPlanetPaperDictionary;

    }
    
}