using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using RE.Soul;

namespace RE
{
    public class GameManager : MonoBehaviour
    {
        [Header("Cursor")]
        [SerializeField] Texture2D _arrow;

        [Header("Status Values")]
        [SerializeField] bool _hasStatus;
        [SerializeField] TextMeshPro _successTextStatus;
        [SerializeField] TextMeshPro _failTextStatus;
        [SerializeField] int _successNumber;
        [SerializeField] int _failNumber;
        [SerializeField] TextMeshPro _successText;
        [SerializeField] TextMeshPro _failText;

        [Header("GameObjects and Prefabs")]
        [SerializeField] int _nextScene;
        [SerializeField] Demon _demon;
        [SerializeField] Animator _sceneTransition;
        [SerializeField] GameObject _altarCanvasPrefab;
        [SerializeField] AudioClip _errorSound;
        [SerializeField] AudioClip _transitionOpenSound;
        [SerializeField] AudioClip _transitionCloseSound;
        public bool gameEnd = false;

        private AltarCamera _altarCamera;

        private Scene _actualScene;
        private AudioSource _audioSource;
        private int _successStatus = 0;
        private int _failStatus = 0;

        private void Awake()
        {
            _altarCamera = FindObjectOfType<AltarCamera>();
            _actualScene = SceneManager.GetActiveScene();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Cursor.SetCursor(_arrow, Vector2.zero, CursorMode.ForceSoftware);
            if (_hasStatus)
            {
                _successText.text = _successNumber.ToString();
                _failText.text = _failNumber.ToString();
            }
        }

        public void GameEnd() ///REFATORAR
        {
            gameEnd = true;
            _altarCamera.Duration = .4f;
            _altarCamera.ChromaticPulse();
            _audioSource.PlayOneShot(_transitionOpenSound);
            GameObject altarCanvas = Instantiate(_altarCanvasPrefab, transform.position, Quaternion.identity);
            altarCanvas.GetComponent<AltarCanvas>().SetStatus(CheckSuccess(), SetText(_successStatus, _successNumber), SetText(_failStatus, _failNumber));
        }

        private string SetText(int status, int number) => status.ToString() + " / " + number.ToString();

        private bool CheckSuccess()
        {
            if (_failStatus >= _failNumber)
                return false;
            else if (_successStatus >= _successNumber)
                return true;
            return false;

        }

        public void SetScore(bool success)
        {
            if (success)
                SetSuccess();
            else
                SetFail();
        }

        public void SetSuccess()
        {
            _successStatus++;
            _successTextStatus.text = _successStatus.ToString();
        }

        public void SetFail()
        {
            _failStatus++;
            _failTextStatus.text = _failStatus.ToString();
            _demon.SetStatus();
            _altarCamera.Duration = .4f;
            _altarCamera.PulseTime();
            _altarCamera.ChromaticPulse();
            _altarCamera.ShakeCamera();

            if (_failStatus >= _failNumber)
                GameEnd();
            else
                _audioSource.PlayOneShot(_errorSound);
        }

        public void SetChangeScene(bool transition = false)
        {
            if (transition)
            {
                _audioSource.PlayOneShot(_transitionCloseSound);
                StartCoroutine(Co_TransitionScene(_nextScene));
            }
            else
                SceneManager.LoadScene(_nextScene);
        }

        private IEnumerator Co_TransitionScene(int index)
        {
            _altarCamera.Duration = 1f;
            _altarCamera.ChromaticPulse();
            var character = GameObject.FindGameObjectWithTag("Player");
            character.GetComponent<Animator>().SetTrigger("transform");
            yield return new WaitForSeconds(0.75f);
            SceneManager.LoadScene(index);
        }

        public void ResetScene()
        {
            _audioSource.PlayOneShot(_transitionCloseSound);
            StartCoroutine(Co_ResetScene(_actualScene.buildIndex));
        }

        private IEnumerator Co_ResetScene(int index)
        {
            _sceneTransition.SetTrigger("start");
            yield return new WaitForSeconds(0.25f);
            SceneManager.LoadScene(index);
        }
    }
}
