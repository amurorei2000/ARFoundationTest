using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    public MarkerList[] markers;
    public Text[] markerText;

    ARTrackedImageManager imageManager;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        imageManager.trackedImagesChanged += OnImageChanged;
    }

    void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        List<ARTrackedImage> trackedImages_added = args.added;
        List<ARTrackedImage> trackedImages_updated = args.updated;
        List<ARTrackedImage> trackedImages_removed = args.removed;

        // 이미지 추적 정보 확인용 텍스트 출력하기
        ShowText(trackedImages_added, trackedImages_updated, trackedImages_removed);

        // 추적된 이미지에 대응하는 게임 오브젝트 활성화하기
        for(int i = 0; i < trackedImages_updated.Count; i++)
        {
            for(int j = 0; j < markers.Length; j++)
            {
                // 만일, 추적할 이미지의 이름과 미리 지정한 텍스쳐별 오브젝트 이름이 같다면...
                if(trackedImages_updated[i].referenceImage.name == markers[j].imageName)
                {
                    // 현재 이미지 추적 상태라면...
                    if(trackedImages_updated[i].trackingState == UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                    {
                        //GameObject go = Instantiate(markers[j].markerOject);
                        markers[j].markerOject.SetActive(true);
                        markers[j].markerOject.transform.position = trackedImages_updated[i].transform.position;
                        markers[j].markerOject.transform.up = trackedImages_updated[i].transform.forward;
                    }
                    else
                    {
                        markers[j].markerOject.SetActive(false);
                    }
                }
            }
        }
    }

    // 추적 중인 이미지 확인용
    void ShowText(List<ARTrackedImage> added, List<ARTrackedImage> updated, List<ARTrackedImage> removed)
    {
        // 텍스트 비우기
        foreach(Text ma in markerText)
        {
            ma.text = "";
        }
        
        // 추가된 이미지 확인
        for (int i = 0; i < added.Count; i++)
        {
            markerText[0].text += added[i].referenceImage.name + "\r\n";
        }

        // 갱신된 이미지 확인
        for (int i = 0; i < updated.Count; i++)
        {
            markerText[1].text += updated[i].referenceImage.name + "\r\n";
            markerText[1].text += updated[i].trackableId + "\r\n";
        }

        // 추가된 이미지 확인
        for (int i = 0; i < removed.Count; i++)
        {
            markerText[2].text += removed[i].referenceImage.name + "\r\n";
        }
    }

    private void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnImageChanged;
    }
}

// 마커 등록용 클래스
[System.Serializable]
public class MarkerList
{
    public string imageName;
    public GameObject markerOject;
}
