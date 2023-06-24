namespace PepegaAR.Core.Player.WeaponSystem
{
    using Interfaces;

    public interface IWeaponManager
    {
        IWeapon CurrentWeapon { get; }
    }
}