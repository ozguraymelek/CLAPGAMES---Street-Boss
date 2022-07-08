using UnityEngine;

public class IdleTrigger : MonoBehaviour
{
    private BoxCollider _collider;
    [SerializeField] private GameSettings gameSettings;
    private void Start()
    {
        _collider = GetComponent<BoxCollider>();
        CkyEvents.OnTransToRunner += OnTransToRunner;
    }

    private void OnTriggerEnter(Collider other)
    {
        IStackable _stackable = other.GetComponent<IStackable>();

        if(other.GetComponent<Prince>()!=null)
            StackManager.Instance.InteractHand();
        
        if (_stackable == null) return;

        if (other.GetComponent<Food>().isOnList == false) return;

        StackManager.Instance.FoodMovesToCircle(other.GetComponent<Food>());
        
    }

    private void OnTransToRunner()
    {
        EnableColliderTrigger(true);
        ActivateGameInfoPanel();
    }

    public void EnableColliderTrigger(bool b)
    {
        _collider.isTrigger = b;
    }

    public void ActivateGameInfoPanel()
    {
        UI_Manager.Instance.panelGameInfoCanvas.SetActive(true);
    }
}