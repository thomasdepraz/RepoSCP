using SCP.Ressources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCP.Camera
{
    public enum CameraState
    {
        FOCUSED,
        UNFOCUSED,
        TWEENING
    }
    public class CameraController : MonoBehaviour
    {
        public CameraState camState { get; private set; }
        public CameraProfile profile;
        private Vector3 originPosition;

        private void Awake()
        {
            new Registry().Register<CameraController>(this);
        }

        void Start()
        {
            originPosition = transform.position;
        }

        public void ChangeState(Vector2 target)
        {
            switch (camState)
            {
                case CameraState.FOCUSED:
                    UnfocusTarget();
                    break;
                case CameraState.UNFOCUSED:
                    FocusTarget(target);
                    break;

                default:
                    Debug.LogWarning("Incorrect state");
                    break;
            }
        }

        public void FocusTarget(Vector2 target)
        {
            camState = CameraState.TWEENING;
            Vector3 newTarget = new Vector3(target.x, target.y, profile.focusedZPos);
            TweenCamera(newTarget, 0.5f, ()=> camState = CameraState.FOCUSED);
        }

        public void UnfocusTarget()
        {
            camState = CameraState.TWEENING;
            TweenCamera(originPosition, 0.5f, () => camState = CameraState.UNFOCUSED);
        }

        public void TweenCamera(Vector3 target, float time, Action onEndCallback)
        {
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, target, time).setEaseInOutQuint().setOnComplete(() => { onEndCallback?.Invoke();  });
            //eventually tween fov
        }
    }
}
