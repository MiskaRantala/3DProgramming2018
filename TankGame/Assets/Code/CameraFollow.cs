using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class CameraFollow : MonoBehaviour, ICameraFollow
    {

        [SerializeField]
        private SetBehind _setBehind;
        [SerializeField]
        private float _distance;
        [SerializeField]
        private float _angle;
        [SerializeField]
        private Transform _target;

        private enum SetBehind
        {
            along_z_axis,
            from_behind
        }

        public void SetDistance(float distance)
        {
            _distance = distance;
        }

        public void SetAngle(float angle)
        {
            _angle = angle;
        }

        public void SetTarget(Transform targetTransform)
        {
            _target = targetTransform;
        }

             
        private Vector3 _oldPos, _oldForward, _Pos, _Forward;

        private Vector3 _cameraPos;


        void LateUpdate()
        {

            _Pos = _target.position;
            _Forward = _target.forward;

            if (_Pos == _oldPos && (_Forward == _oldForward || _setBehind == SetBehind.along_z_axis)) return;

            _cameraPos = _Pos;

            float angle = Mathf.Deg2Rad * (_angle);
            float horizontal = Mathf.Sin(angle) * _distance;
            float y = Mathf.Cos(angle) * _distance;


            if (_setBehind == SetBehind.from_behind)
            {
                Vector3 direction = _Forward;                
                direction.y = 0;                
                direction.Normalize();
                direction = -direction * horizontal;
          
                _cameraPos.x += direction.x;
                _cameraPos.y += y;
                _cameraPos.z += direction.z;
            }
            else
            {
                _cameraPos.y += y;
                _cameraPos.z -= horizontal;               
            }

            gameObject.transform.position = _cameraPos;
            gameObject.transform.LookAt(_target);

            _oldPos = _Pos;
            _oldForward = _Forward;

        }
    }
}
