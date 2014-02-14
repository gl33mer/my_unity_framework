using UnityEngine;
using Com.Nravo.Framework;

namespace Com.Nravo.Framework
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [Range(0.0f, 1.0f)] public float soundVolume;
        [Range(0.0f, 1.0f)] public float musicVolume;

        public AudioClip backgroundMusic;
        public AudioClip planeMusic;

        public bool SoundEnabled { set; get; }
        public bool MusicEnabled { set; get; }

        private AudioSource _backgroundMusicSource;

        #region init

        public override void Init()
        {
            SoundEnabled = true;
            MusicEnabled = true;
            CreateBackgroundMusicSource();
            if (MusicEnabled) { TurnMusicOn(); }
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

        #region music

        private void TurnMusicOff()
        {
            if (_backgroundMusicSource.isPlaying)
            {
                _backgroundMusicSource.Pause();
            }
            MusicEnabled = false;
        }

        private void TurnMusicOn()
        {
            _backgroundMusicSource.Play();
            MusicEnabled = true;
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
            if (SoundEnabled) { source.Play(); }
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
            if (SoundEnabled) { source.Play(); }
            Destroy(go, clip.length);
            return source;
        }

        #endregion
    }
}
