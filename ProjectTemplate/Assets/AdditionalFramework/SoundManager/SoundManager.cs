using UnityEngine;
using Com.Nravo.Framework;
using System.Runtime.InteropServices;

namespace Com.Nravo.Framework
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [Range(0.0f, 1.0f)] public float soundVolume;
        [Range(0.0f, 1.0f)] public float musicVolume;

        public AudioClip backgroundMusic;
        private AudioSource _backgroundMusicSource;

        private bool _soundEnabled;
        private bool _musicEnabled;

        public bool IsSoundEnabled
        {
            get { return _soundEnabled; }
        }

        public bool IsMusicEnabled
        {
            get { return _musicEnabled; }
        }

        #region init

        public override void Init()
        {
            _soundEnabled = true;
            _musicEnabled = true;
            CreateBackgroundMusicSource();
        }

        private void CreateBackgroundMusicSource()
        {
            GameObject go = new GameObject("Background Music");
            go.transform.parent = Camera.main.transform;

            _backgroundMusicSource = go.AddComponent<AudioSource>();
            _backgroundMusicSource.clip = backgroundMusic;
            _backgroundMusicSource.loop = true;
            _backgroundMusicSource.volume = musicVolume;
        }

        #endregion

        #region toggles
        public void ToggleSound()
        {
            _soundEnabled = !_soundEnabled;
        }

        public void ToggleMusic()
        {
            if (_musicEnabled && _backgroundMusicSource.isPlaying)
            {
                _backgroundMusicSource.Pause();
                _musicEnabled = false;
                return;
            }

            _backgroundMusicSource.Play();
            _musicEnabled = true;
        }

        #endregion

        #region sound

        public AudioSource Play(AudioClip clip)
        {
            return Play(clip, Camera.main.transform, soundVolume, 1.0f);
        }

        public AudioSource Play(AudioClip clip, Transform emitter)
        {
            return Play(clip, emitter, soundVolume, 1.0f);
        }

        public AudioSource Play(AudioClip clip, Transform emitter, float volume)
        {
            return Play(clip, emitter, volume, 1.0f);
        }

        public AudioSource Play(AudioClip clip, Transform emitter, float volume, float pitch)
        {
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = emitter.position;
            go.transform.parent = emitter;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            if (_soundEnabled) { source.Play(); }
            Destroy(go, clip.length);
            return source;
        }
            
        // Play at point
        public AudioSource Play(AudioClip clip, Vector3 point)
        {
            return Play(clip, point, soundVolume, 1.0f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume)
        {
            return Play(clip, point, volume, 1.0f);
        }

        public AudioSource Play(AudioClip clip, Vector3 point, float volume, float pitch)
        {
            //Create an empty game object
            GameObject go = new GameObject("Audio: " + clip.name);
            go.transform.position = point;

            //Create the source
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            if (_soundEnabled) { source.Play(); }
            Destroy(go, clip.length);
            return source;
        }

        #endregion
    }
}
