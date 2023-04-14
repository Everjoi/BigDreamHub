using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySite.BL.Interfaces;
using MySite.DAL.Implementations;
using MySite.DAL.Interfaces;
using MySite.Models;
using System.Collections.Generic;
using System.Linq;

namespace MySite.BL.Implementations
{
    public class SongBL:ISongBL
	{
        public IList<Song> FilteredSongs { get; set; }
        public  int CountOfPage { get; set; }
        public int PageNumber { get; set; }
        
       
        const int _pageSize = 6;
        private ISongDAL _song;
        private List<Song> _filteredSongs ;

        public SongBL()
		{
			_song = new SongDAL();

            if(_song.GetAllSongs().Count % _pageSize == 0)
                CountOfPage = _song.GetAllSongs().Count / _pageSize;
            else
                CountOfPage = (int)(_song.GetAllSongs().Count / _pageSize) + 1;
        }


        public IList<Song> GetSongs()
        {
            

            var paginatedSongs = _song.GetAllSongs().Skip(PageNumber * _pageSize).Take(_pageSize);
            return paginatedSongs.ToList();

        }

        public IList<Song> GetSongsByAuthor(string author)
        {
            return _song.FindSongsByAuthor(author).ToList();
        }

        public IList<Song> GetSongsByName(string name)
        {
            return _song.FindSongsByName(name);
        }

        public IList<Song> GetSongsByStyle(string style)
        {
            return _song.FindSongsByStyle(style);
        }


        public void Filtering(string _title,string _style,string _author)
        {
            var songByName = _song.GetAllSongs().ToList();
            var songByStyle = _song.GetAllSongs().ToList();
            var songByAuthor = _song.GetAllSongs().ToList();



            if(!_title.IsNullOrEmpty())
            {
                songByName.Clear();
                songByName = GetSongsByName(_title).ToList();
            }
            if(!_style.IsNullOrEmpty())
            {
                songByStyle.Clear();
                songByStyle = GetSongsByStyle(_style).ToList();
            }
            if(!_author.IsNullOrEmpty())
            {
                songByAuthor.Clear();
                songByAuthor = GetSongsByAuthor(_author).ToList();
            }


            _filteredSongs = (from s1 in songByName
                              join s2 in songByStyle on new { s1.Id } equals new { s2.Id }
                              join s3 in songByAuthor on new { s1.Id } equals new { s3.Id }
                              select s1).ToList();



            if(_filteredSongs.Count % _pageSize == 0)
                CountOfPage = _filteredSongs.Count / _pageSize;
            else
                CountOfPage = (int)(_filteredSongs.Count / _pageSize) + 1;


            if(_title.IsNullOrEmpty() && _author.IsNullOrEmpty() && _style.IsNullOrEmpty())
            {
                FilteredSongs = null;
            }
            else
            {
                FilteredSongs = _filteredSongs;
            }
        }


        public IList<Song> GetFilteredSongs()
        {
            var paginatedSongs = FilteredSongs.Skip(PageNumber * _pageSize).Take(_pageSize);
            return paginatedSongs.ToList();
        }


    }
}



