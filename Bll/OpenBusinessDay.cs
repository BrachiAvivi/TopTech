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
        ClsDB db;
        List<Call> calls;
        Company company;

        public OpenBusinessDay()
        {
            db = ClsDB.Instance;
            
        }


        
        public void CreateDestinations()
        {
            destinations = new List<Destination>();
            foreach (Call item in calls)
            {
                

                //init list of destinations
            }
        }
        
        public void OpenDay()
        {
            //opening new day to save in database
            BusinessDay day = new BusinessDay();
            //all the destinations that can by chosen today
            List<Destination> destinations = db.GetDestinations();

        }



    }


}
