using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

[System.Serializable]
public class MarkerPrefabs
{
    public string markerName;
    public GameObject targetPrefab;
}

public class ImageRecognitionMulti : MonoBehaviour
{
    /* Insepctor array */
    public MarkerPrefabs[] markerPrefabCombos;
    ARTrackedImageManager m_TrackedImageManager;
    
    void Awake()
    {
     
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        for(int i = 0; i < eventArgs.updated.Count; i++)
        {            
            var trackedImage = eventArgs.updated[i];
            /* If an image is properly tracked */
            // if (trackedImage.trackingState == TrackingState.Tracking || trackedImage.trackingState == TrackingState.Limited)
            Debug.Log("state: " + trackedImage.trackingState.ToString() + "name: " + trackedImage.referenceImage.name);


            for (int j = 0; j < markerPrefabCombos.Length; j++)
            {
                /* If trackedImage matches an image in the array */
                if (markerPrefabCombos[j].markerName == trackedImage.referenceImage.name)
                {
                    /* Set the corresponding prefab to active at the center of the tracked image */
                    if (trackedImage.trackingState == TrackingState.Tracking)
                    {
                        markerPrefabCombos[j].targetPrefab.SetActive(true);
                        markerPrefabCombos[j].targetPrefab.transform.position = trackedImage.transform.position;
                        markerPrefabCombos[j].targetPrefab.transform.up = trackedImage.transform.up;
                    }
                    else
                    {
                        markerPrefabCombos[j].targetPrefab.SetActive(false);
                    }
                }
            }
        }
    }
}
