using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using System;
using UnityEngine;

public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;
    public static bool isGripped = false;

    public Transform start;
    public Transform end;
    public GameObject notch;
    private Camera zoomCamera;
    public float zoomInDistance = 1f;
    public float zoomOutDistance = 2f;

    public float pullAmount { get; private set; } = 0.0f;

    private LineRenderer _lineRenderer;
    private IXRSelectInteractor pullingInteractor = null;
    private Vector3 originalCameraPosition;

    // º“∏Æ ¿Áª˝
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _lineRenderer = GetComponent<LineRenderer>();
        _audioSource = GetComponent<AudioSource>();
        zoomCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        originalCameraPosition = zoomCamera.transform.localPosition;
        
    }

    public void SetPullInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactor;
    }

    public void Release()
    {
        PullActionReleased?.Invoke(pullAmount);
        pullingInteractor = null;
        pullAmount = 0f;
        notch.transform.localPosition = new Vector3(0, 0, 0);
        UpdateString();

        PlayReleaseSound();

        // ¡‹æ∆øÙ
        ZoomOut();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                pullAmount = CalculatePull(pullPosition);

                UpdateString();

                // ¡‹¿Œ
                ZoomIn();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection = end.position - start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;
        return Mathf.Clamp(pullValue, 0, 1);
    }

    private void UpdateString()
    {
        Vector3 linePosition = Vector3.right * Mathf.Lerp(start.localPosition.x, end.localPosition.x, pullAmount);
        notch.transform.localPosition = new Vector3(linePosition.x + 0.2f, notch.transform.localPosition.y, notch.transform.localPosition.z);
        _lineRenderer.SetPosition(1, linePosition);
        isGripped = true;
    }

    private void PlayReleaseSound()
    {
        _audioSource.Play();
    }

    private void ZoomIn()
    {
        float distance = Mathf.Lerp(zoomOutDistance, zoomInDistance, pullAmount);
        zoomCamera.transform.localPosition = originalCameraPosition + new Vector3(-distance, 0f, 0f);
    }

    private void ZoomOut()
    {
        zoomCamera.transform.localPosition = originalCameraPosition;
    }

}
