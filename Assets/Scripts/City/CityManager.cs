using UnityEngine;
using System.Collections.Generic;
using System;

namespace RE.City
{
    public class CityManager : MonoBehaviour
    {
        [SerializeField] List<NPC> _npcs;
        [SerializeField] GameState _gameState;

        void Start()
        {
            SetNPCAnimationAndDialog();
        }

        private void SetNPCAnimationAndDialog()
        {
            if (_gameState.actualId == null)
                return;

            NPC npc = _npcs.Find(s => s.id == _gameState.actualId);
            if (npc != null)
            {
                npc.SetNPCConclusion();
            }
        }
    }
}
