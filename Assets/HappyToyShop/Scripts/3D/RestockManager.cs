using System.Collections.Generic;
using UnityEngine;

public class RestockManager : MonoBehaviour
{
    public static RestockManager instance;

    public List<RestockTask> tasks = new();

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        GenerateTasks();
    }
    public void GenerateTasks()
    {
        tasks.Clear();

        var dataGame = GameManager3D.instance.DataGame;

        for (int i = 0; i < dataGame.CurrentShelfsID.Count; i++)
        {
            RestockTask task = new RestockTask();

            task.toyData = GameManager3D.instance.FactorySystem.TakeToy(dataGame.CurrentShelfsID[i]);
            task.targetAmount = dataGame.CurrentShelfsAmount[i];
            task.currentAmount = 0;

            tasks.Add(task);
        }
    }

    public void AddToy(ToyData toyData)
    {
        RestockTask task = tasks.Find(x => x.toyData == toyData);

        if (task == null)
            return;

        task.currentAmount++;

        if (task.Completed)
        {
            Debug.Log($"{toyData.name} completado");
        }

        CheckCompleted();
    }

    private void CheckCompleted()
    {
        foreach (var task in tasks)
        {
            if (!task.Completed)
                return;
        }

        Debug.Log("Reposición terminada");

        GameManager3D.instance.DataGame.CurrentShelfsAmount.Clear();
        GameManager3D.instance.DataGame.CurrentShelfsID.Clear();
        ScenesManager.instance.ChangeMode2D();


    }
}