using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform carTransform;
    [Range(1, 10)]
    public float followSpeed = 2;
    [Range(1, 10)]
    public float lookSpeed = 5;

    private Vector3 localOffset;

    void Start() {
        // Store the initial offset in car's local space
        localOffset = Quaternion.Inverse(carTransform.rotation) * (transform.position - carTransform.position);
    }

    void FixedUpdate() {
        // Move camera to follow car with rotated offset
        Vector3 desiredPosition = carTransform.TransformPoint(localOffset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Get the car's Y rotation
        float targetYRotation = carTransform.eulerAngles.y;

        // Create a rotation with fixed X angle (e.g., 40°), car's Y, and 0 Z
        Quaternion targetRotation = Quaternion.Euler(10f, targetYRotation, 0f);

        // Smoothly rotate to the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
    }
}
