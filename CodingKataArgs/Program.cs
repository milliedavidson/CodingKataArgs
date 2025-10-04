using CodingKataArgs;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // General usage
            var parser1 = new ArgsParser("l:bool,p:int,d:string", 
                new[] { "-l", "-p", "8080", "-d", "/usr/logs" });
            
            Console.WriteLine($"Logging: {parser1.GetBool('l')}");
            Console.WriteLine($"Port: {parser1.GetInt('p')}");
            Console.WriteLine($"Directory: {parser1.GetString('d')}");

            // Parsing lists and negative numbers
            var parser2 = new ArgsParser("g:stringlist,n:intlist", 
                new[] { "-g", "this,is,a,list", "-n", "1,2,-3,5" });
            
            Console.WriteLine($"String list: [{string.Join(", ", parser2.GetStringList('g'))}]");
            Console.WriteLine($"Int list: [{string.Join(", ", parser2.GetIntList('n'))}]");

            // Using default values when no arguments provided
            var parser3 = new ArgsParser("l:bool,p:int,d:string", new string[] { });
            
            Console.WriteLine($"Default logging: {parser3.GetBool('l')}");
            Console.WriteLine($"Default port: {parser3.GetInt('p')}");
            Console.WriteLine($"Default directory: '{parser3.GetString('d')}'");
        }
        catch (ArgsException ex)
        {
            Console.WriteLine($"Args Error: {ex.Message}");
        }
    }
}
