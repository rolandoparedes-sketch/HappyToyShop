using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class LinqExamples : MonoBehaviour
{
    public List<int> numbers = new List<int>() { 2, 13, 5, 1, 8, 10, 4 };


    void Start()
    {
        var numberHigherThan5 = numbers.Where(x => x > 5);


        numbers.ForEach(ctx => Debug.Log(ctx));
                    
        /*foreach (var number in numberHigherThan5)
        {
            Debug.Log(number);
        }*/
    }
    [Button]
    public void TestID()
    {
        int has4 = numbers.FirstOrDefault(ctx => ctx == 100);

        print(has4);
    }
    [Button]
    public void TestAny()
    {
        bool isTrue= numbers.Any(ctx => ctx >= 10);
        print(isTrue);
    }
    [Button]
    public void TestOrderByAcending()
    {
        numbers = numbers.OrderBy(ctx => ctx).ToList();
    }
    [Button]
    public void TestOrderByDescending()
    {
        numbers = numbers.OrderByDescending(ctx => ctx).ToList();
    }
    public struct Enemy
    {
        public string EnemyName;
        public int EnemyCost;
        public Enemy (string enemyName, int enemyCost)
        {
            EnemyName = enemyName;
            EnemyCost = enemyCost;
        }
    }
    public enum SelectBy
    {
        EnemyName,
        EnemyCost,
    }
    [Button]

    public void TestSelect()
    {
        List<Enemy> list = new List<Enemy>();
        list.Add(new Enemy("Tratalalero", 6));
        list.Add(new Enemy("Bombardiro", 5));
        list.Add(new Enemy("Chimpanzini", 4));
        list.Add(new Enemy("LaVacaSaturno", 3));
        list.Add(new Enemy("Frigocamello", 2));
        list.Add(new Enemy("Bombobini", 1));

        var result = list.Select(ctx => ctx.EnemyName).ToList();

        result.ForEach(ctx => Debug.Log(ctx));


    }
    [Button]
    public void TestTake()
    {
        var takeTest = numbers.Take(3).ToList();

        takeTest.ForEach(ctx => Debug.Log(ctx));
    }

    [Button]
    public void TestSkip()
    {

        var takeTest = numbers.Skip(3).ToList();

        takeTest.ForEach(ctx => Debug.Log(ctx));
    }
    public enum Type
    {
        None,
        Fire,
        Water,
        Earth
    }
    public struct Ability
    {
        public string AbilityName;
        public Type AbilityType;
        public Ability (string name, Type type)
        {
            AbilityName = name;
            AbilityType = type;
        }
    }
    public void TestGroupBy()
    {
        List<Ability> abilities = new();
        abilities.Add(new("WaterShoot", Type.Water));
        abilities.Add(new("Bubbles", Type.Water));

        abilities.Add(new("FireBall", Type.Fire));
        abilities.Add(new("Incinerate", Type.Fire));

        abilities.Add(new("Earthquake", Type.Earth));
        abilities.Add(new("Burrow", Type.Earth));

        var groupAbilities = abilities.GroupBy(key => key.AbilityType);

        Dictionary<Type, List<string>> dic = groupAbilities.ToDictionary
            (
            group => group.Key,
            group => group.Select(ability =>ability.AbilityName).ToList()
            );

        Dictionary<Type, List<Ability>> dic2 = groupAbilities.ToDictionary
            (
            group => group.Key,
            group => group.Select(ability => ability).ToList()
            );


    }
    [Button]
    public void TestChainLinq()
    {
        var result = numbers.Where(x => x != 1)
            .OrderByDescending(x => x)
            .Take(3)
            .Select(x => x.ToString())
            .ToList();
    }

    [Button]

    public void TestAll()
    {
        bool result = numbers.All(x => x != 1);
    }
    public void TestContains()
    {
        bool result = numbers.Contains(1);
    }
    public void TestCount()
    {
        numbers.Count(); 
    }    
   
}

