using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CkyUtils
{
    public class CkyMath : MonoBehaviour
    {
        public static void TurnTo(GameObject _actor, Vector3 _targetPos, float _rotationSpeed)
        {
            Vector3 _direction = _targetPos - _actor.transform.position;
            float _angle = Vector3.Angle(_direction, _actor.transform.forward);
            _direction.y = 0;

            _actor.transform.rotation = Quaternion.Slerp(_actor.transform.rotation,
                                                Quaternion.LookRotation(_direction),
                                                Time.deltaTime * _rotationSpeed);
        }

        public static void TurnToAngle(GameObject _actor, Vector3 _targetAngle, float _rotationSpeed)
        {
            _actor.transform.rotation = Quaternion.Slerp(_actor.transform.rotation,
                                                Quaternion.LookRotation(_targetAngle),
                                                Time.deltaTime * _rotationSpeed);
        }

        public static void RotateToVelocityDirection(GameObject _actor, NavMeshAgent _agent)
        {
            Vector3 agentVelocity = _agent.velocity;
            bool isMoving = agentVelocity != Vector3.zero;

            if (isMoving)
            {
                _actor.transform.rotation = Quaternion.LookRotation(agentVelocity, Vector3.up);
            }
        }

        public static bool IsTargetInRange(GameObject _actor, Transform _targetTr, float _visDist, float _visAngle)
        {
            Vector3 _direction = _targetTr.position - _actor.transform.position;
            float _sqrDir = _direction.sqrMagnitude;

            if (_sqrDir < _visDist * _visDist)
                return true;

            return false;
        }

        public static float DistanceBetween(GameObject _actor, Transform _targetTr)
        {
            float _distance = (_targetTr.position - _actor.transform.position).magnitude;

            return _distance;
        }

        public static bool CanSeeTarget(GameObject _actor, Transform _targetTr, float _visDist, float _visAngle)
        {
            Vector3 direction = _targetTr.position - _actor.transform.position;
            float angle = Vector3.Angle(direction, _targetTr.forward);

            if (direction.magnitude < _visDist && angle < _visAngle)
                return true;

            return false;
        }

        public static bool CanSeeTargetWithRay(Vector3 rayStartPos, Vector3 rayDirection, float rayMaxDistance)
        {
            RaycastHit hit;

            if (Physics.Raycast(rayStartPos, rayDirection, out hit, rayMaxDistance))
            {
                Debug.DrawRay(rayStartPos, rayDirection * hit.distance, Color.blue);
                return true;
            }
            else
            {
                Debug.DrawRay(rayStartPos, rayDirection * rayMaxDistance, Color.red);
                return false;
            }
        }

        public static bool IsArrivedToTheTargetPosition(GameObject _actor, Vector3 _targetPos, float _arriveDist)
        {
            Vector3 direction = _targetPos - _actor.transform.position;
            float _sqrDir = direction.sqrMagnitude;

            if (_sqrDir < _arriveDist * _arriveDist)
            {
                return true;
            }

            return false;
        }
    }
}