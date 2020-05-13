using UnityEngine;
using UnityEngine.Events;

public class ButtonAction : MonoBehaviour, IActionable
{
    [SerializeField] private UnityEvent buttonEvent = default;

    public void Do(UIButton doButton)
    {
        buttonEvent.Invoke();
    }
}
