using Microsoft.AspNetCore.Mvc;
using MySite.Models;

namespace MySite.UI.Controllers
{

    public class AdminController:Controller
    {


        [HttpGet]
        public IActionResult Choose()
        {
            return View("/UI/Views/Admin/Admin.cshtml");
        }



        [HttpGet]       
        public IActionResult Add()
        {
            return View("/UI/Views/Adding/Add.cshtml");
        }

        [HttpGet]
        public IActionResult Remove()
        {
            return View("/UI/Views/Removing/Remove.cshtml");
        }


        [HttpPost]
        public IActionResult AddSong([FromForm] string Title,[FromForm] string Style,[FromForm] string Author)
        {

            if(TempData["Message"] != null)
            {
                TempData.Remove("Message");
            }else
            {
                TempData["Message"] = "Something went wrong";
            }

            using(var context = new BigDreamHdbContext())
            {
                var song = new Song
                {
                    Title = Title,
                    Authors = new[] { context.Authors.Where(s => s.Name == Author).FirstOrDefault() },
                    Styles = new[] { context.Styles.Where(s => s.Name == Style).FirstOrDefault() }
                };


                context.Songs.Add(song);
                TempData["Message"] = "Song successfully added!";
                context.SaveChanges();
            }
            return RedirectToAction("Add");
        }


        [HttpPost]
        public IActionResult RemoveSong([FromForm] string Title)
        {

            if(TempData["Message"] != null)
            {
                TempData.Remove("Message");
            }
            else
            {
                TempData["Message"] = "Something went wrong";
            }

            using(var context = new BigDreamHdbContext())
            {

                foreach(var item in context.Songs)
                {
                    if(item.Title == Title)
                    {
                        context.Songs.Remove(item);
                        TempData["Message"] = "Song successfully removed!";                       
                    }
                }
                context.SaveChanges();
            }

            return RedirectToAction("Remove");
        }


    }

}
