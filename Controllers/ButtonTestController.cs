using ExtremeRecycler.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExtremeRecycler.Controllers
{
	public class ButtonTestController : Controller
	{
		static List<ButtonModel> buttons = new List<ButtonModel>();
		Random random = new Random();

		public ButtonTestController()
		{
			if (buttons.Count == 0)
			{
				for (int i = 0; i < 25; i++)
				{
					if (random.Next(10) > 5) buttons.Add(new ButtonModel(true, false));
					else buttons.Add(new ButtonModel(false, false));
				}
				buttons[0].Flagged = true;
			}
		}

		public IActionResult Index()
		{
			return View("ButtonTest", buttons);
		}

		public ActionResult OnButtonClick(string toggle)
		{
			int buttonNumber = Int32.Parse(toggle);

			if (!buttons[buttonNumber].Flagged)
			{
				buttons[buttonNumber].State = !buttons[buttonNumber].State;
			}

			return View("ButtonTest", buttons);
		}

		public ActionResult OnRightButtonClick(string toggle)
		{
			int buttonNumber = Int32.Parse(toggle);
			buttons[buttonNumber].Flagged = !buttons[buttonNumber].Flagged;
			return View("ButtonTest", buttons);
		}
	}
}
