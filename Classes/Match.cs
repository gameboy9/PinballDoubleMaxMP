using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballDoubleMaxMP.Classes
{
	internal class Match
	{
		public int id = 0;
		public int machineID = 0;
		public List<Player> players = new List<Player>();
		public DateTime start = new DateTime();
		public bool shadowMatch = false; // Indicates if the match is a shadow match
	}
}
