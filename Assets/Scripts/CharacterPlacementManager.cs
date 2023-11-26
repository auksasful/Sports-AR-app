using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterPlacementManager : MonoBehaviour
{
    public GameObject placementObject;
    public XROrigin sessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager ARPlaneManager;
    private List<ARRaycastHit> raycastHitList = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                bool collision = raycastManager.Raycast(Input.GetTouch(0).position, raycastHitList,
                    TrackableType.PlaneWithinPolygon);

                if (collision)
                {
                    GameObject gameObject = Instantiate(placementObject);
                    gameObject.transform.position = raycastHitList[0].pose.position;
                    gameObject.transform.rotation = raycastHitList[0].pose.rotation;
                    //Debug.Log("click");
                }

                foreach (var planes in ARPlaneManager.trackables)
                {
                    planes.gameObject.SetActive(false);
                }

                ARPlaneManager.enabled = false;
            }
        }
    }

    public bool IsDropDownPressed()
    {
        if(EventSystem.current.currentSelectedGameObject?.GetComponent<Dropdown>() == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void SwitchExercise(GameObject exerciseObj)
    {
        placementObject = exerciseObj;
    }

}
