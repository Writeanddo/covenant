using System;
using System.Collections.Generic;
using UnityEngine;

namespace RE
{
    [CreateAssetMenu(fileName = "GameState", menuName = "States/GameState")]
    [Serializable]
    public class GameState : ScriptableObject
    {
        public string actualId = null;
        public Vector2 characterPosition = new Vector2(24.5f, 2.85f);
        public bool tempZeroBool;

        public void TempZeroGameState()
        {
            if (!tempZeroBool)
                return;

            tempZeroBool = false;
            actualId = null;
        }

        public void SetActualId(string id)
        {
            actualId = id;
        }

        public void SetCharacterPosition(Vector2 position)
        {
            characterPosition = position;
        }
    }
   
}