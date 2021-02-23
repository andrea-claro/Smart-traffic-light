using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    private const string NorthGenerator = "north";
    private const string SouthGenerator = "south";

    private bool _hasStopped;
    private int _carsCounter;
    private int _maxCarSpawn;

    [SerializeField] private float minSpawnIntervalInSeconds;
    [SerializeField] private float maxSpawnIntervalInSeconds;

    [SerializeField] private List<GameObject> spawnableObjects = new List<GameObject>();

    [SerializeField] private AgentManager manager;
    [SerializeField] private Data data;
    [SerializeField] private UIController uiController;

    private IEnumerator Spawn()
    {
        if (_carsCounter < _maxCarSpawn)
        {
            var spawned = Instantiate(GetRandomSpawnableFromList(), transform.position, transform.rotation, transform);

            _carsCounter++;

            CarMover mover = spawned.GetComponent<CarMover>();
            mover.Direction = transform.forward;

            if (_carsCounter == _maxCarSpawn)
                mover.IsLastOne = true;

            data.TotalNCarGenerated++;

            if (gameObject.tag.Equals(NorthGenerator))
                data.NCarGeneratedNorth++;

            if (gameObject.tag.Equals(SouthGenerator))
                data.NCarGeneratedSouth++;

            yield return new WaitForSeconds(Random.Range(minSpawnIntervalInSeconds, maxSpawnIntervalInSeconds));
            StartCoroutine();
        }
    }

    private GameObject GetRandomSpawnableFromList()
    {
        int randomIndex = Random.Range(0, spawnableObjects.Count);
        return spawnableObjects[randomIndex];
    }

    private void StopCoroutine()
    {
        StopCoroutine(nameof(Spawn));
    }

    private void StartCoroutine()
    {
        StartCoroutine(nameof(Spawn));
    }

    public void Reset()
    {
        StopCoroutine();

        _carsCounter = 0;

        StartCoroutine();
    }

    private void OnTriggerStay(Collider other)
    {
        CarMover thisCar = other.GetComponent<CarMover>();

        if (thisCar != null)
            if (!thisCar.IsMoving && !_hasStopped)
            {
                StopCoroutine();
                _hasStopped = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {
        CarMover thisCar = other.GetComponent<CarMover>();

        if (thisCar != null)
            if (thisCar.IsMoving && _hasStopped)
            {
                StartCoroutine();
                _hasStopped = false;
            }
    }

    public void GettingSettingsFromMenu()
    {
        if (gameObject.tag.Equals(NorthGenerator))
        {
            if (SkipMenuData.NorthGeneratorMaxCarSpawn != 0)
                _maxCarSpawn = SkipMenuData.NorthGeneratorMaxCarSpawn;
            else
                _maxCarSpawn = 1000;

            uiController.maxNCarGeneratedNorthValue.text = _maxCarSpawn.ToString();
        }

        if (gameObject.tag.Equals(SouthGenerator))
        {
            if (SkipMenuData.SouthGeneratorMaxCarSpawn != 0)
                _maxCarSpawn = SkipMenuData.SouthGeneratorMaxCarSpawn;
            else
                _maxCarSpawn = 1000;

            uiController.maxNCarGeneratedSouthValue.text = _maxCarSpawn.ToString();
        }
    }

    public void OnMaxCarGenerated()
    {
        manager.OnEnd(generator: true);
    }
}