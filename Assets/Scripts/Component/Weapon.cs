using System.Collections.Generic;
using UnityEngine;

namespace SwecorTestSavchenko
{
    public class Weapon : MonoBehaviour
    {
        public List<Gun> Guns = new();
        public Gun ActiveGun;
    }

    [System.Serializable]
    public class Gun
    {
        public float Cooldown;
        public ShellType ShootingShellType;
        public float CurrentCooldown;
        public bool IsActive;
    }

    public enum ShellType
    {
        FastAndWeak,
        SlowAndStrong
    }
}