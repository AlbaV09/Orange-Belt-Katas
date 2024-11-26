namespace Kata_3;
public class AbilityContainer<T> where T : IAbility
{
    private List<T> abilities = new List<T>();

    public void AddAbility(T ability)
    {
        abilities.Add(ability);
        
        string abilityType = ability switch
        {
            AttackAbility => "Attack Ability",
            HealAbility => "Heal Ability",
            _ => "Unknown Ability"
        };
        Console.WriteLine($"- {abilityType} added: {ability.Name} (Effect: {ability.Effect})");
    }

    public void RemoveAbility(T ability)
    {
        abilities.Remove(ability);
        
        Console.WriteLine($"- {ability.Name} removed");
    }

    public IEnumerable<T> GetAbilities()
    {
        return abilities;
    }
}

public interface IAbility
{ 
    string Name { get; }
    string Effect { get; }
}

public class AttackAbility : IAbility
{
    public string Name { get; private set; }
    public string Effect { get; private set; }

    public AttackAbility(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }
}
public class HealAbility : IAbility
{
    public string Name { get; private set; }
    public string Effect { get; private set; }

    public HealAbility(string name, string effect)
    {
        Name = name;
        Effect = effect;
    }
}

class Program
{
    public void Adding()
    {
        var abilityContainer = new AbilityContainer<IAbility>();
        var slashAttack = new AttackAbility("Slash Attack", "Deals 15 damage");
        var healingLight = new HealAbility("Healing Light", "Restores 20 damage");
        
        Console.WriteLine("Adding abilities to the container...");
        
        abilityContainer.AddAbility(slashAttack);
        abilityContainer.AddAbility(healingLight);
        
        Console.WriteLine("\nListing all abilities in the container:");
        foreach (var ability in abilityContainer.GetAbilities())
        {
            Console.WriteLine($"- {ability.Name} (Effect: {ability.Effect})");
        }
    }
    public static void Main(string[] args)
    {
      new Program().Adding();  
    }
}