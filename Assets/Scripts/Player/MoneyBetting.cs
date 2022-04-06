using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyBetting : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Time to bet another pack of money")]
    private float _bettingSpeed = .5f;

    private bool _bettingMoney = false;
    private float _currentTime = 0;

    private StackingManager _stackingManager;
    private PlayerController _playerController;
    private OptionManager _option;

    private void Start()
    {
        _stackingManager = GetComponent<StackingManager>();
        _playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Betting Zone"))
        {
            if (!_stackingManager.hasMoney)
                return;

            _playerController.slowerRun = true;
            _bettingMoney = true;
            _option = other.GetComponentInParent<OptionManager>();
            BetMoney();
            _currentTime = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Betting Zone"))
        {
            _playerController.slowerRun = false;
            _bettingMoney = false;
            _option = null;
        }
    }

    private void Update()
    {
        //Bet money once in a while if player's in betting zone and has money
        if (!_bettingMoney || !_stackingManager.hasMoney)
            return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _bettingSpeed && _option != null)
        {
            _currentTime = 0;
            BetMoney();
        }
    }

    private void BetMoney()
    {
        if (_option.BetMoney(_stackingManager.firstObjectInQueue)) //Doesn't bet money if option has reached its capacity
            _stackingManager.BetMoney();
    }
}
