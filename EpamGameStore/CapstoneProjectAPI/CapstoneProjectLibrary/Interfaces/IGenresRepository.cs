using CapstoneProjectLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneProjectLibrary.Interfaces
{
    public interface IGenresRepository
    {
        public int AddGenreToList(string GenreName);
        public void AsignGenresToGame(int gameId, int[] genresToAsign);
        public List<GameGenre> GetGenresList();
        public List<GameGenre> GetGameGenres(int gameId);
    }
}
