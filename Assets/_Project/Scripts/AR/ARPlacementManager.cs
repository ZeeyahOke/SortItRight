using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private Transform reticle;

    private static readonly List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool hasHit;
    private Pose hitPose;

    public bool HasHit => hasHit;
    public Pose HitPose => hitPose;

    private void Update()
    {
        UpdateReticle();
    }

    private void UpdateReticle()
    {
        Vector2 screenCentre = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        hasHit = raycastManager.Raycast(screenCentre, hits, TrackableType.PlaneWithinPolygon);

        if (hasHit)
        {
            hitPose = hits[0].pose;
            reticle.SetPositionAndRotation(hitPose.position, hitPose.rotation);
        }

        reticle.gameObject.SetActive(hasHit);
    }
}
