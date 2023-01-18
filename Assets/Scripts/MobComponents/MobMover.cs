using PlayerComponents;
using UnityEngine;
using Utils;

namespace MobComponents
{
    [RequireComponent(typeof(MobAnimator))]
    public class MobMover : MonoBehaviour, IMobComponent
    {
        [SerializeField] private float _sightDistance = 5f;
        [SerializeField] private float _moveSpeed;

        [HideInInspector]
        public bool Active = true;

        private MobAnimator _mobAnimator;
        private Vector3 _targetPosition = Vector3.zero;

        private void Awake()
        {
            _mobAnimator = GetComponent<MobAnimator>();
            PickRandomPosition();
            EventBus.Sub(OnDeath, EventBus.PLAYER_DEATH);
        }

        private void OnDestroy()
        {
            EventBus.Unsub(OnDeath, EventBus.PLAYER_DEATH);
        }

        private void Update()
        {
            if (Active)
            {
                var playerDistance = (transform.position - Player.Instance.transform.position).Flat().magnitude;
                var targetDistance = (transform.position - _targetPosition).Flat().magnitude;
                if (_sightDistance >= playerDistance)
                {
                    _targetPosition = Player.Instance.transform.position;
                }
                else if (targetDistance < 0.2f)
                {
                    PickRandomPosition();
                }

                var direction = (_targetPosition - transform.position).Flat().normalized;

                transform.SetPositionAndRotation(transform.position + direction * Time.deltaTime * _moveSpeed,
                    Quaternion.LookRotation(direction, Vector3.up));
            }

            _mobAnimator.SetIsRun(Active);
        }


        private void PickRandomPosition()
        {
            _targetPosition.x = Random.value * 11 - 6;
            _targetPosition.z = Random.value * 11 - 6;
        }

        public void OnSpawn()
        {
            enabled = true;
            Active = true;
        }

        public void OnDeath()
        {
            enabled = false;
        }
    }
}