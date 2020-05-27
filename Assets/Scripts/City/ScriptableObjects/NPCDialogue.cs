using System;
using System.Collections.Generic;
using UnityEngine;

namespace RE
{
    [CreateAssetMenu(fileName = "NPCDialogue", menuName = "States/NPCDialogue")]
    [Serializable]
    public class NPCDialogue : ScriptableObject
    {
        public List<Dialogue> _dialogues;
    }

    [Serializable]
    public class Dialogue
    {
        public string phrase;
        public GameObject dialogPrefab;
    }
}
