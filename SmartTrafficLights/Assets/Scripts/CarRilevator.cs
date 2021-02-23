﻿using UnityEngine;

public class CarRilevator : MonoBehaviour
{
    private const string NorthGenerated = "north";
    private const string SouthGenerated = "south";

    private SemaphoreController _semaphore;

    [SerializeField] private Data data;

    private void Awake()
    {
        _semaphore = GetComponentInParent<SemaphoreController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CarMover mover = other.GetComponent<CarMover>();

        if (mover != null)
        {
            _semaphore.CarsOnRilevator++;
            _semaphore.TotalCarRilevated++;

            mover.Semaphore = _semaphore;
            mover.tag = _semaphore.gameObject.tag;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CarMover mover = other.GetComponent<CarMover>();

        if (mover != null)
        {
            _semaphore.CarsOnRilevator--;

            if (mover.TimeWaited > 0f)
            {
                data.TotalNCarBeenWaiting++;

                if (mover.tag.Equals(NorthGenerated))
                    data.NCarBeenWaitingNorth++;

                if (mover.tag.Equals(SouthGenerated))
                    data.NCarBeenWaitingSouth++;
            }
        }
    }
}