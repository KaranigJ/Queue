using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastButton : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Camera _camera;
    private Transform _transform;
    
    
    private void Awake()
    {
        _camera = Camera.main;
        _transform = transform;
    }

    public bool CheckIfClicked(Transform target)
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Raycast depends on camera projection mode
            var origin = Vector2.zero;
            var direction = Vector2.zero;
            var distance = Vector3.Distance(_camera.transform.position, _transform.position);
            
            origin = _camera.ScreenToWorldPoint(Input.mousePosition);

            
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, _layerMask);
            //RaycastHit2D hit = Physics2D.Raycast(origin, dir);

            //Check if we hit anything
            if (hit.transform == target)
            {
                return true;
                //Debug.Log("We hit " + hit.collider.name);
            }
        }
        
        return false;
    }
}
