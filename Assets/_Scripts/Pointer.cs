﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    [SerializeField] float m_DefaultLengh = 5.0f;
    [SerializeField] public VR_InputModule m_InputModule;
    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        PointerEventData data = m_InputModule.GetData();
        float TargetLengh = data.pointerCurrentRaycast.distance == 0 ? m_DefaultLengh : data.pointerCurrentRaycast.distance;

        RaycastHit hit = CreateRaycast(TargetLengh);

        Vector3 endPosition = transform.position + (transform.forward * TargetLengh);

        if(hit.collider != null)
            endPosition = hit.point;


        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRaycast(float lengh)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLengh);
        return hit;
    }
}
