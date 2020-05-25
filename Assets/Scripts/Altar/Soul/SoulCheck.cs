using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace RE.Soul
{
    public class SoulCheck : MonoBehaviour
    {
        [SerializeField] SoulChecker _soulChecker;
        [SerializeField] UnityEvent _trueAnswer;
        [SerializeField] UnityEvent _wrongAnswer;

        private SoulState _actualSoul;
        bool wrongValue = false;

        public void CheckDiscrepancy(SoulState soulState)
        {

        }

        public void ItemsToCheck(SoulState soulState, bool paperSign)
        {
            _actualSoul = soulState;
            for (int i = 0; i < _soulChecker.valuesToCheck.Length; i++)
            {
                if (!wrongValue)
                    SetValueTypes(_soulChecker.valuesToCheck[i]);
            }
            if (wrongValue)
            {
                wrongValue = false;
                if (paperSign)
                    _wrongAnswer.Invoke();
               //Descomentar caso for preciso validar acerto quando queima o papel errado
                // else
                   // _trueAnswer?.Invoke();
            }
            else
            {
                if (!paperSign)
                    _wrongAnswer?.Invoke();
                else
                    _trueAnswer?.Invoke();
            }
        }

        private void SetValueTypes(CheckValueType valueType)
        {
            switch (valueType)
            {
                case CheckValueType.SoulType:
                    CheckUniqueItem(_soulChecker.soulTypes, _actualSoul.soulPaperTypeFace);

                    break;
                case CheckValueType.SoulPlanet:
                    CheckUniqueItem(_soulChecker.soulPlanets, _actualSoul.soulPaperPlanet);

                    break;
                case CheckValueType.SoulTitle:
                    CheckUniqueItem(_soulChecker.soulTitles, _actualSoul.soulTitle);

                    break;
                case CheckValueType.SoulElement:
                    CheckUniqueItem(_soulChecker.soulElements, _actualSoul.soulPaperElement);

                    break;
                case CheckValueType.SoulName:
                    
                    CheckUniqueName(_soulChecker.soulNames, _actualSoul.soulName);

                    break;
                default:
                    break;
            }
        }

        private void CheckUniqueName(string[] nameList, string name)
        {
            bool nameCheck = false;
            foreach (string n in nameList)
            {
                if (name.Contains(n))
                    nameCheck = true;
            }
            if(!nameCheck)
                wrongValue = true;
        }

        private void CheckUniqueItem<T>(T[] soulTypes, T t)
        {
            foreach (var tItem in soulTypes)
            {
                if (!EqualityComparer<T>.Default.Equals(t, tItem))
                {
                    wrongValue = true;
                    break;
                }
            }
        }
    }

    public enum CheckValueType
    {
        SoulType,
        SoulPlanet,
        SoulTitle,
        SoulElement,
        SoulName
    }

}
