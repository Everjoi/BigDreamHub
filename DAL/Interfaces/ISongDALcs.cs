using MySite.Models;
using System.Collections.Generic;

namespace MySite.DAL.Interfaces
{
    public interface ISongDAL
	{
        public  IList<Song> GetAllSongs();
        public IList<Song> FindSongsByAuthor(string author);
        public IList<Song> FindSongsByStyle(string style);
        public IList<Song> FindSongsByName(string name);

    }
}
