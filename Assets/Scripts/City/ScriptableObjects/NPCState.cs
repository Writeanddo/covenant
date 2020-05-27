using System;
using UnityEngine;

namespace RE
{
    [CreateAssetMenu(fileName = "NPCState", menuName = "States/NPCState")]
    [Serializable]
    public class NPCState : ScriptableObject
    {
        public string id;
        public NPCStatus npcStatus;
        public int tutorialLevelIndex;
        public bool greetingAnimation;
    }
}
