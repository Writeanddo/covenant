using UnityEngine;
using UnityEditor;

namespace RE
{
    [CreateAssetMenu(fileName = "GameState", menuName = "States/GameState")]
    public class GameState : ScriptableObject
    {
        public string actualId;
        public Vector2 playerPosition;
    }
}