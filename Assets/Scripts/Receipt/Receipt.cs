using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections;
using GearsAndDreams.GameSystems;
using static GearsAndDreams.GameSystems.GameManager;

namespace GearsAndDreams.Receipt
{
    public class Receipt : MonoBehaviour
    {
        private RectTransform panelRectTransform;
        [SerializeField] private float animationDuration = 1f;
        [SerializeField] private float startYPosition = 1000f;
        [SerializeField] private Ease easeType = Ease.OutBack;

        [Header("점수 텍스트 References")]
        [SerializeField] private TextMeshProUGUI materialSelectionScoreText;
        [SerializeField] private TextMeshProUGUI castingScoreText;
        [SerializeField] private TextMeshProUGUI polishingScoreText;
        [SerializeField] private TextMeshProUGUI cuttingScoreText;
        [SerializeField] private TextMeshProUGUI totalScoreText;

        [Header("애니메이션 설정")]
        [SerializeField] private float scoreDisplayDelay = 0.5f;
        [SerializeField] private float scoreAnimationDuration = 0.5f;

        private void Start()
        {
            InitializePanelPosition();
            StartCoroutine(ShowScoresSequentially());
        }

        private void InitializePanelPosition()
        {
            if (panelRectTransform == null)
            {
                panelRectTransform = GetComponent<RectTransform>();
            }

            Vector2 position = panelRectTransform.anchoredPosition;
            position.y = startYPosition;
            panelRectTransform.anchoredPosition = position;
        }

        private IEnumerator ShowScoresSequentially()
        {
            // 패널 내려오는 애니메이션
            panelRectTransform.DOAnchorPosY(0f, animationDuration)
                .SetEase(easeType);

            // 패널 애니메이션이 완료될 때까지 대기
            yield return new WaitForSeconds(animationDuration);

            // 각 게임의 점수를 가져옴
            int materialScore = GameManager.Instance.GetGameScore(GameType.MaterialSelection);
            int castingScore = GameManager.Instance.GetGameScore(GameType.Casting);
            int polishingScore = GameManager.Instance.GetGameScore(GameType.Polishing);
            int cuttingScore = GameManager.Instance.GetGameScore(GameType.Cutting);
            int totalScore = materialScore + castingScore + polishingScore + cuttingScore;

            // 초기화
            materialSelectionScoreText.text = "재료 : ";
            castingScoreText.text = "주조 : ";
            polishingScoreText.text = "연마 : ";
            cuttingScoreText.text = "절삭 : ";
            totalScoreText.text = "보수 : ";

            // Material Selection 점수 표시
            yield return new WaitForSeconds(scoreDisplayDelay);
            yield return AnimateScore(materialSelectionScoreText, materialScore, "재료");

            // Casting 점수 표시
            yield return new WaitForSeconds(scoreDisplayDelay);
            yield return AnimateScore(castingScoreText, castingScore, "주조");

            // Polishing 점수 표시
            yield return new WaitForSeconds(scoreDisplayDelay);
            yield return AnimateScore(polishingScoreText, polishingScore, "연마");

            // Cutting 점수 표시
            yield return new WaitForSeconds(scoreDisplayDelay);
            yield return AnimateScore(cuttingScoreText, cuttingScore, "절삭");

            // Total 점수 표시
            yield return new WaitForSeconds(scoreDisplayDelay);
            yield return AnimateScore(totalScoreText, totalScore, "보수");
        }

        private IEnumerator AnimateScore(TextMeshProUGUI textComponent, int targetScore, string gameType)
        {
            float elapsedTime = 0;
            int startScore = 0;

            while (elapsedTime < scoreAnimationDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / scoreAnimationDuration;
                int currentScore = (int)Mathf.Lerp(startScore, targetScore, progress);
                textComponent.text = $"{gameType} : {currentScore}";
                yield return null;
            }

            textComponent.text = $"{gameType} : {targetScore}";
        }
    }
}
