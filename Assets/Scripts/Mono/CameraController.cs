using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _rotator;
    [SerializeField] private List<float> _angles;
    [SerializeField] private float _rotateDuration = 0.2f;
    
    private int _rotationIndex = 0;

    private void Start()
    {
        if (_angles.Count == 0)
            return;

        _rotator.rotation = Quaternion.AngleAxis(_angles[_rotationIndex], Vector3.up);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _rotationIndex = (_rotationIndex + 1) % _angles.Count;

            DOTween.Kill(_rotator);
            _rotator.DORotate(new Vector3(0f, _angles[_rotationIndex], 0f), _rotateDuration);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rotationIndex = _rotationIndex - 1;
            if (_rotationIndex < 0)
                _rotationIndex = _angles.Count - 1;

            DOTween.Kill(_rotator);
            _rotator.DORotate(new Vector3(0f, _angles[_rotationIndex], 0f), _rotateDuration);
        }
    }
}
