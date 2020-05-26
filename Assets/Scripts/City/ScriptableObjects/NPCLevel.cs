using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RE
{
    [CreateAssetMenu(fileName = "NPCState", menuName = "States/NPCState")]
    [Serializable]
    public class NPCState : ScriptableObject
    {
        public string id;
        public GameObject NPC;
        public Scene scene;
    }
}
