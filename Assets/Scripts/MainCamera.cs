using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

namespace RE
{
    [RequireComponent(typeof(PostProcessVolume))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] float _shakeDuration;
        [SerializeField] float _shakeIntensity;
        [SerializeField] float _chromaticFXTimer;
        [SerializeField] float _chromaticFXSpeed;
        private Vector3 _originalPos;

        private PostProcessVolume _ppVolume;
        private ChromaticAberration _chromaticAberration;

        private void Awake()
        {
            _ppVolume = FindObjectOfType<PostProcessVolume>();
            _ppVolume.profile.TryGetSettings(out _chromaticAberration);
        }

        public void ShakeCamera()
        {
            StartCoroutine(Co_ShakeCamera());
        }

        private IEnumerator Co_ShakeCamera()
        {
            float shakeDuration = _shakeDuration;
            _originalPos = transform.position;
            while (shakeDuration > 0)
            {
                transform.position = _originalPos + Random.insideUnitSphere * _shakeIntensity;
                shakeDuration -= Time.deltaTime * 1f;
                yield return null;
            }
            transform.position = _originalPos;
        }

        public void ChromaticPulse()
        {
            StartCoroutine(Co_ChromaticPulse());
        }

        private IEnumerator Co_ChromaticPulse()
        {
            float timer = _chromaticFXTimer;
            float initialIntensity = _chromaticAberration.intensity.value;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                if (_chromaticAberration.intensity.value < 1)
                    _chromaticAberration.intensity.value += _chromaticFXSpeed;
                yield return null;
            }
            while (timer < _chromaticFXTimer)
            {
                timer += Time.deltaTime;
                if (_chromaticAberration.intensity.value > initialIntensity)
                    _chromaticAberration.intensity.value -= _chromaticFXSpeed;
                yield return null;
            }
        }

        public void PulseTime()
        {
            StartCoroutine(Co_PulseTime());
        }

        private IEnumerator Co_PulseTime()
        {
            float timer = _chromaticFXTimer;
            Time.timeScale = 0.5f;
            while (timer > 0)
            {
                timer -= Time.deltaTime;
                yield return null;
            }
            Time.timeScale = 1f;
        }
    }
}
