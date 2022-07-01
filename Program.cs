var argv = new ArgumentParser()
    .AddDemandOption("input")
    .AddDemandOption("output")
    .Parse(args);

var csvConverterArgument = new CsvConverterArgument(argv["input"], argv["output"]);

ConvertCsvToTsv(csvConverterArgument.InputFilePath, csvConverterArgument.OutputFilePath);

void ConvertCsvToTsv(string inputFilePath, string outputFilePath)
{
    if (string.IsNullOrEmpty(inputFilePath) || string.IsNullOrEmpty(outputFilePath))
    {
        throw new ArgumentException("inputFilePath or outputFilePath is null or empty");
    }
    if (!File.Exists(inputFilePath))
    {
        throw new FileNotFoundException("Input file not found", inputFilePath);
    }
    // ReSharper disable InconsistentNaming
    const string COMMA = ",";
    const string TAB = "\t";
    var text = File.ReadAllText(inputFilePath);
    var convertedText = text.Replace(COMMA, TAB);
    File.WriteAllText(outputFilePath, convertedText);
}

struct CsvConverterArgument
{
    public CsvConverterArgument(string inputFilePath, string outputFilePath)
    {
        InputFilePath = inputFilePath;
        OutputFilePath = outputFilePath;
    }

    public string InputFilePath { get; set; }
    public string OutputFilePath { get; set; }
}

class ArgumentParser
{
    /// <summary>
    /// 必須オプションを追加する
    /// </summary>
    /// <param name="optionName"></param>
    /// <returns></returns>
    public ArgumentParser AddDemandOption(string optionName)
    {
        _optionKeys.Add(optionName);
        _demandOptions.Add(optionName, true);
        return this;
    }

    private readonly HashSet<string> _optionKeys = new();
    private readonly Dictionary<string, Boolean> _demandOptions = new();

    public Dictionary<string, string> Parse(string[] args)
    {
        Dictionary<string, string> resultArgv = new();
        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];

            if (arg.StartsWith("--"))
            {
                var optionKey = arg.Substring(2);
                if (_optionKeys.Contains(optionKey))
                {
                    resultArgv.Add(optionKey, args[i + 1]);
                    i++;
                }
                else
                {
                    Console.Error.WriteLine($"unknown option: {optionKey}");
                }
            }
        }

        foreach (var optionKey in _optionKeys)
        {
            if (!resultArgv.ContainsKey(optionKey))
            {
                if (_demandOptions[optionKey])
                {
                    Console.Error.WriteLine($"demand option: {optionKey}");
                    throw new ArgumentException($"demand option: {optionKey}");
                }
            }
        }

        return resultArgv;
    }
}
