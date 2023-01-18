using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace _4._6Lognet4WebApplication.Models
{
    public static class LogManager
    {
        private static ILog log = log4net.LogManager.GetLogger("logeeeeeeer");

        public static ILog Log
        {
            get { return log; }
        }
    }
}