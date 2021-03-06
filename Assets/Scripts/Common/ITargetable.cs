﻿using UnityEngine;
using Assets.Scripts.Game;

namespace Assets.Scripts.Common
{
    public interface ITargetable
    {
        Transform Transform { get; }
        Team Team { get; set; }
        void ReceiveDamage(int damage);
        bool IsDead { get; }
    }
}