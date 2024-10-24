using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public static CameraScroll Instance { get; private set; }

    [Header("Component")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform _camera;
    [Space]
    [SerializeField] private Transform positionA;
    [SerializeField] private Transform positionB;

    [Header("Settings")]
    [SerializeField, Range(0f,1f)] private float startingCameraPercentage;
    [Space]
    [SerializeField] private float scrollSensitivity;
    [SerializeField, Range(0.01f, 100f)] private float smoothInputFactor;
    [SerializeField, Range(0.01f, 100f)] private float smoothScrollFactor;
    [SerializeField] private bool invertScroll;

    private float ScrollInput => inputManager.GetScroll();

    private float smoothInput;
    private float totalDistance;
    private float desiredDistance;
    private float currentDistance;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {
        InitializeSettings();
    }

    private void Update()
    {
        HandlePlayerCameraScroll();
    }

    private void LateUpdate()
    {
        ApplyDistance();
    }

    private void SetSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is more than one CameraScroll instance, proceding to destroy duplicate");
            Destroy(gameObject);
        }
    }

    private void InitializeSettings()
    {
        totalDistance = Vector3.Distance(positionA.position, positionB.position);
        desiredDistance = totalDistance * startingCameraPercentage;
        currentDistance = desiredDistance;

        SetCameraPosition(currentDistance);
    }

    private void HandlePlayerCameraScroll()
    {
        SmoothInput();
        CalculateDesiredDistance();
        SmoothDistance();
    }


    private void SmoothInput() => smoothInput = Mathf.Lerp(smoothInput, ScrollInput, smoothInputFactor * Time.deltaTime);

    private void CalculateDesiredDistance()
    {
        float processedScrollInput = invertScroll ? smoothInput : -smoothInput;

        desiredDistance = Mathf.Clamp(desiredDistance - scrollSensitivity * processedScrollInput, 0f, totalDistance);
    }

    private void SmoothDistance() => currentDistance = Mathf.Lerp(currentDistance, desiredDistance, smoothScrollFactor * Time.deltaTime);

    private void ApplyDistance()
    {
        SetCameraPosition(currentDistance);
    }

    private void SetCameraPosition(float distance)
    {
        Vector3 position = positionA.position + (positionB.position - positionA.position) * distance/totalDistance;
        _camera.position = position;
    }

    private void SetCameraPositionByPercentage(float percentage)
    {
        Vector3 position = Vector3.Lerp(positionA.position, positionB.position, percentage);
        _camera.position = position;
    }

    private void SetCameraPosition(Vector3 position) => _camera.position = position;
}
