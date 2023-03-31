using System;
using UnityEngine;

namespace Service.Input
{
    public interface IInputService
    {
        event Action AttackButtonClicked;
        event Action ReloadButtonClicked;
        Vector2 Movement { get; }
    }
}
