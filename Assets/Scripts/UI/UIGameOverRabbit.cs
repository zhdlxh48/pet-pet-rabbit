using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit
{
    public class UIGameOverRabbit : MonoBehaviour
    {
        private static readonly WaitForSeconds s_fadeTime = new WaitForSeconds(0.02f);
        
        public Image rabbitImage;
        public Image[] textImage;
        public Image[] dotImage;

        private readonly Color initColor = new Color(1.0f, 1.0f, 1.0f, 0.0f); // 처음 시작시 초기화 할 컬러

        private void OnEnable()
        {
            InitializeUIImage(); // UI Image들을 초기화 하는 함수
            StartCoroutine(GameOverUICallback()); // GameOver를 대기
        }

        private void InitializeUIImage()
        {
            rabbitImage.color = initColor;
            foreach (var textImg in textImage)
                textImg.color = initColor;

            foreach (var dotImg in dotImage)
                dotImg.color = initColor;
        }

        private IEnumerator GameOverUICallback()
        {
            print("[SYSTEM] GameOver UI 출력");

            rabbitImage.color = Color.white;

            foreach (var textImg in textImage) {
                StartCoroutine(FadeIn(textImg, 1.8f)); // 이미지를 차례대로 FadeIn
                yield return new WaitForSeconds(1.0f); // 지정된 시간동안 작업을 정지
            }

            foreach (var dotImg in dotImage) {
                dotImg.color = Color.white;
                yield return new WaitForSeconds(1.0f);
            }
        }

        private static IEnumerator FadeIn(Graphic image, float speed)
        {
            var reduceCol = new Color(0.0f, 0.0f, 0.0f, 1.0f); // fadeIn 되는 알파값의 크기를 지정

            while (image.color.a < 0.95f) {
                yield return s_fadeTime;
                image.color += reduceCol * speed * Time.deltaTime; // 이미지의 알파값을 증가시킴
            }
        }
    }
}