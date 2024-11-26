namespace Kata_2;
public class Character
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }
    public delegate void CharacterAction(Character target, int damage);

    public void Attack(Character target, int damage)
    {
        target.Health -= damage;
        Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage");

        target.HealthChanged?.Invoke(target.Name, target.Health);
    }
    public event Action<string, int> HealthChanged;
}
public class Program
{
    public void Players()
    {
        Character character1 = new Character("Ewa", 60);
        Character character2 = new Character("Leo", 80); 
        
        character1.HealthChanged += OnHealthChanged;
        character2.HealthChanged += OnHealthChanged;

        Character.CharacterAction action = character1.Attack;
        action(character2, 15);
        action = character2.Attack;
        action(character1, 20);
        action = character1.Attack;
        action(character2, 10);
    }
    private void OnHealthChanged(string name, int newHealth)
    {
        Console.WriteLine($"[Event] {name}'s health changed to {newHealth}.");
    }
    public static void Main(string[] args)
    {
        new Program().Players();
    }
}
