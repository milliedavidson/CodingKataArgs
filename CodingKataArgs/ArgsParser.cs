namespace CodingKataArgs;

public class ArgsParser
{
    private readonly Dictionary<char, ArgSchemaItem> _schemaItem;
    private readonly Dictionary<char, object> _values;

    public ArgsParser(string schema, string[] args)
    {
        _schemaItem = ParseSchema(schema);
        _values = ParseArgs(args);

        SetDefaults();
    }

    private Dictionary<char, ArgSchemaItem> ParseSchema(string schema)
    {
        var dictionary = new Dictionary<char, ArgSchemaItem>();

        if (string.IsNullOrWhiteSpace(schema))
            return dictionary;

        foreach (var part in schema.Split(','))
        {
            var trimmed = part.Trim();

            if (string.IsNullOrWhiteSpace(trimmed))
                continue;

            var colonIndex = trimmed.IndexOf(':');

            if (colonIndex == -1)
                throw new ArgsException($"Invalid schema format: '{part}'. Expected 'flag:type'");

            if (colonIndex != 1)
                throw new ArgsException($"Flag must be single character: '{part}'");

            var flag = trimmed[0];
            var typeStr = trimmed.Substring(2).ToLower();

            var type = typeStr switch
            {
                "bool" => ArgType.Bool,
                "int" => ArgType.Int,
                "string" => ArgType.String,
                "intlist" => ArgType.IntList,
                "stringlist" => ArgType.StringList,
                _ => throw new ArgsException($"Unknown type '{typeStr}' for flag '{flag}'")
            };

            if (dictionary.ContainsKey(flag))
                throw new ArgsException($"Duplicate flag in schema: '{flag}'");

            dictionary[flag] = new ArgSchemaItem(flag, type);
        }

        return dictionary;
    }

    private Dictionary<char, object> ParseArgs(string[] args)
    {
        var values = new Dictionary<char, object>();

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (!arg.StartsWith("-") || arg.Length != 2)
                throw new ArgsException(
                    $"Invalid argument format: '{arg}'. Expected '-x' where x is a single character");

            var flag = arg[1];

            if (!_schemaItem.ContainsKey(flag))
                throw new ArgsException($"Unknown flag: '-{flag}'");

            var schemaItem = _schemaItem[flag];

            if (schemaItem.Type == ArgType.Bool)
            {
                values[flag] = true;
            }
            else
            {
                if (i + 1 >= args.Length)
                    throw new ArgsException($"Flag '-{flag}' expects a value but none was provided");

                var valueStr = args[i + 1];

                // Check if it's actually a flag (starts with - and is exactly 2 chars with a letter)
                if (valueStr.StartsWith("-") && valueStr.Length == 2 && char.IsLetter(valueStr[1]))
                    throw new ArgsException($"Flag '-{flag}' expects a value but found another flag: '{valueStr}'");

                values[flag] = ParseValue(flag, valueStr, schemaItem.Type);
                i++; // Skip the value argument
            }
        }

        return values;
    }

    private object ParseValue(char flag, string valueStr, ArgType type)
    {
        try
        {
            return type switch
            {
                ArgType.Int => int.Parse(valueStr),
                ArgType.String => valueStr,
                ArgType.IntList => valueStr.Split(',').Select(s => int.Parse(s.Trim())).ToList(),
                ArgType.StringList => valueStr.Split(',').Select(s => s.Trim()).ToList(),
                _ => throw new ArgsException($"Unsupported type for flag '-{flag}'")
            };
        }
        catch (FormatException)
        {
            throw new ArgsException($"Invalid {type.ToString().ToLower()} value for flag '-{flag}': '{valueStr}'");
        }
        catch (OverflowException)
        {
            throw new ArgsException($"Value out of range for flag '-{flag}': '{valueStr}'");
        }
    }

    private void SetDefaults()
    {
        foreach (var schemaItem in _schemaItem.Values)
        {
            if (!_values.ContainsKey(schemaItem.Flag))
            {
                _values[schemaItem.Flag] = schemaItem.Type switch
                {
                    ArgType.Bool => false,
                    ArgType.Int => 0,
                    ArgType.String => "",
                    ArgType.IntList => new List<int>(),
                    ArgType.StringList => new List<string>(),
                    _ => throw new ArgsException($"No default value defined for type: {schemaItem.Type}")
                };
            }
        }
    }

    private T Get<T>(char flag)
    {
        if (!_schemaItem.ContainsKey(flag))
            throw new ArgsException($"Flag '{flag}' not defined in schema");

        return (T)_values[flag];
    }

    public bool GetBool(char flag) => Get<bool>(flag);
    public int GetInt(char flag) => Get<int>(flag);
    public string GetString(char flag) => Get<string>(flag);
    public List<int> GetIntList(char flag) => Get<List<int>>(flag);
    public List<string> GetStringList(char flag) => Get<List<string>>(flag);
}