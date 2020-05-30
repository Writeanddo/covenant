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

        public void ChangeStatus(NPCStatus status)
        {
            npcStatus = status;
            ForceSerialization();
        }

        void ForceSerialization()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
            #endif
        }
    }
}
