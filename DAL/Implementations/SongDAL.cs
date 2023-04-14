using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySite.DAL.Interfaces;
using MySite.Models;

namespace MySite.DAL.Implementations
{
    public class SongDAL:ISongDAL
	{
		private BigDreamHdbContext _context;



        public IList<Song> GetAllSongs()
        {
            using(_context = new BigDreamHdbContext())
            {
                return _context.Songs.Include(s => s.Authors).Include(s => s.Styles).ToList();
            }
        }

        public IList<Song> FindSongsByAuthor(string author)
        {
            using(_context = new BigDreamHdbContext())
            {
                if(author.IsNullOrEmpty())
                {
                    return null;
                }
                else
                {

                    var authorFromDb = _context.Authors.Where(s => s.Name == author).FirstOrDefault()?.Name;

                    var songs = _context.Songs.Where(s => s.Authors.FirstOrDefault().Name == authorFromDb).Include(s => s.Authors).Include(s => s.Styles).ToList();

                    return songs;

                }

            }
        }


        public IList<Song> FindSongsByName(string title)
        {
            using(_context = new BigDreamHdbContext())
            {

                if(title.IsNullOrEmpty())
                {
                    return null;
                }
                else
                {
                    var songs = _context.Songs.Where(s => s.Title == title).Include(s => s.Authors).Include(s => s.Styles).ToList();
                    return songs;
                }
            }
        }

        public IList<Song> FindSongsByStyle(string style)
        {
            using(_context = new BigDreamHdbContext())
            {

                if(style.IsNullOrEmpty())
                {
                    return null;
                }
                else
                {
                    var styleFromDb = _context.Styles.Where(s => s.Name == style).FirstOrDefault()?.Name;
                    var songs = _context.Songs.Where(s => s.Styles.FirstOrDefault().Name == styleFromDb).Include(s => s.Authors).Include(s => s.Styles).Include(s => s.Authors).ToList();

                    return songs;
                }

            }

        }





    }
}
