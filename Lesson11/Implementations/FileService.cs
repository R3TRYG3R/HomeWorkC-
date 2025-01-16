using Lesson11.Interfaces;
using Lesson11.Data.Model;
using System.Text.Json;

namespace Lesson11.Implementations;

public class FileService : IFileService
{
    private const string FilePath = "./Data/Movies.json";

    public void Save(List<Movie> movies)
    {
        List<Movie> currentMovies = new List<Movie>();
        
        if (File.Exists(FilePath))
        {
            var existingMoviesJson = File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(existingMoviesJson))
            {
                currentMovies = JsonSerializer.Deserialize<List<Movie>>(existingMoviesJson);
            }
        }
        
        currentMovies.AddRange(movies);
        
        var jsonString = JsonSerializer.Serialize(movies, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, jsonString);

        Console.WriteLine("Movies saved successfully.");
    }


    public void Delete(string movieTitle)
    {
        if (!File.Exists(FilePath)) return;
        
        var existingMoviesJson = File.ReadAllText(FilePath);
        if (string.IsNullOrEmpty(existingMoviesJson)) return;

        var currentMovies = JsonSerializer.Deserialize<List<Movie>>(existingMoviesJson);

        // Находим фильм по названию
        var movieToDelete = currentMovies.FirstOrDefault(m => m.Title.Equals(movieTitle, StringComparison.OrdinalIgnoreCase));

        if (movieToDelete != null)
        {
            currentMovies.Remove(movieToDelete);

            // Сохраняем обновленный список в файл
            var jsonString = JsonSerializer.Serialize(currentMovies, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, jsonString);

            Console.WriteLine($"Movie '{movieTitle}' deleted successfully.");
        }
        else
        {
            Console.WriteLine($"Movie with title '{movieTitle}' not found.");
        }
    }
}