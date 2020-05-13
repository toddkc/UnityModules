using UnityEngine;

public class DisableOnAwake : MonoBehaviour
{
    [SerializeField] GameObject objectToDisable = default;
    private void Awake()
    {
        objectToDisable.SetActive(false);
    }
}
