using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : ChrisCustomBehaviour {
    public static Camera UICam;
    public static Camera OldSchoolCam;
    public static Camera ThreeDCam;
    public static Camera DiagonalCam;
    public static Light GlobalLight;

    private GameLogic ref_GameLogic;

    public enum CameraMode
    {
        DIAGONAL,
        THREEDIMENSIONAL,
        OLDSCHOOL
    }
    static CameraMode _cameraMode = CameraMode.DIAGONAL;
    public static CameraMode CAMERA_MODE
    {
        get { return _cameraMode; }
        set { _cameraMode = value; }
    }
    [SerializeField]
    private CameraMode editor_cameraMode;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        InitCameras();
        InitLighting();

        ref_GameLogic = FindObjectOfType<GameLogic>();
        ref_GameLogic.Init();
    }
    public static void AssignCameraTargets(GameObject player)
    {
        DiagonalCam.GetComponent<CameraController>().AssignCamera(player);
        ThreeDCam.GetComponent<CameraController>().AssignCamera(player);
        OldSchoolCam.GetComponent<CameraController>().AssignCamera(player);
    }
    private void InitCameras() {
        _cameraMode = editor_cameraMode;
        Camera[] cams = FindObjectsOfType<Camera>();
        for (int i = 0; i < cams.Length; i++)
        {
            switch (cams[i].name)
            {
                case "Camera_UI":
                    {
                        UICam = cams[i];
                        print("UICam Initialized");
                    }
                    break;
                case "Camera_Diagonal":
                    {
                        DiagonalCam = cams[i];
                        DiagonalCam.gameObject.SetActive(_cameraMode == CameraMode.DIAGONAL);
                        print("DiagonalCam Initialized - Is Active: " + DiagonalCam.gameObject.activeInHierarchy);
                    }
                    break;
                case "Camera_3D":
                    {
                        ThreeDCam = cams[i];
                        ThreeDCam.gameObject.SetActive(_cameraMode == CameraMode.THREEDIMENSIONAL);
                        print("ThreeDCam Initialized - Is Active: " + ThreeDCam.gameObject.activeInHierarchy);
                    }
                    break;
                case "Camera_OldSchool":
                    {
                        OldSchoolCam = cams[i];
                        OldSchoolCam.gameObject.SetActive(_cameraMode == CameraMode.OLDSCHOOL);
                        print("OldSchoolCam Initialized - Is Active: " + OldSchoolCam.gameObject.activeInHierarchy);
                    }
                    break;
            }
        }
    }
    private void InitLighting()
    {
        GlobalLight = FindObjectOfType<LightController>().GetComponent<Light>();
        print("GlobalLight Initialized - Is Active: " + GlobalLight.gameObject.activeInHierarchy);
    }


}
