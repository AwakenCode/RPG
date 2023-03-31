using System;
using UnityEngine;
using UEInput = UnityEngine.Input;

namespace Service.Input
{
    public class PlayerInput : MonoBehaviour, IInputService
    {
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";

        public Vector2 Movement { get; private set; }

        public event Action AttackButtonClicked;
        public event Action ReloadButtonClicked;

        public void Update()
        {
            float inputX = UEInput.GetAxis(Horizontal);
            float inputY = UEInput.GetAxis(Vertical);

            Movement = new Vector2(inputX, inputY);

            if (UEInput.GetKeyDown(KeyCode.R))
                ReloadButtonClicked?.Invoke();

            if (UEInput.GetMouseButton(0))
                AttackButtonClicked?.Invoke();
        }
    }
}