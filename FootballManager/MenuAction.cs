using System;
using System.Collections.Generic;
using System.Text;

namespace FootballManager
{
    public class MenuAction(string description, Action action)
    {
        public string Description { get; } = description;
        public Action Action { get; } = action;
    }
}
