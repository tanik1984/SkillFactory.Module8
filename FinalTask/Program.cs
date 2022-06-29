using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask;

internal static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Необходимо указать путь к файлу параметром командной строки");
            Console.WriteLine("Пример:");
            Console.WriteLine("FinalTask.exe <file_path>");

            return;
        }

        var fileName = args[0];

        if (!File.Exists(fileName))
        {
            Console.WriteLine("Указанный файл не существует");

            return;
        }

        var binaryFormatter = new BinaryFormatter();

        var fileStream = File.OpenRead(fileName);

        var deserializedObject = binaryFormatter.Deserialize(fileStream);

        if (deserializedObject is Student[] students)
            CreateGroups(students);
    }

    private static void CreateGroups(Student[] students)
    {
        var desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        var studentsFolder = Path.Combine(desktopFolder, "Students");

        Directory.CreateDirectory(studentsFolder);

        foreach (var student in students)
        {
            var groupFileName = Path.Combine(studentsFolder, student.Group);

            using (var fileStream = File.OpenWrite(groupFileName))
            {
                fileStream.Seek(0, SeekOrigin.End);

                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"{student.Name}, {student.DateOfBirth}");
                }
            }
        }
    }
}