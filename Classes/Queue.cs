using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballDoubleMaxMP.Classes
{
	internal class Queue
	{
		public List<Player> players = new List<Player>();

		public Queue()
		{
			players = new List<Player>();
		}

		public void AddPlayer(Player player)
		{
			players.Add(player);
		}

		public bool RemovePlayer(Player player)
		{
			return players.Remove(player);
		}
	}
}
