using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementReticle : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private GameObject visual;

    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public bool HasHit { get; private set; }

    private void Update()
    {
        Vector2 screenCentre = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        HasHit = raycastManager.Raycast(screenCentre, hits, TrackableType.PlaneWithinPolygon);

        if (HasHit)
        {
            Pose pose = hits[0].pose;
            transform.SetPositionAndRotation(pose.position, pose.rotation);
        }

        visual.SetActive(HasHit);
    }

    private void OnDisable()
    {
        HasHit = false;
        visual.SetActive(false);
    }
}
