namespace LexiconMovieApi.Core.DTOs.Actor;

public class ActorCreateDto
{
    public string Name { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}