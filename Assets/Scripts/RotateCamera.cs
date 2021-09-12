using System;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
   [SerializeField]
   private float rotateSpeed;

   private bool _cameraCanRotate;

   private float _horizontalInput;

   private void Update()
   {
      
      _horizontalInput = Input.GetAxis("Horizontal");
      _cameraCanRotate = _horizontalInput != 0;
      
   }

   private void FixedUpdate()
   {
      if(_cameraCanRotate)
         transform.Rotate(Vector3.up, _horizontalInput * Time.fixedDeltaTime * rotateSpeed);
   }
}
