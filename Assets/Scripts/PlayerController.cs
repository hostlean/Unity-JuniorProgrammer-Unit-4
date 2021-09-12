using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private float moveSpeed;

    [SerializeField]
    private float powerUpStrength;
    
    [SerializeField]
    private float powerUpActiveTime;

    [SerializeField] 
    private GameObject powerUpIndicatorPrefab;




    private GameObject _powerUpIndicator;
    private Rigidbody _rigidbody;
    private RotateCamera _rotateCamera;
    private Transform _focalPoint;
    private float _forwardInput;
    private bool _hasPowerUp;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rotateCamera = FindObjectOfType<RotateCamera>();
        _focalPoint = _rotateCamera.transform;

    }


    private void Update()
    {
        if (_powerUpIndicator)
        {
            _powerUpIndicator.transform.position = transform.position + new Vector3(0, -.55f, 0);
        }
        
        _forwardInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (_forwardInput != 0)
        {
            _rigidbody.AddForce(_focalPoint.forward * moveSpeed * _forwardInput, ForceMode.Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            _hasPowerUp = true;
            _powerUpIndicator =
                Instantiate(powerUpIndicatorPrefab, 
                    transform.position, 
                    powerUpIndicatorPrefab.transform.rotation);
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && _hasPowerUp)
        {
            var dir = collision.contacts[0].point - transform.position;
            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            
            enemyRb.AddForce(dir.normalized * powerUpStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpActiveTime);
        _hasPowerUp = false;
        Destroy(_powerUpIndicator);
    }
}