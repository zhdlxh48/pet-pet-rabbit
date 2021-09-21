using System;
using UnityEngine;

namespace Rabbit
{
    public class GameHealth : GameStatus
    {
        private const float DEFAULT_MAX_HEALTH = 100.0f;

        [ReadOnly] [SerializeField] private float _maxHealth;
        [ReadOnly] [SerializeField] private float _health;

        public float Health => _health;
        public float MaxHealth => _maxHealth;

        public override void Initialize()
        {
            // 0이하면 기본값이 100으로 설정
            SetMaxHealth(DEFAULT_MAX_HEALTH);
            _health = _maxHealth;
        }

        public void SetMaxHealth(float size)
        {
            try {
                if (size <= 0) {
                    _maxHealth = DEFAULT_MAX_HEALTH;
                    throw new System.Exception("Set MaxHealth value is negative or zero. Set to default");
                }
            }
            catch (System.Exception _ex) {
                Debug.LogError(_ex);
                throw;
            }
            _maxHealth = size;
        }
        
        /// <summary>
        /// Health를 감소시키는 함수
        /// </summary>
        /// <param name="point">피해량</param>
        /// <returns>_health가 0 이하인지 판단</returns>
        public void Damage(float point)
        {
            _health = Mathf.Clamp(_health - point, 0.0f, _maxHealth);
        }
        /// <summary>
        /// Health를 감소시키는 함수
        /// </summary>
        /// <param name="type">피해를 입히는 노트 Type</param>
        public void Damage(NoteType type)
        {
            Damage(StageManager.HealthPreset.healthDic[type].damage);
        }

        /// <summary>
        /// health를 회복시키는 함수
        /// </summary>
        /// <param name="point">회복량</param>
        public void Heal(float point)
        {
            if (_health + point > _maxHealth) {
                _health = _maxHealth;
            }
            else {
                _health += point;
            }
        }
        /// <summary>
        /// health를 회복시키는 함수
        /// </summary>
        /// <param name="point">회복을 시키는 노트 Type</param>
        public void Heal(NoteType type)
        {
            Heal(StageManager.HealthPreset.healthDic[type].heal);
        }
    }
}