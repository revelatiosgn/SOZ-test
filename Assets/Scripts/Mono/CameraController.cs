using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _cameraTarget;
    [SerializeField] private List<float> _angles;
    [SerializeField] private float _rotateDuration = 0.2f;
    [SerializeField] private float _moveSpeed = 10f;
    
    private int _rotationIndex = 0;

    private void Start()
    {
        if (_angles.Count == 0)
            return;

        _cameraTarget.rotation = Quaternion.AngleAxis(_angles[_rotationIndex], Vector3.up);
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _rotationIndex = (_rotationIndex + 1) % _angles.Count;

            DOTween.Kill(_cameraTarget);
            _cameraTarget.DORotate(new Vector3(0f, _angles[_rotationIndex], 0f), _rotateDuration);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _rotationIndex = _rotationIndex - 1;
            if (_rotationIndex < 0)
                _rotationIndex = _angles.Count - 1;

            DOTween.Kill(_cameraTarget);
            _cameraTarget.DORotate(new Vector3(0f, _angles[_rotationIndex], 0f), _rotateDuration);
        }

        float moveSpeed = _moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed *= 2f;

        if (Input.GetKey(KeyCode.W))
        {
            _cameraTarget.transform.position += _cameraTarget.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _cameraTarget.transform.position -= _cameraTarget.transform.forward * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _cameraTarget.transform.position -= _cameraTarget.transform.right * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _cameraTarget.transform.position += _cameraTarget.transform.right * moveSpeed * Time.deltaTime;
        }

        if (Input.mousePosition.x == 0)
            _cameraTarget.transform.position -= _cameraTarget.transform.right * moveSpeed * Time.deltaTime;
        else if (Input.mousePosition.x == Screen.width - 1)
            _cameraTarget.transform.position += _cameraTarget.transform.right * moveSpeed * Time.deltaTime;

        if (Input.mousePosition.y == 0)
            _cameraTarget.transform.position -= _cameraTarget.transform.forward * moveSpeed * Time.deltaTime;
        else if (Input.mousePosition.y == Screen.height - 1)
            _cameraTarget.transform.position += _cameraTarget.transform.forward * moveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
