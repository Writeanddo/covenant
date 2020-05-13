using UnityEngine;
using System;
using System.Collections.Generic;

namespace RE
{
    public class Soul : MonoBehaviour
    {

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

    public enum SoulType {
        Default
    }

    [Serializable] public class SoulPlanetDictionary : SoulDictionary<SoulPlanet, Sprite> { }
    [Serializable] public class SoulPlanetPaperDictionary : SoulDictionary<SoulPlanet, Sprite> { }
    [Serializable] public class SoulTitleDictionary : SoulDictionary<SoulTitle, Sprite> { }
    [Serializable] public class SoulElementDictionary : SoulDictionary<SoulElement, Sprite> { }
    [Serializable] public class SoulElementAuraDictionary : SoulDictionary<SoulElement, GameObject> { }
    [Serializable] public class SoulTypeDictionary : SoulDictionary<SoulType, GameObject> { }
    [Serializable] public class SoulTypeFaceDictionary : SoulDictionary<SoulType, Sprite> { }
}
