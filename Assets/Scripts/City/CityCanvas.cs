using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace RE.City
{
    public class CityCanvas : MonoBehaviour
    {
        [SerializeField] GameObject _container;
        [SerializeField] UnityEvent _closeEvent;

        private CityManager _cityManager;
        private NPCDialogue _actualNPCDialogue;
        private int _actualDialogueIndex = 0;
        private GameObject _actualDialogue;
        private bool _finalDialog = false;

        private void Awake()
        {
            _cityManager = FindObjectOfType<CityManager>();
        }

        public void SetInitialDialogue(NPCDialogue npcDialogue)
        {
            _actualDialogueIndex = 0;
            _actualNPCDialogue = npcDialogue;
            SetDialog();
        }

        public void SetFinalDialogue(NPCDialogue npcDialogue)
        {
            Debug.Log("SetFinalDialogue");
            _actualNPCDialogue = npcDialogue;
            _finalDialog = true;
            SetDialog();
        }

        public void SetDialog()
        {
            _container.SetActive(true);
            InstantiateDialog();
        }

        public void NextDialogue()
        {
            _actualDialogueIndex++;
            if (_actualDialogueIndex >= _actualNPCDialogue._dialogues.Count)
                EndDialog();
            else
            {
                Destroy(_actualDialogue);
                InstantiateDialog();
            }
        }

        private void EndDialog()
        {
            if (_finalDialog)
            {
                _finalDialog = false;
                Destroy(_actualDialogue);
                _container.SetActive(false);
                _closeEvent.Invoke();
            }
            else
                _cityManager.SetTutorialScene();
        }

        private void InstantiateDialog()
        {
            Debug.Log("InstantiateDialog");
            Dialogue dialogue = _actualNPCDialogue._dialogues[_actualDialogueIndex];
            _actualDialogue = Instantiate(dialogue.dialogPrefab);
            _actualDialogue.transform.SetParent(_container.transform, false);
            TextMeshProUGUI textMesh = _actualDialogue.transform.Find("Phrase").GetComponent<TextMeshProUGUI>();
            textMesh.text = dialogue.phrase;
        }
    }

}
