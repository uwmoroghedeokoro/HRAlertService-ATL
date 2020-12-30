using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MangoASAservice;
using mangoservicetest;

/// <summary>
/// Summary description for employee
/// </summary>
public class employee
{

    public int recid;
    public string empid;
    public string fname;
    public string mname;
    public string lname;
    public string nis;
    public string trn;
    public string licenseno;
    public DateTime licenseExp;
    public string gender;
    public string marital;
    public DateTime dob;
    public string nationality;
    public string address;
    public string city;
    public string country;
    public string mobile;
    public string hometel;
    public string worktel;
    public string emailwork;
    public string emailother;
    public bool active;
    public string photo;
    public bool canLogin;
    public bool approveTime;
    public bool approveLeave;
    public bool terminated;
    public int locationid;
    public int salCount;
    public int canid;
    public double csalary;
    public bool chkUniform;
    public bool chkTag;
    public bool chkProxi;
    public bool chkID;
    public bool chkPolice;
    public string payCycle {
        get
        {
            return _get_pay_cycle();
        }
        set
        {
            payCycle = value;
        }
    }

    public job myJob = new job();
    
    public List<worker.subordinates> mySubordinate = new List<worker.subordinates>();
    public List<worker.supervisors> mySupervisors = new List<worker.supervisors>();
    
     

    public string fullname()
    {
        return fname + " " + lname;
    }
    
    public employee()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public employee(int idz)
    {
        recid = idz;
        SqlCommand cmd = new SqlCommand();
        try
        {
        cmd.Connection = utility.getConn();
                cmd.Connection.Open();

                cmd.CommandText = "select * from view_employees where id=" + recid;

                SqlDataReader dbread = cmd.ExecuteReader();
                while (dbread.Read())
                {
                    
                    fname = (string)dbread["fname"];
                    mname = (string)dbread["mname"];
                    lname = (string)dbread["lname"];
                    nis = (string)dbread["nis"];
                    trn = (string)dbread["trn"];
                    licenseno = (string)dbread["licenseno"];
                    licenseExp = Convert.ToDateTime(dbread["licenseExp"]);
                    gender = (string)dbread["gender"];
                    marital = (string)dbread["marital"];
                    dob = (DateTime)dbread["dob"];
                    nationality = (string)dbread["nationality"];
                    address = (string)dbread["address"];
                    city = (string)dbread["city"];
                    country = (string)dbread["country"];
                    mobile = (string)dbread["mobile"];
                    hometel = (string)dbread["hometel"];
                    worktel = (string)dbread["worktel"];
                    emailother = (string)dbread["emailother"];
                    emailwork = (string)dbread["emailwork"];
                    empid = (string)dbread["empid"];
                    photo = (string)dbread["photo"];
                    canLogin = Convert.ToBoolean(dbread["canLogin"]);
                    approveLeave = Convert.ToBoolean(dbread["approveLeave"]);
                    approveTime = Convert.ToBoolean(dbread["approveTime"]);
                    active = Convert.ToBoolean(dbread["active"]);
                    terminated = Convert.ToBoolean(dbread["terminated"]);
                    locationid = dbread["location"] == DBNull.Value?-1:Convert.ToInt16(dbread["location"]);
                    myJob.edepartment.ID= dbread["deptid"]==System.DBNull.Value?0: Convert.ToInt32(dbread["deptid"]);
                    csalary = 0.0;// Convert.ToDouble(dbread["csalary"]);
             //   payCycle = Convert.ToString(dbread["payFreq"]);
                chkUniform = Convert.ToBoolean(dbread["chkUniform"]);
                chkTag = Convert.ToBoolean(dbread["chkTag"]);
                chkID = Convert.ToBoolean(dbread["chkID"]);
                chkProxi = Convert.ToBoolean(dbread["chkProxi"]);
                chkPolice = Convert.ToBoolean(dbread["chkPolice"]);
            }
                dbread.Close();
       

        }
        finally
        {
            cmd.Connection.Close();
        }
    }


