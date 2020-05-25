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

    public class Dialogue
    {
        public string Author { get; set; }
        public string Phrase { get; set; }
    }
}
