namespace GitHubTreadingCLI;

public class CliOptions
{
    public string Language {get; set;} = "csharp";
    public string Since {get; set;} ="daily"; //daily, weekly, month

    public static CliOptions Parse(string[] args)
    {
        var options = new CliOptions();
        for(int i = 0; i < args.Length; i++)
        {
            string arg = args[i].ToLower();
            if((arg == "--Language" || arg == "-l") && i + 1 < args.Length)
            {
                options.Language = args[++i];
            }
            else if((arg == "--since" || arg == "-s") && i + 1 < args.Length)
            {
                options.Since = args[++i];
            }

        }

        return options;
    }
}
