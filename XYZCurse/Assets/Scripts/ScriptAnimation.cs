using System;
using UnityEngine;
using UnityEngine.Events;

namespace Script
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ScriptAnimation : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] private int _frameRate = 10;
        [SerializeField] private UnityEvent<string> _onComplete;  // эвент когда конец
        [SerializeField] private AnimationClips[] _clips;  //массив спрайтов

        private SpriteRenderer _renderer;      //рендер в котором меняем спрайты

        //сервесные переменные
        private float _secondPerFrame;         //сколько секунд для показа 1 спрайта
        private float _nextFrameTime;          //time for next update
        private int _currentFrame;             //index sprite from massiv
        private bool _isPlaing = true;         //index sprite from massiv

        private int _currentClip;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();     //забираем зависимости
            _secondPerFrame = 1f / _frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaing;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaing = false;
        }

        private void StartAnimation()
        {
            _nextFrameTime = Time.time ;
            enabled = _isPlaing = true;
            _currentFrame = 0;
        }


        private void Update()
        {
            if (_nextFrameTime > Time.time) return;

            var clip = _clips[_currentClip];  
            if(_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentFrame = 0;//если цикл то сбрасываем на ноль
                }
                else
                {
                    enabled = _isPlaing = clip.AllowNextClip;
                    clip.OnComplete?.Invoke();
                    _onComplete?.Invoke(clip.Name);
                    if (clip.AllowNextClip) 
                    {
                        _currentFrame = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                    }
                    return;
                }
            }

            _renderer.sprite = clip.Sprites[_currentFrame];

            _nextFrameTime += _secondPerFrame;
            _currentFrame++;
        }
    }

    [Serializable]
    public class AnimationClips
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool Loop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplete => _onComplete;
    }
}