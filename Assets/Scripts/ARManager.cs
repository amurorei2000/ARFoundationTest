using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.EventSystems;

public class ARManager : MonoBehaviour
{
    public GameObject indicator;
    public Text logText;
    public Button[] btn_colors;
    public GameObject myCar;

    ARRaycastManager arm;
    List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
    Color[] colors = new Color[3] { Color.red, Color.blue, Color.white };

    void Start()
    {
        // 레이캐스트 매니저 컴포넌트 캐싱하기
        arm = GetComponent<ARRaycastManager>();

        // 색상 버튼에 색상 교체 함수를 연결
        for (int i = 0; i < 3; i++)
        {
            int num = i;
            btn_colors[i].onClick.AddListener(() => { ChangeColor(num); });
        }
    }

    void Update()
    {
        // 땅 추적 및 인디케이터 표시 함수
        SearchGround();

        if (Input.touchCount > 0 && !EventSystem.current.currentSelectedGameObject)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                // 터치를 하면 인디케이터 위치에 차 모델링을 켠다.
                myCar.SetActive(true);
                myCar.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
            }
        }
    }

    void SearchGround()
    {
        // 화면 중앙 위치 잡기
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // 저장용 리스트
        hitInfos = new List<ARRaycastHit>();

        // 레이를 발사한다.
        if (arm.Raycast(screenCenter, hitInfos))
        {
            // 메인 카메라의 수평 회전 값을 구한다.
            Vector3 dir = Camera.main.transform.forward;
            dir.y = 0;
            Quaternion rot = Quaternion.LookRotation(dir);

            // 부딪힌 대상의 위치와 카메라의 수평 회전 방향으로 인디케이터를 위치시킨다.
            indicator.SetActive(true);
            Pose hitPose = hitInfos[0].pose;
            indicator.transform.SetPositionAndRotation(hitPose.position, rot);

            // 부딪힌 대상의 리스트를 출력한다.
            HitLog();
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    // 부딪힌 대상의 거리를 출력하는 함수
    void HitLog()
    {
        logText.text = "";

        for(int i = 0; i < hitInfos.Count; i++)
        {
            logText.text += i.ToString() + ": " + hitInfos[i].distance + "m\r\n";
        }
    }

    // 색상 교체 함수
    public void ChangeColor(int num)
    {
        Material mat = myCar.transform.Find("Vehicle7Meshes").Find("MainMesh").GetComponent<MeshRenderer>().materials[1];
        mat.color = colors[num];
    }

}
