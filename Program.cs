var csvConverterArgument = new CsvConverterArgument();
var argumentParser = new ArgumentParser();
argumentParser.AddParamHook("--input", delegate (string arg) { csvConverterArgument.InputFilePath = arg; });
argumentParser.AddParamHook("--output", delegate (string arg) { csvConverterArgument.OutputFilePath = arg; });

try
{
    argumentParser.Parse(args);
}
catch (ArgumentException e)
{
    Console.Error.WriteLine(e.Message);
    return 1;
}

// ReSharper disable InconsistentNaming
const string COMMA = ",";
const string TAB = "\t";

var text = File.ReadAllText(csvConverterArgument.InputFilePath);
var convertedText = text.Replace(COMMA, TAB);
File.WriteAllText(csvConverterArgument.OutputFilePath, convertedText);
return 0;

struct CsvConverterArgument
{
    public string InputFilePath { get; set; }
    public string OutputFilePath { get; set; }
}

class ArgumentParser
{
    public void AddParamHook(string key, Action<string> action)
    {
        _paramHooks.Add(key, action);
    }

    private readonly Dictionary<string, Action<string>> _paramHooks = new();

    public void Parse(string[] args)
    {
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            if (_paramHooks.ContainsKey(arg))
            {
                if (args.Length <= i + 1)
                {
                    throw new ArgumentException($"{arg}: requires an argument");
                }
                else
                {
                    var nextArg = args[i + 1];
                    _paramHooks[arg](nextArg);
                    i++;
                }
            }
        }
    }
}
