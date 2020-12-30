using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using mangoservicetest;

namespace MangoASAservice
{
  public  class worker
    {
      public struct alertz
        {
           public string alertName;
           public DateTime alertDue;
           public string recipients;
           public string employeeName;
           public string location;
            public employee thiEmp;
        }
        public worker()
        {

        }

        public static SqlConnection returnCon()
        {
            SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=mangohrm;Integrated Security=True;Connection Timeout=5");

            try
            {
               // con.Open();
            }catch (Exception ex)
            {

            }
            return con;
                
           
        }


        public enum Logtype
        {
            LoginSuccess = 1,
            LoginFail = 2,
            RecordCreated = 3,
            RecordUpdated = 4,
            RecordDeleted = 5,
            UserLogout = 6,
            UnauthorizedAttempt = 7,
            SystemConfigUpdate = 8
        }


        public struct holidayType
        {
            public DateTime hDate;
            public bool rep;
        }

        public struct paygrade
        {
            public int ID;
            public string name;
        }

        public struct jobtitle
        {
            public int ID;
            public string title;
        }
        public struct empStatus
        {
            public int ID;
            public string status;
        }
        public struct jobCategory
        {
            public int ID;
            public string category;
        }
        public struct department
        {
            public int ID;
            public string name;
        }
        public struct location
        {
            public int ID;
            public string name;
            public string country;
            public string address;
            public string city;
            public string province;
            public string postal;
            public string phone;
            public string fax;
        }

        public struct empSalary
        {
            public int ID;
            public paygrade pay_grade;
            public string freq;
            public string currency;
            public double amount;
            public string reason;
            public DateTime recDate;
            public string comment;
            public double vehAllow;
            public double liveAllow;
            public double gasAllow;
        }

        public struct attachment
        {
            public int ID;
            public string title;
            public DateTime datein;
            public string aType;
            public string fname;
        }

        public struct supervisors
        {
            public int ID;
            public int supID;
            public string supName;
            public string method;
            public string email;
        }
        public struct subordinates
        {
            public int ID;
            public int supID;
            public string supName;
            public string method;
            public string email;
        }

        public struct workExperience
        {
            public int ID;
            public string company;
            public string jobtitle;
            public DateTime frDate;
            public DateTime toDate;
            public string comment;
        }

        public struct education
        {
            public int ID;
            public string edLevel;
            public string institution;
            public DateTime frDate;
            public DateTime toDate;
            public string edMajor;
        }

        public struct timeline
        {
            public int id;
            public string title;
            public string type;
            public DateTime datein;
            public string comment;
            public string attachname;
        }

        public struct recruitStages
        {
            public int id;
            public string stageName;
        }
        public struct leaverequest
        {
            public int id;
            public int empid;
            public int leaveid;
            public int days;
            public DateTime startd;
            public DateTime endd;
            public string status;
            public string comment;
        }

        public struct authAction
        {
            public bool authenticated;
            public int userID;
            public int roleid;
            public bool hruser;
        }

        public struct competency
        {
            public int compid;
            public string compname;
            public string descr;
            public int groupid;
        }

        public struct shareTrail
        {
            public int trailid;
            public string postBy;
            public DateTime posted;
            public string trailMessage;
        }
        public struct timesheetdata
        {
            public string clockin;
            public string clockout;
            public double hourz;
            public bool stat;
            public string empnote;
            public bool reqpending;
            public bool rejd;
            public double break_duration;
        }
    }


   
}
