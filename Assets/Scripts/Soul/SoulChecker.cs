using System;
using System.Collections.Generic;
using UnityEngine;

namespace RE.Soul
{
    [CreateAssetMenu(fileName = "SoulChecker", menuName = "States/SoulChecker")]
    [Serializable]
    public class SoulChecker : ScriptableObject
    {
        public CheckValueType[] _valuesToCheck;
        public SoulType[] soulTypes;
        public SoulElement[] soulElements;
        public SoulPlanet[] soulPlanets;
        public SoulTitle[] soulTitles;
        public string[] soulNames;

        private SoulState _actualSoul;
        bool wrongValue = false;

        public void CheckDiscrepancy(SoulState soulState)
        {

        }

        public void ItemsToCheck(SoulState soulState)
        {
            _actualSoul = soulState;
            for (int i = 0; i < _valuesToCheck.Length; i++)
            {
                if (!wrongValue)
                    SetValueTypes(_valuesToCheck[i]);
            }
            if (wrongValue)
            {
                wrongValue = false;
                Debug.Log("Wrong Info!!");
            }
            else
            {
                Debug.Log("Correct Info!!");
            }
        }


        private void SetValueTypes(CheckValueType valueType)
        {
            switch (valueType)
            {
                case CheckValueType.SoulType:
                    foreach (var s in soulTypes)
                    {
                        if (_actualSoul.soulPaperTypeFace != s)
                        {
                            wrongValue = true;
                            break;
                        }
                    }
                    break;
                case CheckValueType.SoulPlanet:
                    foreach (var s in soulPlanets)
                    {
                        if (_actualSoul.soulPaperPlanet != s)
                        {
                            wrongValue = true;
                            break;
                        }
                    }
                    break;
                case CheckValueType.SoulTitle:
                    foreach (var s in soulTitles)
                    {
                        if (_actualSoul.soulTitle != s)
                        {
                            wrongValue = true;
                            break;
                        }
                    }
                    break;
                case CheckValueType.SoulElement:
                    foreach (var s in soulElements)
                    {
                        if (_actualSoul.soulPaperElement != s)
                        {
                            wrongValue = true;
                            break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /*private bool CheckUniqueItem(T[] soulTypes, T t)
        {
            foreach (var tItem in soulTypes)
            {
                var prop = t.GetType();
                prop.GetValue(_actualSoul);
                return false;
            }

        } */
    }

    public enum CheckValueType
    {
        SoulType,
        SoulPlanet,
        SoulTitle,
        SoulElement
    }

}