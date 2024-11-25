using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace GearsAndDreams.GameSystems
{
    public class SoundManager : Singleton<SoundManager>
    {
        [System.Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
            public SoundType soundType;
            [Range(0f, 1f)]
            public float volume = 0.5f;
            public bool loop = false;
            [HideInInspector]
            public AudioSource source;
        }

        public enum SoundType
        {
            BGM,
            SFX,
        }

        private float bgmMasterVolume = 1f;
        private float sfxMasterVolume = 1f;

        private const string BGM_KEY = "BGMVolume";
        private const string SFX_KEY = "SFXVolume";
        private const float FADE_DURATION = 1f;  // 페이드 효과 지속 시간

        [Header("사운드 세팅")]
        public Sound[] sounds;
        private Dictionary<string, Sound> soundDictionary = new Dictionary<string, Sound>();

        private void Start()
        {
            LoadSoundSettings();
            Debug.Log("SoundManager Start - Loading sounds");

            foreach (Sound sound in sounds)
            {
                GameObject soundObject = new GameObject($"Sound_{sound.name}");
                soundObject.transform.SetParent(transform);
                sound.source = soundObject.AddComponent<AudioSource>();

                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.loop = sound.loop;

                UpdateSoundVolume(sound);
                soundDictionary.Add(sound.name, sound);
                Debug.Log($"Added sound: {sound.name}, Type: {sound.soundType}, Volume: {sound.volume}");
            }
        }

        public void Play(string soundName)
        {
            Debug.Log($"Attempting to play sound: {soundName}");
            if (soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                if (sound.clip == null)
                {
                    Debug.LogError($"Audio clip is missing for sound: {soundName}");
                    return;
                }

                float targetVolume = sound.volume * (sound.soundType == SoundType.BGM ? bgmMasterVolume : sfxMasterVolume);
                sound.source.volume = 0f;
                sound.source.Play();
                Debug.Log($"Playing sound: {soundName}, Target Volume: {targetVolume}");

                // Kill any existing tweens on this AudioSource
                DOTween.Kill(sound.source);

                // 페이드 인 효과
                sound.source.DOFade(targetVolume, FADE_DURATION)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => Debug.Log($"Fade in complete for: {soundName}"));
            }
            else
            {
                Debug.LogWarning($"Sound not found in dictionary: {soundName}");
            }
        }

        public void Stop(string soundName)
        {
            if (soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                if (sound.source.isPlaying)
                {
                    // Kill any existing tweens on this AudioSource
                    DOTween.Kill(sound.source);

                    // 페이드 아웃 효과
                    sound.source.DOFade(0f, FADE_DURATION)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                            sound.source.Stop();
                            Debug.Log($"Stopped sound: {soundName}");
                        });
                }
            }
        }

        public void StopAllSound()
        {
            foreach (var sound in soundDictionary.Values)
            {
                sound.source.Stop();
            }
        }

        public void SetVolume(string soundName, float volume)
        {
            if (soundDictionary.TryGetValue(soundName, out Sound sound))
            {
                sound.volume = Mathf.Clamp01(volume);
                sound.source.volume = sound.volume;
            }
        }

        private void UpdateSoundVolume(Sound sound)
        {
            float masterVolume = sound.soundType == SoundType.BGM ? bgmMasterVolume : sfxMasterVolume;
            sound.source.volume = sound.volume * masterVolume;
        }

        private void UpdateAllSoundVolumes()
        {
            foreach (var sound in soundDictionary.Values)
            {
                UpdateSoundVolume(sound);
            }
        }

        public void SetBGMVolume(float volume)
        {
            bgmMasterVolume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat(BGM_KEY, bgmMasterVolume);
            PlayerPrefs.Save();

            // BGM 타입의 모든 사운드 볼륨 업데이트
            foreach (var sound in soundDictionary.Values)
            {
                if (sound.soundType == SoundType.BGM)
                {
                    UpdateSoundVolume(sound);
                }
            }
        }

        public void SetSFXVolume(float volume)
        {
            sfxMasterVolume = Mathf.Clamp01(volume);
            PlayerPrefs.SetFloat(SFX_KEY, sfxMasterVolume);
            PlayerPrefs.Save();

            // SFX 타입의 모든 사운드 볼륨 업데이트
            foreach (var sound in soundDictionary.Values)
            {
                if (sound.soundType == SoundType.SFX)
                {
                    UpdateSoundVolume(sound);
                }
            }
        }

        private void LoadSoundSettings()
        {
            bgmMasterVolume = PlayerPrefs.GetFloat(BGM_KEY, 1f);
            sfxMasterVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);
            UpdateAllSoundVolumes();
        }

        public void StopAllSoundsOfType(SoundType soundType)
        {
            Debug.Log($"Stopping all sounds of type: {soundType}");
            foreach (var sound in soundDictionary.Values)
            {
                if (sound.soundType == soundType && sound.source.isPlaying)
                {
                    // Kill any existing tweens on this AudioSource
                    DOTween.Kill(sound.source);

                    // 페이드 아웃 효과
                    sound.source.DOFade(0f, FADE_DURATION)
                        .SetEase(Ease.Linear)
                        .OnComplete(() => {
                            sound.source.Stop();
                            Debug.Log($"Fade out complete for: {sound.name}");
                        });
                }
            }
        }
    }
}