    public List<worker.subordinates> my_subordinates()
    {
        List<worker.subordinates> subs = new List<worker.subordinates>();
        

             SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from view_EmpOrg where supid=" + recid;

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                worker.subordinates sub = new worker.subordinates();
                sub.ID = Convert.ToInt32(dbread["subID"]);
                sub.supName = Convert.ToString(dbread["subName"]);
                subs.Add(sub);
            }
            dbread.Close();
           
        }
        catch (Exception ex)
        {

        }finally
        {
            cmd.Connection.Dispose();
          
        }
        return subs;

    }


    public List<worker.supervisors> my_supervisors()
    {
        List<worker.supervisors> supes = new List<worker.supervisors>();


        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from view_EmpOrg where subid=" + recid;

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                worker.supervisors sub = new worker.supervisors();
                sub.ID = Convert.ToInt32(dbread["supid"]);
                sub.supName = Convert.ToString(dbread["supName"]);
                sub.email = Convert.ToString(dbread["supEmail"]);
                supes.Add(sub);
            }
            dbread.Close();

        }
      
        finally
        {
            cmd.Connection.Dispose();

        }
        return supes;

    }

    public String _get_pay_cycle()
    {
        String pc = "";
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select top 1 payFreq from tblEmpSalary where empid=" + recid + " order by id DESC";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                pc = Convert.ToString(dbread["payFreq"]);
            }
            dbread.Close();

        }
       
        finally
        {
            cmd.Connection.Dispose();

        }

        return pc;
    }

  

  
    private int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    public employee(int empid,bool empd)
    {
       // recid = idz;
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from view_employees where empid=" + empid;

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                recid = Convert.ToInt32(dbread["id"]);
                fname = (string)dbread["fname"];
                mname = (string)dbread["mname"];
                lname = (string)dbread["lname"];
                nis = (string)dbread["nis"];
                trn = (string)dbread["trn"];
                licenseno = (string)dbread["licenseno"];
                licenseExp = Convert.ToDateTime(dbread["licenseExp"]);
                gender = (string)dbread["gender"];
                marital = (string)dbread["marital"];
                dob = (DateTime)dbread["dob"];
                nationality = (string)dbread["nationality"];
                address = (string)dbread["address"];
                city = (string)dbread["city"];
                country = (string)dbread["country"];
                mobile = (string)dbread["mobile"];
                hometel = (string)dbread["hometel"];
                worktel = (string)dbread["worktel"];
                emailother = (string)dbread["emailother"];
                emailwork = (string)dbread["emailwork"];
               // empid = (string)dbread["empid"];
                photo = (string)dbread["photo"];
                canLogin = Convert.ToBoolean(dbread["canLogin"]);
                approveLeave = Convert.ToBoolean(dbread["approveLeave"]);
                approveTime = Convert.ToBoolean(dbread["approveTime"]);
                active = Convert.ToBoolean(dbread["active"]);
                terminated = Convert.ToBoolean(dbread["terminated"]);
                locationid = dbread["location"] == DBNull.Value ? -1 : Convert.ToInt16(dbread["location"]);
                myJob.edepartment.ID = dbread["deptid"] == System.DBNull.Value ? 0 : Convert.ToInt32(dbread["deptid"]);
                csalary = 0.0;//Convert.ToDouble(dbread["csalary"]);
                chkUniform = Convert.ToBoolean(dbread["chkUniform"]);
                chkTag = Convert.ToBoolean(dbread["chkTag"]);
                chkID = Convert.ToBoolean(dbread["chkID"]);
                chkProxi = Convert.ToBoolean(dbread["chkProxi"]);
                chkPolice = Convert.ToBoolean(dbread["chkPolice"]);
            }
            dbread.Close();


        }
        finally
        {
            cmd.Connection.Close();
        }
    }

   
   
    public job jobInfo()
    {
        job jInfo = new job();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from view_empJobDetails where empid=" + recid;

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                jInfo.eCategory.category = dbread["category"] == null ? "-" : dbread["category"].ToString();
                jInfo.empStatus.status = dbread["empStat"] == null ? "-" : dbread["empStat"].ToString();
                jInfo.edepartment.name = dbread["department"] == null ? "-" : dbread["department"].ToString();
                jInfo.edepartment.ID = ((dbread["deptid"]) != System.DBNull.Value) ? Convert.ToInt32(dbread["deptid"]) : 0;
                jInfo.startDate = dbread["jobStart"].ToString();
                jInfo.joindate = Convert.ToDateTime(dbread["joindate"]);
                jInfo.jTitle.ID =  ((dbread["jobtitleid"])!=System.DBNull.Value)? Convert.ToInt32(dbread["jobtitleid"]):0;
                jInfo.endDate = dbread["jobEnd"].ToString();
                jInfo.jTitle.title = dbread["jobtitle"] == null ? "-" : dbread["jobtitle"].ToString();
                jInfo.elocation.name = dbread["locName"].ToString();
                jInfo.elocation.country = dbread["country"].ToString();
                jInfo.origHireDate = dbread["origJoinDate"]==null ? "":dbread["origJoinDate"].ToString();
                // active = Convert.ToBoolean(dbread["active"]);

            }
            dbread.Close();


        }
        finally
        {
            cmd.Connection.Close();
           
        }
        return jInfo;
    }

    public void updateBF(string leaveType, string balDays)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "updateBF";
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@leaveType", leaveType);
            cmd.Parameters.AddWithValue("@bf", balDays);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public void adjustLeaveBal(string leaveType, string balDays)
    {
        SqlCommand cmd = new SqlCommand();

       try
        {
            cmd.CommandText = "adjustEmpEntitlement";
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@leavetypeid", leaveType);
            cmd.Parameters.AddWithValue("@days", balDays);

            cmd.ExecuteNonQuery();
        }
        finally
        {
            cmd.Connection.Close();
        }
    }
    public void addRec(string vbalDays,string sbalDays)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveEmp";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@mname", mname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@nis", nis);
            cmd.Parameters.AddWithValue("@trn", trn);
            cmd.Parameters.AddWithValue("@recid", recid);
            cmd.Parameters.AddWithValue("@licenseno", licenseno);
            cmd.Parameters.AddWithValue("@licenseExp", licenseExp);
            cmd.Parameters.AddWithValue("@gender", gender);
            cmd.Parameters.AddWithValue("@marital", marital);
            cmd.Parameters.AddWithValue("@dob", dob);
            cmd.Parameters.AddWithValue("@nationality", nationality);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.Parameters.AddWithValue("@country", country);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            cmd.Parameters.AddWithValue("@hometel", hometel);
            cmd.Parameters.AddWithValue("@worktel", worktel);
            cmd.Parameters.AddWithValue("@emailwork", emailwork);
            cmd.Parameters.AddWithValue("@emailother", emailother);
            cmd.Parameters.AddWithValue("@active", active);
            cmd.Parameters.AddWithValue("@balDays", vbalDays);
            cmd.Parameters.AddWithValue("@canid", canid);
            cmd.Parameters.AddWithValue("@sbalDays", sbalDays);
            SqlParameter param=new SqlParameter();
            param.ParameterName = "ret";
            
            param.Direction=System.Data.ParameterDirection.ReturnValue;
            cmd.Parameters.Add(param);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

            recid = (int)cmd.Parameters["ret"].Value;
        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }
    public void addJobDetails()
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveEmpJobDetails";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@jobtitle", myJob.jTitle.ID);
            cmd.Parameters.AddWithValue("@empStatus", myJob.empStatus.ID);
            cmd.Parameters.AddWithValue("@jobCategory", myJob.eCategory.ID);
            cmd.Parameters.AddWithValue("@department", myJob.edepartment.ID);
            cmd.Parameters.AddWithValue("@location", myJob.elocation.ID);
            cmd.Parameters.AddWithValue("@jobStart", myJob.startDate);
            cmd.Parameters.AddWithValue("@jobEnd", myJob.endDate);
            cmd.Parameters.AddWithValue("@joindate", myJob.joindate);
            cmd.Parameters.AddWithValue("@origHireDate", myJob.origHireDate);

            //    cmd.Parameters.AddWithValue("@chkUniform", chkUniform);
            //   cmd.Parameters.AddWithValue("@chkTag", chkTag);
            //   cmd.Parameters.AddWithValue("@chkProxi", chkProxi);
            //  cmd.Parameters.AddWithValue("@chkID", chkID);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

         }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }
    public int NoSalaryInfo()
    {
        int noSal = 0;

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select count(*) as cnt from view_employees where joindate ='1/1/1900'";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                noSal = Convert.ToInt16(dbread["cnt"]);
            }


        }
        finally
        {

        }

        return noSal;
    }
    public List<employee> employeesNOjoinDate()
    {
        List<employee> noJoinDate=new List<employee>();

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select id,fname,lname from view_employees where joindate ='1/1/1900'";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                employee tmp = new employee();
                tmp.recid = Convert.ToInt32(dbread["id"]);
                tmp.fname = (string)dbread["fname"];
                tmp.lname = (string)dbread["lname"];
                noJoinDate.Add(tmp);
            }


        }
        finally
        {

        }

        return noJoinDate;
    }
    public int employeesNOsalary()
    {
       int noSalary =0;

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select count(*) as cnt from view_employees tb1 where department='-' OR locName='-' OR jobtitle='-' OR joinDate='1/1/1900' OR  id not in (select empid from tblEmpSalary)";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                noSalary = Convert.ToInt32(dbread["cnt"]);
            }


        }
        finally
        {

        }

        return noSalary;
    }

    public List<employee> employeesMissingsalary()
    {
        List<employee> noSalary = new List<employee>();

        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select ID,fname,lname,department,locName,jobtitle,joinDate,(select count(empid) from tblEmpSalary tb2 where tb2.empid=tb1.ID) as salCount from view_employees tb1 where department='-' OR locName='-' OR jobtitle='-' OR joinDate='1/1/1900' OR  id not in (select empid from tblEmpSalary)";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                employee emp = new employee();
                emp.recid = Convert.ToInt32(dbread["ID"]);
                emp.salCount = Convert.ToInt32(dbread["salCount"]);
                emp.fname = (string)dbread["fname"];
                emp.lname = (string)dbread["lname"];
                emp.myJob.edepartment.name = (string)dbread["department"];
                emp.myJob.elocation.name = (string)dbread["locName"];
                emp.myJob.jTitle.title  = (string)dbread["jobtitle"];
                emp.myJob.joindate  = Convert.ToDateTime(dbread["joinDate"]);
                noSalary.Add(emp);
            }


        }
        finally
        {

        }

        return noSalary;
    }

    public List<worker.workExperience>workHistory()
    {
        List<worker.workExperience> wHistory = new List<worker.workExperience>();
         SqlCommand cmd = new SqlCommand();
         try
         {
             cmd.Connection = utility.getConn();
             cmd.Connection.Open();

             cmd.CommandText = "select * from tblEmpWorkExp where empid=" + recid + " order by frDate DESC";

             SqlDataReader dbread = cmd.ExecuteReader();
             while (dbread.Read())
             {
                 worker.workExperience tmp = new worker.workExperience();
                 tmp.ID = Convert.ToInt32(dbread["ID"]);
                 tmp.company = Convert.ToString(dbread["company"]);
                 tmp.jobtitle = Convert.ToString(dbread["jobtitle"]);
                 tmp.frDate = Convert.ToDateTime(dbread["frDate"]);
                 tmp.toDate= Convert.ToDateTime(dbread["toDate"]);
                 tmp.comment = Convert.ToString(dbread["comment"]);

                 wHistory.Add(tmp);
             }
             dbread.Close();
         }
         finally
         {
             cmd.Connection.Close();
         }
         return wHistory;
    }
    public List<worker.attachment> attachments()
    {
        List<worker.attachment> mAttachments = new List<worker.attachment>();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from tblEmpAttachments where empid=" + recid + " order by datetin DESC";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                worker.attachment tmp = new worker.attachment();
                tmp.ID = Convert.ToInt32(dbread["ID"]);
                tmp.title = Convert.ToString(dbread["aTitle"]);
                tmp.aType = Convert.ToString(dbread["aType"]);
                tmp.datein = Convert.ToDateTime(dbread["datetin"]);
              
                tmp.fname = Convert.ToString(dbread["filename"]);

                mAttachments.Add(tmp);
            }
            dbread.Close();
        }
        finally
        {
            cmd.Connection.Close();
        }
        return mAttachments;
    }
    public List<worker.education> eduHistory()
    {
        List<worker.education> eHistory = new List<worker.education>();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from tblEmpEdu where empid=" + recid + " order by frDate DESC";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                worker.education tmp = new worker.education();
                tmp.ID = Convert.ToInt32(dbread["ID"]);
                tmp.edLevel = Convert.ToString(dbread["edLevel"]);
                tmp.institution = Convert.ToString(dbread["institution"]);
                tmp.frDate = Convert.ToDateTime(dbread["frDate"]);
                tmp.toDate = Convert.ToDateTime(dbread["toDate"]);
                tmp.edMajor = Convert.ToString(dbread["edMajor"]);

                eHistory.Add(tmp);
            }
            dbread.Close();
        }
        finally
        {
            cmd.Connection.Close();
        }
        return eHistory;
    }

    public List<worker.empSalary>salaryHistory()
    {
        List<worker.empSalary> salHistory = new List<worker.empSalary>();
        SqlCommand cmd = new SqlCommand();
        try
        {
            cmd.Connection = utility.getConn();
            cmd.Connection.Open();

            cmd.CommandText = "select * from view_empSalary where empid=" + recid + " order by ID DESC";

            SqlDataReader dbread = cmd.ExecuteReader();
            while (dbread.Read())
            {
                worker.empSalary tmp = new worker.empSalary();
                tmp.ID = Convert.ToInt32(dbread["ID"]);
                tmp.amount = Convert.ToDouble(dbread["payamt"]);
               tmp.comment = Convert.ToString(dbread["comment"]);
               tmp.currency = Convert.ToString(dbread["currency"]);
               tmp.freq = Convert.ToString(dbread["payfreq"]);
               tmp.pay_grade.name = Convert.ToString(dbread["paygrade"]);
               tmp.reason = Convert.ToString(dbread["reason"]);
               tmp.recDate = Convert.ToDateTime(dbread["datein"]);
              // tmp = Convert.ToString(dbread["reason"]);
                // active = Convert.ToBoolean(dbread["active"]);
               salHistory.Add(tmp);
            }
            dbread.Close();
      }
        finally
        {
            cmd.Connection.Close();

        }
        return salHistory;
    }

    public void addSalaryDetails(worker.empSalary eSalary)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveEmpSalaryDetails";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", eSalary.ID);
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@paygradeid", eSalary.pay_grade.ID);
            cmd.Parameters.AddWithValue("@freq", eSalary.freq);
            cmd.Parameters.AddWithValue("@currency", eSalary.currency);
            cmd.Parameters.AddWithValue("@amount", eSalary.amount);
            cmd.Parameters.AddWithValue("@reason",eSalary.reason);
            cmd.Parameters.AddWithValue("@recDate", eSalary.recDate);
            cmd.Parameters.AddWithValue("@comment", eSalary.comment);

            cmd.Parameters.AddWithValue("@vehAllow", eSalary.vehAllow);
            cmd.Parameters.AddWithValue("@liveAllow", eSalary.liveAllow);
            cmd.Parameters.AddWithValue("@gasAllow", eSalary.gasAllow);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }

    public void addWorkExp(worker.workExperience workExp)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveWorkExp";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", workExp.ID);
            cmd.Parameters.AddWithValue("@company", workExp.company);
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@jobtitle", workExp.jobtitle);
            cmd.Parameters.AddWithValue("@frDate", workExp.frDate);
            cmd.Parameters.AddWithValue("@toDate", workExp.toDate);
            cmd.Parameters.AddWithValue("@comment", workExp.comment);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }

    public void addEdu(worker.education eduExp)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveEdu";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", eduExp.ID);
            cmd.Parameters.AddWithValue("@edLevel", eduExp.edLevel);
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@institution", eduExp.institution);
            cmd.Parameters.AddWithValue("@frDate", eduExp.frDate);
            cmd.Parameters.AddWithValue("@toDate", eduExp.toDate);
            cmd.Parameters.AddWithValue("@edMajor", eduExp.edMajor);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }

    public void addTimeline(worker.timeline tmline)
    {
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "saveTimeline";
            cmd.Connection = utility.getConn();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", tmline.id);
            cmd.Parameters.AddWithValue("@title", tmline.title);
            cmd.Parameters.AddWithValue("@empid", recid);
            cmd.Parameters.AddWithValue("@type", tmline.type);
            cmd.Parameters.AddWithValue("@datein", tmline.datein);
            cmd.Parameters.AddWithValue("@comment", tmline.comment);
            cmd.Parameters.AddWithValue("@attachname", tmline.attachname);

            cmd.Connection.Open();
            cmd.ExecuteNonQuery();

        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally
        {

            cmd.Connection.Close();
        }
    }

   public struct job
    {
     
       public worker.jobtitle jTitle;
       public worker.empStatus empStatus;
       public worker.jobCategory eCategory;
       public worker.department edepartment;
       public worker.location elocation;
       public String startDate;
       public String endDate;
        public DateTime joindate;
        public String origHireDate;
        public String payCycle;
    }

   
   
}

