using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RE
{
    public class Paper : MonoBehaviour
    {
        [SerializeField] float _speed;
        [SerializeField] AudioClip _passSound;
        [SerializeField] AudioClip _getSound;
        private Transform _paperWaypoint;
        private GameObject _tableLimits;
        private Animator _animator;
        private AudioSource _audioSource;

        private GameManager _gameManager;
        private Camera _mainCamera;
        private CandleLight _candleLight;
        private Pen _pen;

        private Vector2 _mousePos;
        private float xLimitPos;
        private float xMinPos;
        private float xMaxPos;
        private float yMinPos;
        private float yMaxPos;

        private bool miniPaper = true;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _audioSource = GetComponent<AudioSource>();
            _mainCamera = FindObjectOfType<Camera>();
            _gameManager = FindObjectOfType<GameManager>();
            _candleLight = FindObjectOfType<CandleLight>();
            _candleLight.Interacted += PaperBurn;
            _pen = FindObjectOfType<Pen>();
            _pen.Interacted += PaperSign;
            _paperWaypoint = GameObject.FindGameObjectWithTag("PaperWaypoint").transform;
            _tableLimits = GameObject.FindGameObjectWithTag("TableLimits");
        }

        private void Start()
        {
            if (!_gameManager.gameEnd)
            {
                SetXYPos();
                _audioSource.PlayOneShot(_passSound);
                StartCoroutine(Co_MoveToWaypoint());
            }
        }

        private IEnumerator Co_MoveToWaypoint()
        {
            while (transform.position != _paperWaypoint.position)
            {
                transform.position = Vector2.MoveTowards(transform.position, _paperWaypoint.position, _speed * Time.deltaTime);
                yield return null;
            }
            yield return null;
        }

        private void SetXYPos()
        {
            foreach (Transform child in _tableLimits.transform)
            {
                if (child.name == "yMax")
                    yMaxPos = child.position.y;
                if (child.name == "yMin")
                    yMinPos = child.position.y;
                if (child.name == "xMax")
                    xMaxPos = child.position.x;
                if (child.name == "xMin")
                    xMinPos = child.position.x;
                if (child.name == "xLimit")
                    xLimitPos = child.position.x;
            }
        }
        private void OnMouseDown()
        {
            _audioSource.PlayOneShot(_getSound);
        }

        private void OnMouseDrag()
        {
            Vector2 inputMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _mousePos.x = Mathf.Clamp(inputMousePos.x, xMinPos, xMaxPos);
            _mousePos.y = Mathf.Clamp(inputMousePos.y, yMinPos, yMaxPos);
            transform.position = new Vector2(_mousePos.x, _mousePos.y);
            if (!miniPaper && transform.position.x < xLimitPos)
                SetPaperAsMini(true);
            else if (miniPaper && transform.position.x > xLimitPos)
                SetPaperAsMini(false);
        }

        private void SetPaperAsMini(bool isMini)
        {
            miniPaper = isMini;
            _animator.SetBool("miniPaper", isMini);
        }

        public void PaperBurn()
        {
            StartCoroutine(Co_PaperBurn());
            _candleLight.Interacted -= PaperBurn;
        }

        private IEnumerator Co_PaperBurn()
        {
            _animator.SetTrigger("setFire");
            yield return new WaitForSeconds(.4f);
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            StartCoroutine(Co_DestroyPaper());
        }

        public void PaperSign()
        {
            StartCoroutine(Co_PaperSign());
            _pen.Interacted -= PaperSign;
        }

        private IEnumerator Co_PaperSign()
        {
            _animator.SetTrigger("setSign");
            yield return new WaitForSeconds(1f);
            StartCoroutine(Co_DestroyPaper());
        }

        private IEnumerator Co_DestroyPaper()
        {
            yield return new WaitForSeconds(.3f);
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _candleLight.Interacted -= PaperBurn;
            _pen.Interacted -= PaperSign;
        }

    }
}
