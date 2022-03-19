using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldController : MonoBehaviour
{
    public GameObject _target;

    [SerializeField]
    private bool _rightStick;
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private Vector3 _leftRotation;
    [SerializeField]
    private Vector3 _rightRotation;

    private Vector2 _leftInput;
    private Vector2 _rightInput;
    private Vector3 _currentTarget;

    private uint lIndex = 0;
    private uint rIndex = 0;

    public Vector3[] _smoothDeltaL = new Vector3[5];
    public Vector3[] _smoothDeltaR = new Vector3[5];

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //allow the player to release their mouse with escape, and relock it with click
        if (Cursor.lockState.Equals(CursorLockMode.None) || Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void FixedUpdate()
    {
        //naming conventions used are for for keyboard bindings
        if (!_rightStick) //diffferent inputs for the 2 circles
        {
            Vector3 keyRotation = new Vector3(_leftInput.x, _leftInput.y, 0);

            //only change the position to rotate to if we have new input
            if (keyRotation.x != 0 || keyRotation.y != 0)
            {
                _currentTarget = new Vector3(0f, 0f, Mathf.Atan2(keyRotation.y, keyRotation.x) * Mathf.Rad2Deg - 90);

                _smoothDeltaL[lIndex % 5] = new Vector3(0f, 0f, Mathf.Atan2(keyRotation.y, keyRotation.x) * Mathf.Rad2Deg - 90);
                lIndex++;

                _currentTarget = _smoothDeltaL[0];

                for (int i = 1; i < 5; i++)
                {
                    _currentTarget += _smoothDeltaL[i];
                }

                _currentTarget = new Vector3(0f, 0f, _currentTarget.z / 5);
            }

            keyRotation.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.Euler(_currentTarget),
                _speed);

            transform.position = _target.transform.position + transform.up * 2.5f;
        }
        else
        {
            // uncomment for x-only movement
            /*
            Vector3 mouseRotation = new Vector3(0, 0, _rightInput.x);
            mouseRotation.z = Mathf.Clamp(mouseRotation.z, -1f, 1f); 

            _rightRotation += mouseRotation * _speed/40;
            */
            Vector3 mouseRotation = new Vector3(_rightInput.x, _rightInput.y, 0);

            //only change the position to rotate to if we have new input
            if (mouseRotation.x != 0 || mouseRotation.y != 0)
            {
                _currentTarget = new Vector3(0f, 0f, Mathf.Atan2(mouseRotation.y, mouseRotation.x) * Mathf.Rad2Deg - 90);

                _smoothDeltaR[lIndex % 5] = new Vector3(0f, 0f, Mathf.Atan2(mouseRotation.y, mouseRotation.x) * Mathf.Rad2Deg - 90);
                lIndex++;

                _currentTarget = _smoothDeltaR[0];

                for (int i = 1; i < 5; i++)
                {
                    _currentTarget += _smoothDeltaR[i];
                }

                _currentTarget = new Vector3(0f, 0f, _currentTarget.z / 5);
            }

            mouseRotation.Normalize();
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.Euler(_currentTarget),
                _speed);

            transform.position = _target.transform.position + transform.up * 2.5f;
        }
    }

    public void OnMoveLeft(InputAction.CallbackContext ctx) => _leftInput = ctx.ReadValue<Vector2>();
    public void OnMoveRight(InputAction.CallbackContext ctx) => _rightInput = ctx.ReadValue<Vector2>();
}
