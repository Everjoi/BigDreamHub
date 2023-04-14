using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySite.BL.Implementations;
using MySite.BL.Interfaces;
using MySite.DAL.Implementations;
using MySite.Models;

namespace MySite.UI.Controllers
{
	public class SongControllers:Controller
	{
        private ISongBL _songBL;


        public SongControllers(ISongBL songBL)
        {

            //_songBL= new SongBL();
            _songBL = songBL;  
        }


        [Route("/song/songs")]
		public IActionResult Songs(int page=0)
		{
            _songBL.PageNumber = page;
            return View("/UI/Views/Song/Songs.cshtml", _songBL);         
        }


        [Route("/song/filteredsong")]
        public IActionResult FilteredSongs(string title = null,string style = null,string author = null)
        {
            if(title.IsNullOrEmpty() && style.IsNullOrEmpty() && author.IsNullOrEmpty())
            {                 
                return null; // add some extention 
            }
            else
            {
                _songBL.Filtering(_title: title,_style: style,_author: author);
            }

            return ShowFilteredSongs();
        }


        public IActionResult ShowFilteredSongs(int page=0)
        {
            _songBL.PageNumber=page;
            return View("/UI/Views/Song/SongWithFilter.cshtml",_songBL);
        }


    }
}
