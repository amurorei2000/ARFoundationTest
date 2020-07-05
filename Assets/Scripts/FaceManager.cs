using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class FaceManager : MonoBehaviour
{
    public Text touchText;
    public GameObject[] faceCaps;
    public Button[] fButtons;
    public ARSession session;

    ARFaceManager afm;

    void Start()
    {
        afm = GetComponent<ARFaceManager>();

        // 버튼에 페이스 캡 프리팹을 변경하는 함수를 연결한다.
        for(int i = 0; i < fButtons.Length; i++)
        {
            int num = i;

            fButtons[num].onClick.AddListener(() => { ChangeFaceCap(num); });
        }
    }

    void Update()
    {
        // 만일, 터치된 손가락의 갯수가 0 이상이라면...
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved)
            {
                // 손가락 움직임 값을 출력한다.
                touchText.text = TouchDelta();
            }
            else
            {
                touchText.text = "";
            }
        }
    }

    // 손가락 움직임 값을 벡터로 반환하는 함수
    string TouchDelta()
    {
        Touch touch = Input.GetTouch(0);
        string deltaPos = string.Format("x: {0:f3}, y: {1:f3}", touch.deltaPosition.x, touch.deltaPosition.y);

        return deltaPos;
    }

    void ChangeFaceCap(int num)
    {
        //afm.enabled = false;

        //foreach(ARFace face in afm.trackables)
        //{
        //    face.enabled = false;
        //}
        session.enabled = false;
        afm.facePrefab = faceCaps[num];
        StartCoroutine(ReTracking());
    }

    IEnumerator ReTracking()
    {
        yield return null;
        //afm.enabled = true;
        /*foreach (ARFace face in afm.trackables)
        {
            face.enabled = true;
        }*/
        session.enabled = true;
    }
}
