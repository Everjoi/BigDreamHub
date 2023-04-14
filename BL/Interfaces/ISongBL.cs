using MySite.Models;


namespace MySite.BL.Interfaces
{
    public interface ISongBL
	{
		public int PageNumber { get; set; }


		public IList<Song> GetSongs();
		public IList<Song> GetSongsByAuthor(string author);
		public IList<Song> GetSongsByStyle(string style);
		public IList<Song> GetSongsByName(string name);
		public void Filtering(string _title,string _style,string _author);
		public IList<Song> GetFilteredSongs();

    }
}
