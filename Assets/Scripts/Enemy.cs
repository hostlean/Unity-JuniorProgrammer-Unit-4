using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody _rigidbody;
    private GameObject _player;
    
    public SpawnManager SpawnManager { get; set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = FindObjectOfType<PlayerController>().gameObject;
        
        
    }

    private void Update()
    {
        if(transform.position.y < -10)
        {
            SpawnManager.Enemies.Remove(this);
            Destroy(gameObject);
        }
       
    }

    private void FixedUpdate()
    {
        Vector3 lookDir = _player.transform.position - transform.position;
        
        _rigidbody.AddForce(lookDir.normalized * speed, ForceMode.Force);
    }
}