using MangoASAservice;
using mangoservicetest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRAlertService
{
    public partial class Service1 : ServiceBase
    {
        static SqlConnection conz;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Timer t = new Timer(TimerCallback, null, 0, (1000 * 60 * 60) * 6);
           // Timer t = new Timer(TimerCallback, null, 0, (1000 * 60));
          //  WriteToFile("Running........ waiting for schedule");
        }

        protected override void OnStop()
        {
        }

        private static void TimerCallback(Object o)
        {
           WriteToFile("Last scheduled timer: " + DateTime.Now.ToString());
            // if (DateTime.Now.Hour == 9)
            {
                doAction();
            }

        }

        private static void doAction()
        {
           WriteToFile("Execute Event : HR Service : " + DateTime.Now.ToString() + "\n");
            //reset leave
            SqlCommand sqlcom = new SqlCommand();
            string result = "Successful";
            mailconfig mailC = new mailconfig();
            try
            {
                conz = utility.returnCon();

                sqlcom.Connection = conz;
                // sqlcom.CommandText = "resetEmployeeEntitlement";
                // sqlcom.CommandType = System.Data.CommandType.StoredProcedure;

                sqlcom.Connection.Open();
                // sqlcom.ExecuteNonQuery();


                // check for emp alerts
                // Console.WriteLine("ready");

                mailC.init();

                foreach (worker.alertz emAlert in utility.alertzDue())
                {
                    string txt = emAlert.alertDue.ToShortDateString() == (DateTime.Today.ToShortDateString()) ? "Today" : " in " + (emAlert.alertDue - DateTime.Today).TotalDays + " days";
                    string msg = "<span style='font-size:12pt'>This is to notify you that the below Employee alert is due " + txt + ". <br><br>";
                    msg += "Employee: <b>" + emAlert.employeeName + "</b><br>";
                    msg += "Alert: <b>" + emAlert.alertName + "</b><br>";
                    msg += "Due Date: <b>" + emAlert.alertDue.ToShortDateString() + "</b></span><br>";
                    String recipient = emAlert.recipients;
                    bool ccHR = false;
                    if (emAlert.recipients.Contains("notify-supervisor"))
                    {
                        recipient = "";ccHR = true;
                       foreach (worker.supervisors sup in emAlert.thiEmp.my_supervisors())
                        {
                            recipient += sup.email + ";";
                        }
                    }
                    WriteToFile("Alert: " + emAlert.thiEmp.fullname() + " " + emAlert.alertName + " " + emAlert.alertDue.ToShortDateString() + " :: sent to " + recipient);
                    mailC.sendMail("Employee Reminder for " + emAlert.employeeName + " - " + emAlert.alertName, msg, recipient,ccHR);
                  
                }


            }
            catch (Exception ex)
            {
                result = "Failed : " + ex.Message.ToString();
                WriteToFile(ex.Message.ToString());
            }
            finally
            {
                sqlcom.CommandType = System.Data.CommandType.Text;
                sqlcom.CommandText = "insert into tblServiceLog (result) values ('" + result + "')";
                sqlcom.ExecuteNonQuery();

                sqlcom.Connection.Close();
                // mailC.sendMail("IRAT HR-Service Event", "<br><br>Service Execution time : " + DateTime.Now.ToString() + "<br>Errors : " + result, "it-support@islandroutes.com");
            }
        }

        public static void WriteToFile(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ServiceLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }


        public static void WriteToFile_C(string Message)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ConnectivityLog_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + ".txt";
            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(Message);
                }
            }
        }

    }
}
