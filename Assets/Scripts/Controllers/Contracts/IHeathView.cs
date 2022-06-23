namespace Controllers.Contracts
{
    public interface IHeathView
    {
        public void SetupHealth(float maxAmount, float currentAmount);
    }
}