using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class targetviewer : MonoBehaviour
{
    public GameObject _target;
    private Vector2 _rightInput;
    private Vector2 _leftInput;

    [SerializeField]
    private Vector3 _rightRotation;
    [SerializeField]
    private float _speed = 100;

    public bool _rightStick;

    private uint lIndex = 0;
    private uint rIndex = 0;
    private Vector3 _currentTarget;

    public Vector3[] _smoothDeltaL = new Vector3[5];
    public Vector3[] _smoothDeltaR = new Vector3[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*
        Vector3 mouseRotation = new Vector3(0, 0, _rightInput.x);
        mouseRotation.z = Mathf.Clamp(mouseRotation.z, -1f, 1f);

        _rightRotation += mouseRotation * _speed / 40;
        */
        if (!_rightStick)
        {
            Vector3 keyRotation = new Vector3(_leftInput.x, _leftInput.y, 0);

            if (keyRotation.x != 0 || keyRotation.y != 0)
            {
                keyRotation.Normalize();

                _smoothDeltaL[lIndex % 5] = new Vector3(0f, 0f, Mathf.Atan2(keyRotation.y, keyRotation.x) * Mathf.Rad2Deg);
                lIndex++;

                _currentTarget = _smoothDeltaL[0];

                for (int i = 1; i < 5; i++)
                {
                    _currentTarget += _smoothDeltaL[i];
                }

                _currentTarget = new Vector3(0f, 0f, _currentTarget.z / 5);

                //+90 for the offset of upwards controls
                transform.rotation = Quaternion.Euler(0f, 0f, _currentTarget.z - 90);

                transform.position = _target.transform.position + transform.up * 3.5f;
            }
        }
        else
        {
            Vector3 mouseRotation = new Vector3(_rightInput.x, _rightInput.y, 0);

            if (mouseRotation.x != 0 || mouseRotation.y != 0)
            {
                mouseRotation.Normalize();

                _smoothDeltaR[rIndex % 5] = new Vector3(0f, 0f, Mathf.Atan2(mouseRotation.y, mouseRotation.x) * Mathf.Rad2Deg);
                rIndex++;

                _currentTarget = _smoothDeltaR[0];

                for (int i = 1; i < 5; i++)
                {
                    _currentTarget += _smoothDeltaR[i];
                }

                _currentTarget = new Vector3(0f, 0f, _currentTarget.z / 5);

                //+90 for the offset of upwards controls
                transform.rotation = Quaternion.Euler(0f, 0f, _currentTarget.z - 90);

                transform.position = _target.transform.position + transform.up * 3.5f;
                /*
                if (mouseRotation.x != 0 && mouseRotation.y != 0)
                {
                    mouseRotation.Normalize();
                    _rightRotation = new Vector3(0, 0, (_rightInput.x + _rightInput.y) * 180);

                    //_rightRotation += mouseRotation * _speed / 40;
                }


                Debug.Log(_rightRotation);

                transform.rotation = Quaternion.Euler(-_rightRotation);
                transform.position = _target.transform.position + transform.up * 3.5f;
                */
            }
        }
        
    }

    public void OnMoveRight(InputAction.CallbackContext ctx) => _rightInput = ctx.ReadValue<Vector2>();
    public void OnMoveLeft(InputAction.CallbackContext ctx) => _leftInput = ctx.ReadValue<Vector2>();
}
