using Lesson11;
using Lesson11.Data.Model;
using Lesson11.Implementations;
using Lesson11.Interfaces;

Menu menu = new();
IMovieService movieService = new MovieService();
IFileService fileService = new FileService();

menu.DisplayMenu();

MenuChoice choice = menu.GetMenuChoice();

bool flag = true;
while (flag)
{
    switch (choice.Id)
    {
        case 1:
            Console.WriteLine($"You chose {choice.Description}");

            Console.WriteLine("Enter movie name:");
            var movieName = Console.ReadLine();
            
            var res = movieService.SearchMovie(movieName);
            
            if (res != null && res.Results != null && res.Results.Any())
            {
                var movies = res.Results.Select(r => new Movie(r)).ToList();
                
                // Выводим результаты поиска
                foreach (var movie in movies)
                {
                    Console.WriteLine($"Movie: {movie.Title} ({movie.Description})");
                }

                // Сохраняем результаты
                fileService.Save(movies);
            }
            else
            {
                Console.WriteLine("No movies found.");
            }
            break;
        case 2:
            Console.WriteLine($"You chose {choice.Description}");
            break;
        case 3:
            Console.WriteLine($"You chose {choice.Description}");
            Console.WriteLine("Enter movie title to delete:");
            var titleToDelete = Console.ReadLine();
            fileService.Delete(titleToDelete);
            break;
        case 4:
            flag = false;
            Console.WriteLine("Exit");
            break;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
}

Console.WriteLine("Goodbye!");