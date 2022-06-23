using Controllers.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour, IHeathView
    {
        [SerializeField] private Image _image;

        public void SetupHealth(float maxAmount, float currentAmount)
        {
            _image.fillAmount = currentAmount / maxAmount;
        }
    }
}