using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Gameplay
{
    public class MainUIManager : MonoBehaviour
    {
        [SerializeField] private Slider shieldSlider;
        [SerializeField] private Slider lifeSupportSlider;
        [SerializeField] private TMP_Text shieldText, lifeSupportText;
        
        public void UpdateUI(float Shield, float LifeSupport)
        {
            shieldSlider.value = Shield / 100;
            shieldText.text = Shield.ToString();
            lifeSupportSlider.value = LifeSupport / 100;
            lifeSupportText.text = LifeSupport.ToString();

        }
    }
}