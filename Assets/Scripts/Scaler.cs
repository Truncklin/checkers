using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scaler : MonoBehaviour
{
    [SerializeField]private GameObject _objectRotate;
    [SerializeField]private GameObject Debug;
    
    private float     _baseScale;
    private float     _baseDistance;
    private bool      _isRotating; 
    private Camera    _camera;
    private Transform _objectForRotate;
    private TextMeshPro tmp;
    private Vector2   _touch;
    
    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        tmp = Debug.GetComponent<TextMeshPro>();
         _objectForRotate = _objectRotate.gameObject.GetComponentInParent<Transform>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(RotateL());
        }*/
        HandleScaling();
        HandleSwiping();
    }

    private void HandleScaling()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            switch (touch2.phase)
            {
                case TouchPhase.Began:
                    _baseScale = _camera.fieldOfView;
                    _baseDistance = Vector2.Distance(touch1.position, touch2.position);
                    break;
                case TouchPhase.Moved:
                    float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                    if (_baseDistance > 80)
                    {
                        float rate = _baseDistance / currentDistance;
                        float scale = _baseScale * rate;
                        _camera.fieldOfView = scale;
                    }
                    break;
            }
        }
    }

    private void HandleSwiping()
    {
        if (Input.touchCount == 1)
        {
            Touch touch1 = Input.GetTouch(0);

            switch (touch1.phase)
            {
                case TouchPhase.Began:
                    _touch = touch1.position;
                    
                    break;
                case TouchPhase.Ended:
                    Vector2 count = _touch - touch1.position;
                    if (!_isRotating &&  count.magnitude > 10 )
                    {
                        StartCoroutine(_touch.x - touch1.position.x < 0 ? Rotate(1) : Rotate(-1));
                    }
                    break;
            }
        }
    }
    
    IEnumerator Rotate(int index)
    {
        _isRotating = true;
        float duration = 1f;
        Quaternion startRotation = _objectForRotate.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, startRotation.eulerAngles.y + 90*index, 0);

        float timeElapsed = 0f;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapsed / duration);
            _objectForRotate.rotation = Quaternion.Slerp(startRotation, targetRotation, t);
            yield return null;
        }
        _isRotating = false;
    }
    
}
