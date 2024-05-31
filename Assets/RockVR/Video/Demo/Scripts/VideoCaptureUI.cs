using UnityEngine;
using System.Diagnostics;

namespace RockVR.Video.Demo
{
    public class VideoCaptureUI : MonoBehaviour
    {
        private bool isPlayVideo = false;
        private void Awake()
        {
            Application.runInBackground = true;
            isPlayVideo = false;
        }

        public void StartCapture()
        {
            if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.NOT_START)
            {
                VideoCaptureCtrl.Instance.StartCapture();
            }
        }

        public void StopCapture()
        {
            if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                VideoCaptureCtrl.Instance.StopCapture();
            }
        }

        private void OngUI()
        {
            if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.NOT_START)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Start Capture"))
                {
                    VideoCaptureCtrl.Instance.StartCapture();
                }
            }
            else if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Stop Capture"))
                {
                    VideoCaptureCtrl.Instance.StopCapture();
                }
                if (GUI.Button(new Rect(180, Screen.height - 60, 150, 50), "Pause Capture"))
                {
                    VideoCaptureCtrl.Instance.ToggleCapture();
                }
            }
            else if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.PAUSED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Stop Capture"))
                {
                    VideoCaptureCtrl.Instance.StopCapture();
                }
                if (GUI.Button(new Rect(180, Screen.height - 60, 150, 50), "Continue Capture"))
                {
                    VideoCaptureCtrl.Instance.ToggleCapture();
                }
            }
            else if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.STOPPED)
            {
                if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Processing"))
                {
                    // Waiting processing end.
                }
            }
            else if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.FINISH)
            {
                if (!isPlayVideo)
                {
                    if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "View Video"))
                    {
#if UNITY_5_6_OR_NEWER
                        // Set root folder.
                        isPlayVideo = true;
                        VideoPlayer.instance.SetRootFolder();
                        // Play capture video.
                        VideoPlayer.instance.PlayVideo();
                    }
                }
                else
                {
                    if (GUI.Button(new Rect(10, Screen.height - 60, 150, 50), "Next Video"))
                    {
                        // Turn to next video.
                        VideoPlayer.instance.NextVideo();
                        // Play capture video.
                        VideoPlayer.instance.PlayVideo();
#else
                        // Open video save directory.
                        Process.Start(PathConfig.saveFolder);
#endif
                    }
                }
            }
        }
    }
}