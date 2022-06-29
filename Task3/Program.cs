namespace Task3;

internal static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Необходимо указать путь к папке параметром командной строки");
            Console.WriteLine("Пример:");
            Console.WriteLine("task1.exe <folder_path>");

            return;
        }

        var folderPath = args[0];

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Указанная папка не существует");

            return;
        }

        var folderRemover = new FolderRemover();

        folderRemover.Process(folderPath, TimeSpan.FromMinutes(30));
    }
}