using UnityEngine;
using UnityEngine.UI;
using GearsAndDreams.GameSystems;

namespace GearsAndDreams.UI
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;

        private void Start()
        {
            // 현재 볼륨 값으로 슬라이더 초기화
            if (SoundManager.Instance != null)
            {
                bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
                sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

                // 슬라이더 값이 변경될 때 호출될 이벤트 등록
                bgmSlider.onValueChanged.AddListener(OnBGMSliderValueChanged);
                sfxSlider.onValueChanged.AddListener(OnSFXSliderValueChanged);
            }
        }

        private void OnBGMSliderValueChanged(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetBGMVolume(value);
            }
        }

        private void OnSFXSliderValueChanged(float value)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.SetSFXVolume(value);
            }
        }

        private void OnDestroy()
        {
            // 이벤트 리스너 제거
            if (bgmSlider != null)
                bgmSlider.onValueChanged.RemoveListener(OnBGMSliderValueChanged);
            if (sfxSlider != null)
                sfxSlider.onValueChanged.RemoveListener(OnSFXSliderValueChanged);
        }
    }
}
