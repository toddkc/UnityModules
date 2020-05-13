namespace Utility
{
    using UnityEngine;

    public class ColorController : MonoBehaviour
    {
        [SerializeField] private Color defaultColor = Color.white;
        [SerializeField] private bool isEmission = false;
        private Renderer rend;

        public Color DefaultColor
        {
            get { return defaultColor; }
            set { defaultColor = value; }
        }

        private void Awake()
        {
            rend = GetComponent<Renderer>();
            SetColor(DefaultColor);
        }

        public void SetColor(Color color)
        {
            if (isEmission) rend.material.SetColor("_EmissionColor", color);
            else rend.material.SetColor("_Color", color);
        }
    }
}