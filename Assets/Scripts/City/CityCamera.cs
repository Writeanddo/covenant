using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CityCamera : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float _xTransitionPos;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float maxSpeed = 9.5f;
    [SerializeField] float _chromaticFXSpeed;

    private float xVelocity = 0f;
    private float yVelocity = 0f;
    public float Duration { get; set; }

    private PostProcessVolume _ppVolume;
    private ChromaticAberration _chromaticAberration;

    private void Awake()
    {
        _ppVolume = FindObjectOfType<PostProcessVolume>();
        _ppVolume.profile.TryGetSettings(out _chromaticAberration);
    }

    private void Start()
    {
        Duration = 1f;
        ChromaticPulse();
        Vector2 newPos = new Vector2(player.transform.position.x - _xTransitionPos, transform.position.y);
        transform.position = newPos;
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float newXpos = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref xVelocity, smoothTime, maxSpeed);
        float newYpos = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref yVelocity, smoothTime, maxSpeed);
        transform.position = new Vector3(newXpos, newYpos, -11f);
    }

    //REFATORAR!!!
    public void ChromaticPulse()
    {
        StartCoroutine(Co_ChromaticPulse());
    }

    private IEnumerator Co_ChromaticPulse()
    {
        float timer = 0;
        float initialIntensity = _chromaticAberration.intensity.value;
        _chromaticAberration.intensity.value = 1f;
        while (timer < Duration)
        {
            timer += Time.deltaTime;
            if (_chromaticAberration.intensity.value > initialIntensity)
                _chromaticAberration.intensity.value -= _chromaticFXSpeed;
            yield return null;
        }
    }

}
