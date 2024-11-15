using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaManagement.Ultis
{
    public static class ConvertDateTime
    {
        public static string Clock(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm"); 
        }
        public static string Day(DateTime dateTime)
        {
            return dateTime.ToString("dd/mm/yyyy");
        }
        public static string Full(DateTime dateTime)
        {
            return dateTime.ToString("HH:mm:ss dd/mm/yyyy");
        }
    }
}
