using UnityEngine;

namespace Rabbit
{
    // TODO: Debug
    public class LoopObject : MonoBehaviour
    {
        [Header("Looping 하는 Object")]
        [SerializeField] private GameObject loopObject;

        [Header("스크린의 가로 길이")]
        [ReadOnly] [SerializeField] private float screenWidthRatio;
        
        private int excuteTime;
        private Coroutine loopRoutine;

        private void Awake()
        {
            screenWidthRatio = Camera.main.orthographicSize * ((float) Screen.width / Screen.height) * 2.0f;
        }

        private void CheckOneLoopDone()
        {
            if (excuteTime >= screenWidthRatio) {
                excuteTime = 0;
                loopObject.transform.localPosition = Vector3.zero;
            }
        }

        public void MoveLeftLoop()
        {
            loopObject.transform.Translate(Vector3.left, Space.Self);
            excuteTime++;
            CheckOneLoopDone();
        }
    }
}