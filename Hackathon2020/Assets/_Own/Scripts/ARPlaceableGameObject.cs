using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlaceableGameObject : MonoBehaviour
{
    [Title("Events",null,TitleAlignments.Centered,true)]
    public Event OnPlacementPoseFound;
    public Event OnPlacementPoseLost;
    public Event OnObjectPlaced;
    
    
    [Title("References",null,TitleAlignments.Centered,true)]
    [SerializeField] private Camera _camera;
    [SerializeField] protected ARRaycastManager _arRaycastManager;
    [SerializeField] protected ARPlaneManager _arPlaneManager;
    
    [ShowIf("useIndicator",true,true)]
    [SerializeField] protected GameObject _indicator;

    [Title("Options", null, TitleAlignments.Centered, true)]
    [SerializeField] protected TrackableType _trackingType = TrackableType.PlaneWithinPolygon;
    [SerializeField] private bool _placeOnClick = true;
    public bool useIndicator = true;
    
    [Title("Debug",null,TitleAlignments.Centered,true)]
    [ReadOnly]
    [SerializeField]
    private bool _isPlaced;

    
    protected Pose _placementPose;
    
    protected List<ARRaycastHit> _hits;

    private bool _placementPoseIsValid = false;

    public bool placementPoseIsValid => _placementPoseIsValid;

    public Pose placementPose => _placementPose;

    public bool isPlaced => _isPlaced;

    public ARPlaneManager arPlaneManager => _arPlaneManager;

    // Start is called before the first frame update
    void Start()
    {
        _hits = new List<ARRaycastHit>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlaced) return;
        
        updatePositionPose();
        toggleIndicator();
        
        if (_placeOnClick && Input.touchCount > 0 && placementPoseIsValid)
        {
            Place();
        } 
    }

    private void updatePositionPose()
    {
        Vector2 screenCenter = _camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        _hits.Clear();
        _arRaycastManager.Raycast(screenCenter, _hits, _trackingType);
        //Debug.Log(_hits.Count);

        _placementPoseIsValid = _hits.Count > 0;
        //Debug.Log(_placementPoseIsValid);

        if (_placementPoseIsValid)
        {
            _placementPose = _hits[0].pose;
            
            Vector3 cameraForward = _camera.transform.forward;
            Vector3 cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            
            //_indicator.SetActive(true);
        }
    }


    private void toggleIndicator()
    {
        if(!useIndicator) return;
        
        if (placementPoseIsValid)
        {
            if (_indicator.activeSelf == false)
            {
                OnPlacementPoseFound.Raise();
                _indicator.SetActive(true);
            }
            _indicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            if (_indicator.activeSelf == true)
            {
                OnPlacementPoseLost.Raise();
                _indicator.SetActive(false);
            }
        }
    }


    public void Place()
    {
        if(!_isPlaced) OnObjectPlaced.Raise();

        gameObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        _indicator.SetActive(false);
        _isPlaced = true;
    }
}
