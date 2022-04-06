using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackingManager : MonoBehaviour
{
    public static StackingManager instance;

    public enum stackingTypes { backward, forward };

    [SerializeField]
    [Tooltip("Direction of stacking")]
    private stackingTypes _stackingType;

    [SerializeField]
    [Tooltip("First position of stacking queue")]
    private Transform _stackingPosition;

    [SerializeField]
    [Min(0f)]
    [Tooltip("Distance between each stacked object")]
    private float _stackingGap = 0.3f;

    [SerializeField]
    [Tooltip("Scaling multiplier to scale down object when stacked")]
    private float _scaleMultiplier = 1f;

    private List<Transform> _stackedObjects;
    private BankAccount _bankAccount;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;

        _stackedObjects = new List<Transform>();
        _stackedObjects.Add(_stackingPosition); //Set stacking position as first member of the list so other objects will follow it

        //Set stacking direction based on the setting on inspector
        switch (_stackingType)
        {
            case stackingTypes.backward:
                _stackingGap *= -1;
                break;
            default:
                break;
        }
    }

    private void Start()
    {
        _bankAccount = GetComponent<BankAccount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            other.gameObject.layer = LayerMask.NameToLayer("Stacked");
            StackObject(other.transform, true);
        }
    }

    //Called when player stacks money and gained money from questions
    public void StackObject(Transform objectToStack, bool scaleDown = false)
    {
        objectToStack.GetComponent<StackedObjectMovement>().FollowQueue(_stackedObjects[_stackedObjects.Count - 1], _stackingGap);
        _stackedObjects.Add(objectToStack);
        _bankAccount.EarnMoney();

        //Stacked money will need to scale down but gained money from questions won't need to
        if (scaleDown && _scaleMultiplier != 1)
            StartCoroutine(ScaleObjectDown(objectToStack));
    }

    //Called by MoneyBetting script on player to remove first object from list as it's bet
    public void BetMoney()
    {
        if (_stackedObjects.Count < 2) //If the list that contains money has less than 2 elements, it means the list is empty since the first element is not money but is following target for first money
            return;

        //Stop first money following character, remove from the list
        _stackedObjects[1].GetComponent<StackedObjectMovement>().StopFollowingQueue();
        _stackedObjects.RemoveAt(1);
        _bankAccount.SpendMoney();

        if (_stackedObjects.Count > 1) //If player still has money stacked, set the first one's following target to stackingPosition
            _stackedObjects[1].GetComponent<StackedObjectMovement>().FollowQueue(_stackingPosition, _stackingGap);
    }

    //Called when a new object stacked. Scales it down
    private IEnumerator ScaleObjectDown(Transform _object)
    {
        Vector3 startingScale = _object.localScale;
        Vector3 targetScale = startingScale * _scaleMultiplier;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 3f;
            _object.localScale = Vector3.Lerp(startingScale, targetScale, t);
            yield return null;
        }
    }

    //Called by MoneyBetting script on player to bet first money on list and place it on betting zone
    public Transform firstObjectInQueue
    {
        get
        {
            return _stackedObjects[1]; //Index is 1 because very first element is first money's following target
        }
    }

    public bool hasMoney
    {
        get
        {
            return (_stackedObjects.Count > 1);
        }
    }

    public List<Transform> stackedObjects
    {
        get { return _stackedObjects; }
    }
}
