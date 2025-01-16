using Lesson11.Data.Model;

namespace Lesson11.Interfaces;

public interface IFileService
{
    public void Save(List<Movie> movies);
    public void Delete(string movieTitle);
}