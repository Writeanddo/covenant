using System.Collections.Generic;
using UnityEngine;

namespace RE
{
    [CreateAssetMenu(fileName = "GameState", menuName = "States/GameState")]
    public class GameState : ScriptableObject
    {
        public string actualId;
        public Vector2 characterPosition;
        public bool tempZeroBool;

        public void TempZeroGameState()
        {
            if (!tempZeroBool)
                return;

            tempZeroBool = false;
            actualId = null;
            characterPosition = new Vector2(24.5f, 2.85f);
        }
    }

    
   
}