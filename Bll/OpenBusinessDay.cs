using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;

namespace Bll
{
    public class OpenBusinessDay
    {
        //צריך משהוא מלמעלה שמנהל את הענינים
        //פונקציה זו מופעלת כל יום בשעה מסוימת
        //BusinessDay BusinessDay = new BusinessDay();
        //רשימה של קריאות שלא טופלו
        List<Call> calls = new List<Call>();

        public BusinessDay BusinessDay { get; set; }

        List<Destination> destinations;
        public void CreateDestinations()
        {
            destinations = new List<Destination>();
            foreach (Call item in calls)
            {
                

                //init list of destinations
            }
        }
        




    }


}
