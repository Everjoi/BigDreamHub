using Microsoft.AspNetCore.Mvc;

namespace MySite.UI.Controllers
{
	public class ContactsController:Controller
	{

		[Route("/contact/contacts")]
		public IActionResult Contacts()
		{
			return View("/UI/Views/Contact/Contacts.cshtml");
		}
	}
}
