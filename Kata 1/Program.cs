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
            //var warrior = characters.FirstOrDefault(c => c.Role == "Warrior");
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


/*
    character => character.Health < 50
        
    // Equivalent to Action<Character, int>.
// An Action accepts parameters but has no return type.
    Action <string, int>(target, damage) => {
        Console.WriteLine($"{target.Name} suffered {damage} damage.");
    }

// Equivalent to Func<Character, bool>.
// In a Func, the last parameter is always the return type.
    (target) => {
        return character.Health < 50;
    }
Character Creation:
    Create two characters, Warrior and Healer, without using inheritance.
    Each character should have a PrimaryAction property, an Action that holds a lambda representing a role-specific ability (e.g., attack for Warrior, heal for Healer).

Prioritize Actions Based on Health:

    Characters with health below 50 should attack first.
    The healer character should prioritize healing the character with the lowest health.

Invoke Actions Dynamically:

    Use each lambda to dynamically call specific actions based on character role and health status.

Expected Skill Outcome

    Learn to use Actions and lambdas to encapsulate unique abilities, simulating polymorphism without inheritance.
    Execute lambdas based on criteria such as character health and role, illustrating how lambdas create flexible and reusable code.
*/