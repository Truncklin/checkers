using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    public class StartUI : MonoBehaviour
    {
        private GameObject _canvas;
        private Animator _animator;
        [SerializeField] private new GameObject menuCamera;
        [SerializeField] private new GameObject camera;

        private bool animationPlayed;
        private static readonly int AnimStart = Animator.StringToHash("AnimStart");

        private void Start()
        {
            _canvas = GameObject.FindWithTag("Menu");
            _animator = menuCamera.GetComponent<Animator>();
            camera.gameObject.SetActive(false);
        }

        public void StartAnimationCamera()
        {
            _canvas.SetActive(false);
            _animator.SetTrigger(AnimStart);
            animationPlayed = true;
            
            StartCoroutine(WaitForAnimation());
        }

        private IEnumerator WaitForAnimation()
        {
            float _second = 4.4f;
            yield return new WaitForSeconds(_second);
            menuCamera.gameObject.SetActive(false);
            camera.gameObject.SetActive(true);
        }
    }
}
