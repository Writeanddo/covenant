using System;
using System.Linq;
using UnityEngine;
using TMPro;

namespace RE.Soul
{
    public class SoulSpawner : MonoBehaviour
    {
        [SerializeField] SoulDictionary _soulDictionary;
        [SerializeField] GameObject _paperPrefab;
        [SerializeField] Transform _waypointPaper;

        private SoulState SoulState { get; set; }
        private Transform Waypoint { get; set; }
        private GameObject BodyType { get; set; }
        private GameObject BodyAura { get; set; }
        private Sprite BodyPlanet { get; set; }
        private Sprite PaperTypeFace { get; set; }
        private Sprite PaperTitle { get; set; }
        private Sprite PaperElement { get; set; }
        private Sprite PaperPlanet { get; set; }

        private string PapertName { get; set; }

        public GameObject SpawnSoul(SoulState soulState, Transform waypoint)
        {
            SoulState = soulState;
            Waypoint = waypoint;
            var soulProps = Enum.GetValues(typeof(SoulProp)).Cast<SoulProp>();
            foreach (var prop in soulProps)
            {
                SetSoulProps(prop);
            }
            return InstantiateSoul();
        }

        public GameObject SpawnPaper(SoulState soulState)
        {
            SoulState = soulState;
            var soulProps = Enum.GetValues(typeof(SoulProp)).Cast<SoulProp>();
            foreach (var prop in soulProps)
            {
                SetSoulProps(prop);
            }
            return InstantiatePaper();
        }

        private GameObject InstantiateSoul()
        {
            var body = Instantiate(BodyType, Waypoint.position, Quaternion.identity);
            Instantiate(BodyAura, Waypoint.position, Quaternion.identity, body.transform);
            body.transform.Find("planet").GetComponent<SpriteRenderer>().sprite = BodyPlanet;

            return body;
        }

        private GameObject InstantiatePaper()
        {
            var paper = Instantiate(_paperPrefab, _waypointPaper.position, Quaternion.identity);
            paper.transform.Find("name").GetComponent<TextMeshPro>().text = PapertName;
            paper.transform.Find("element").GetComponent<SpriteRenderer>().sprite = PaperElement;
            paper.transform.Find("planet").GetComponent<SpriteRenderer>().sprite = PaperPlanet;
            paper.transform.Find("title").GetComponent<SpriteRenderer>().sprite = PaperTitle;
            paper.transform.Find("typeface").GetComponent<SpriteRenderer>().sprite = PaperTypeFace;

            return paper;
        }

        private void SetSoulProps(SoulProp soulProp)
        {
            switch (soulProp)
            {
                case SoulProp.Type:
                    BodyType = _soulDictionary.soulTypeDictionary[SoulState.soulBodyType]._gameObject;
                    PaperTypeFace = _soulDictionary.soulTypeDictionary[SoulState.soulPaperTypeFace]._sprite;

                    break;
                case SoulProp.Element:
                    BodyAura = _soulDictionary.soulElementDictionary[SoulState.soulPaperElement]._gameObject; // FIX FIX FIX
                    PaperElement = _soulDictionary.soulElementDictionary[SoulState.soulPaperElement]._sprite;

                    break;
                case SoulProp.Planet:
                    BodyPlanet = _soulDictionary.soulPlanetDictionary[SoulState.soulBodyPlanet]._sprite;
                    PaperPlanet = _soulDictionary.soulPlanetDictionary[SoulState.soulPaperPlanet]._sprite;

                    break;
                case SoulProp.Title:
                    PaperTitle = _soulDictionary.soulTitleDictionary[SoulState.soulTitle]._sprite;

                    break;
                case SoulProp.Name:
                    PapertName = SoulState.soulName;

                    break;
                default:
                    break;
            }
        }
    }

    public enum SoulProp
    {
        Type,
        Element,
        Planet,
        Title,
        Name
    }
}
