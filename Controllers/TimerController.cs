using Microsoft.AspNetCore.Mvc;
using System;
using System.Timers;

namespace ExtremeRecycler.Controllers
{
	public class TimerController : Controller
	{
		private System.Timers.Timer timer;

		public TimerController()
		{
			// Create a new timer with a 1 second interval
			timer = new System.Timers.Timer(1000); // Interval is in milliseconds
			timer.Elapsed += TimerElapsed; // Hook up the Elapsed event
			timer.AutoReset = true; // AutoReset to true means it will fire continuously
			timer.Enabled = true; // Start the timer
		}

		private void TimerElapsed(object sender, ElapsedEventArgs e)
		{
			//Console.WriteLine("Timer Elapsed!!!!");
			// This method will be called every time the timer elapses
			// You can perform any action you want here
			// For example, log something, send an email, etc.
		}
	}
}
