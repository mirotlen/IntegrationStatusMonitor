using System.Dynamic;

public class UserInterface
{

    public string[] Run()
    {
        //string folderPath = GetPath();
        //string[] csvFiles = GetFiles(folderPath);
        string[] csvFiles = GetFiles("C:\\Users\\vikto\\source\\repos\\IntegrationStatusMonitor\\TestData");
        string selectedFile = ChooseFileFrom(csvFiles);
        var result = ReadFile(selectedFile);
        return result;
    }
    private string GetPath()
    {
        Console.WriteLine("Podaj ścieżkę do folderu z plikami .csv:");

        string folderPath = Console.ReadLine(); //trim?

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine("Podana ścieżka nie istnieje.");
            GetPath();
        }

        return folderPath;
    }
    private string[] GetFiles(string folderPath, string searchPattern="*.csv") //dać defaultową ścieżkę
    {
        string[] csvFiles = Directory.GetFiles(folderPath, "*.csv");
        if (csvFiles.Length == 0)
        {
            Console.WriteLine("Nie znaleziono plików CSV w podanym folderze.");
            GetPath();
        }
        return csvFiles;
    }
    private string ChooseFileFrom(string[] csvFiles)
    {
        Console.WriteLine("\nZnalezione pliki CSV:");
        for (int i = 0; i < csvFiles.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
        }

        Console.WriteLine("\nWybierz numer pliku do wczytania:");
        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > csvFiles.Length)
        {
            Console.WriteLine("Nieprawidłowy wybór.");
            Console.WriteLine("\nWybierz numer pliku do wczytania:");
        }

        string selectedFile = csvFiles[choice - 1]; // handle index out of range
        Console.WriteLine($"\nWczytywanie pliku: {Path.GetFileName(selectedFile)}\n");
        return selectedFile;
    }
    private string[] ReadFile(string file)
    {
        try
        {
            if(file == null || !File.Exists(file)){
                return new string[0];
            }
            var result = File.ReadAllLines(file); // edit
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas wczytywania pliku: {ex.Message}");
            return new string[0];
        }
    }
}