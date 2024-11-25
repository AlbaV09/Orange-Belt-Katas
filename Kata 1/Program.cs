namespace Kata_1;
public class Character
{   
    public string Name { get; private set; }
    public int Health { get; private set; }
    public string Role { get; private set; }
    public Action PrimaryAction { get; set; }

    public Character(string name, int health, string role, Action primaryAction )
    {
        Name = name;
        Health = health;
        Role = role;
        PrimaryAction = primaryAction;
    }

    public void Heal(int amount, Character warrior)
    {
        warrior.Health += amount;
        Console.WriteLine($"{Name} heals {warrior.Name} for {amount} health! Current Health: {warrior.Health}");
    }
}

public class Program
{
    public void Players()
    {
        List<Character> characters = new List<Character>
        {
            new Character("Ewa", 80, "Warrior", () => Console.WriteLine("Ewa attacks with a mighty strike!")),
            new Character("Leo", 90, "Warrior", () => Console.WriteLine("Leo charges with a fierce attack")),
            new Character("Sol", 30, "Warrior", () => Console.WriteLine("Sol attacks with a fireball")),
            new Character("Mira", 70, "Healer", null)
        };
            var healer = characters.FirstOrDefault(c => c.Role == "Healer");
            if (healer != null) 
            {
                healer.PrimaryAction = () =>
                {
                    var target = characters.Where(c => c.Role != "Healer").OrderBy(c => c.Health).First();
                    Console.WriteLine($"{healer.Name} is healing {target.Name} who has the lowest health.");
                    healer.Heal(20, target);
                };
            }
            
        Console.WriteLine("Starting actions based on character health...\n\nCharacters attacking first (health < 50):");
        characters
            .Where(character => character.Health < 50  && character.Role == "Warrior")
            .ToList()
            .ForEach(character =>
            {
                Console.WriteLine($"{character.Name} is attacking first due to low health! Current Health: {character.Health}");
                character.PrimaryAction?.Invoke();
            });
        Console.WriteLine();
        Console.WriteLine("Additional character actions based on role:");
        characters
            .Where(character => character.Role == "Healer" && character.PrimaryAction != null)
            .ToList()
            .ForEach(character =>
            {
                character.PrimaryAction?.Invoke();
            });
        Console.WriteLine();     
        characters
            .Where(character => character.Role == "Warrior")
            .ToList()
            .ForEach(character =>
            {
                Console.WriteLine($"{character.Name} is standing by with health: {character.Health}");
                character.PrimaryAction?.Invoke();
                Console.WriteLine();
            });
    }

    public static void Main(string[] args)
    {
        Program program = new Program();
        program.Players();
    }
}