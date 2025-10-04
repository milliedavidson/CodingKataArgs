namespace CodingKataArgs;

/// <summary>
/// Represents a single item in an argument schema definition
/// Stores metadata about a command-line flag, including its character and expected data type
/// Used to validate and parse incoming command-line arguments
/// </summary>
public class ArgSchemaItem
{
    public char Flag { get; }

    public ArgType Type { get; }

    public ArgSchemaItem(char flag, ArgType type)
    {
        Flag = flag;
        Type = type;
    }
}