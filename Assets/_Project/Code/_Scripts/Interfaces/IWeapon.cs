namespace PepegaAR.Interfaces
{
    public interface IWeapon 
    {
        void StartShooting();
        void StopShooting();

        void SetActive(bool value);
    }
}