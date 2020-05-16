using System.Collections;
using UnityEngine;

namespace RE
{
    public class Paper : MonoBehaviour
    {
        private GameObject _tableLimits;
        private Animator _animator;

        private Camera _mainCamera;
        private CandleLight _candleLight;
        private Pen _pen;

        private Vector2 _mousePos;
        private float xMinPos;
        private float xMaxPos;
        private float yMinPos;
        private float yMaxPos;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _mainCamera = FindObjectOfType<Camera>();
            _candleLight = FindObjectOfType<CandleLight>();
            _candleLight.Interacted += PaperBurn;
            _pen = FindObjectOfType<Pen>();
            _pen.Interacted += PaperSign;
            _tableLimits = GameObject.FindGameObjectWithTag("TableLimits");
        }

        private void Start()
        {
            SetXYPos();
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
            }
        }

        private void OnMouseDrag()
        {
            Vector2 inputMousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _mousePos.x = Mathf.Clamp(inputMousePos.x, xMinPos, xMaxPos);
            _mousePos.y = Mathf.Clamp(inputMousePos.y, yMinPos, yMaxPos);
            transform.position = new Vector2(_mousePos.x, _mousePos.y);
        }

        public void PaperBurn()
        {
            StartCoroutine(Co_PaperBurn());
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
            _candleLight.Interacted -= PaperBurn;
            _pen.Interacted -= PaperSign;
            Destroy(gameObject);
        }

    }
}
