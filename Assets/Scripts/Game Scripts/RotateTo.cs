using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class RotateTo: MonoBehaviour
 {
    //values that will be set in the Inspector
    RectTransform Target;
    public float RotationSpeed;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    
    void Start(){Target = GameObject.Find("RotatePoint").GetComponent<RectTransform>();}
    void Update()
    {
        
        //find the vector pointing from our position to the target
        _direction = (Target.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }
 }