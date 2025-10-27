using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace LeapLord
{
    public class ButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        //private Button button;
        [SerializeField] private TMP_Text buttText;

        [SerializeField] private Color focusColor = Color.green;
        [SerializeField] private Color unFocusColor = Color.yellow;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            buttText.color = unFocusColor;
            //Debug.Log(buttText.text);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

            buttText.color = focusColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            buttText.color = unFocusColor;
        }

        public void ResetButtonText()
        {
            buttText.color = unFocusColor;
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}


