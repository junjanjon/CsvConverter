var csvConverterArgument = new CsvConverterArgument();
var argumentParser = new ArgumentParser();
argumentParser.AddParamHook("--input", delegate (string arg) { csvConverterArgument.InputFilePath = arg; });
argumentParser.AddParamHook("--output", delegate (string arg) { csvConverterArgument.OutputFilePath = arg; });
argumentParser.Parse(args);

Console.WriteLine(csvConverterArgument.InputFilePath);
Console.WriteLine(csvConverterArgument.OutputFilePath);

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
                var nextArg = args[i + 1];
                _paramHooks[arg](nextArg);
            }
        }
    }
}
