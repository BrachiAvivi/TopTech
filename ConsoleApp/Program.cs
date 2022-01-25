using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bll;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            OpenBusinessDay algoritem = new OpenBusinessDay();
            algoritem.OpenDay();
            algoritem.mark.ToString();
        }
    }
}
