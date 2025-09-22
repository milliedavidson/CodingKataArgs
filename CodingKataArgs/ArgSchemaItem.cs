namespace CodingKataArgs;

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