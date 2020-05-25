using System;
using System.Collections.Generic;
using UnityEngine;
using RE.Soul;

namespace RE
{
    [CreateAssetMenu(fileName = "SoulChecker", menuName = "States/SoulChecker")]
    [Serializable]
    public class SoulChecker : ScriptableObject
    {
        public CheckValueType[] valuesToCheck;
        public SoulType[] soulTypes;
        public SoulElement[] soulElements;
        public SoulPlanet[] soulPlanets;
        public SoulTitle[] soulTitles;
        public string[] soulNames;
    }
}