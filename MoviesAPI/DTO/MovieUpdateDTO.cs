using System;


namespace MoviesAPI.DTO
{
    public class MovieUpdateDTO: MovieCreateDTO
    {
       public Guid MoviesId { get; set; }
    }
}
