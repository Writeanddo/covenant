using UnityEngine;
using System.Collections;

namespace RE.City
{
    public class CityCanvas : MonoBehaviour
    {
        public void SetDialog(NPCDialogue npcDialogue)
        {
            Debug.Log("chegou aqui" + npcDialogue.name);
        }
    }

}
