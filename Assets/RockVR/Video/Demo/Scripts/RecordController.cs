using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RockVR.Video
{
    public class RecordController : MonoBehaviour
    {
        private bool isPlayVideo = false;
        public VideoCaptureCtrl TargetVideoCapture;

        private void Awake()
        {
            Application.runInBackground = true;
            isPlayVideo = false;
        }

        public void StartRecord()
        {
            try
            {
                if (TargetVideoCapture.status == VideoCaptureCtrl.StatusType.NOT_START ||
                    TargetVideoCapture.status == VideoCaptureCtrl.StatusType.STOPPED ||
                    TargetVideoCapture.status == VideoCaptureCtrl.StatusType.FINISH)
                {
                    TargetVideoCapture.StartCapture();
                }
            } catch
            {

            }

            /*if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.NOT_START)
            {
                VideoCaptureCtrl.Instance.StartCapture();
            }
            */
        }

        public void StopRecord()
        {
            try
            {
                if (TargetVideoCapture.status == VideoCaptureCtrl.StatusType.STARTED)
                {
                    TargetVideoCapture.StopCapture();
                }
            } catch
            {

            }

            /*if (VideoCaptureCtrl.Instance.status == VideoCaptureCtrl.StatusType.STARTED)
            {
                VideoCaptureCtrl.Instance.StopCapture();
            }*/
        }
    }
}
