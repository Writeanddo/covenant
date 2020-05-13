using UnityEngine;
using System;

namespace RE
{
    [CreateAssetMenu(fileName = "SoulState", menuName = "States/SoulState")][Serializable]
    public class SoulState : ScriptableObject
    {
        [Header("Body")]
        public SoulType soulBodyType;
        public SoulPlanet soulBodyPlanet;
        [Header("Paper")]
        public string soulName;
        public SoulType soulPaperTypeFace;
        public SoulElement soulPaperElement;
        public SoulPlanet soulPaperPlanet;
        public SoulTitle soulTitle;
        public bool hasSeal;
    }

    
}
