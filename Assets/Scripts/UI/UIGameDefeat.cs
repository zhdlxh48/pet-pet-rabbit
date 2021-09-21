using System;
using UnityEngine;

namespace Rabbit
{
    public class UIGameDefeat : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverRabbit;

        private void OnEnable()
        {
            gameOverRabbit.SetActive(true);
        }
    }
}