using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using mangoservicetest;
/// <summary>
/// Summary description for Users
/// </summary>
public class User
{

    public int ID;
    public int EmpID;
    public int active;
    public string uname;
    public string passw;
    public int roleid;
    public string rolename;
    public bool rec_notify;

    public employee associatedEmployee=new employee();

  

    public bool removeUser(int ID)
    {
        bool success = false;
        SqlCommand cmd = new SqlCommand();

        try
        { 
            cmd.CommandText = "delete from users where ID =" + ID;
            cmd.Connection =   utility.getConn();
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            success = true;
        }
        catch (Exception ex) {

            throw new Exception("Error occurred:" + ex.Message);

        } finally
        {
            cmd.Connection.Close();
        }

        return success;
    }


    public DataTable getUsers()
    {
        //List<User> users = new List<User>();
        DataSet _users = null;
        SqlDataReader rdr;

        SqlCommand cmd = new SqlCommand();
        try
        {

            cmd.CommandText = "getUserList";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Connection= utility.getConn();
            cmd.Connection.Open();
            //rdr = cmd.ExecuteReader();
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
             _users = new DataSet();
            da.Fill(_users, "users");

            /* while (rdr.Read())
             {
                 //users.Add(new User(EmpID = Convert.ToInt32(rdr["empid"]), ID = Convert.ToInt32(rdr["ID"]), roleid = Convert.ToInt32(rdr[roleid]), uname = rdr["uname"].ToString(), passw = rdr["passw"].ToString(), active = Convert.ToBoolean(rdr["active"])));
             }*/

        }
        catch (Exception ex)
        {
            throw new Exception("Error occurred:" + ex.Message);
        }
        finally {

            cmd.Connection.Close();
        }

        return _users.Tables[0];
    }

    public static List<User> hrUsers()
    {
        List<User> hrusers = new List<User>();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select * from view_users where roleid=2 or roleid=3";
        cmd.Connection = utility.getConn();
        cmd.Connection.Open();

        SqlDataReader dbread;

        dbread = cmd.ExecuteReader();

        while (dbread.Read())
        {
            User tmp = new User();
            tmp.associatedEmployee = new employee(Convert.ToInt32(dbread["empid"]));
            hrusers.Add(tmp);
        }

        return hrusers;
    }

    public static List<User> hrAssociates()
    {
        List<User> hrusers = new List<User>();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select * from view_users where roleid=1";
        cmd.Connection = utility.getConn();
        cmd.Connection.Open();

        SqlDataReader dbread;

        dbread = cmd.ExecuteReader();

        while (dbread.Read())
        {
            User tmp = new User();
            tmp.associatedEmployee = new employee(Convert.ToInt32(dbread["empid"]));
            hrusers.Add(tmp);
        }

        return hrusers;
    }

    public static List<User> rec_notify_users()
    {
        List<User> hrusers = new List<User>();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "select * from view_users where rec_notify='true'";
        cmd.Connection = utility.getConn();
        cmd.Connection.Open();

        SqlDataReader dbread;

        dbread = cmd.ExecuteReader();

        while (dbread.Read())
        {
            User tmp = new User();
            tmp.associatedEmployee = new employee(Convert.ToInt32(dbread["empid"]));
            hrusers.Add(tmp);
        }

        return hrusers;
    }



    public User(int _empid,int _ID,int _roleid, string _uname,string _passw,int _active,string _rolename)
    {
        roleid = _roleid;
         ID = _ID;
         EmpID = _empid;
         active = _active;
         uname = _uname;
         passw = _passw;
         rolename = _rolename;
    }

    public User()
    {
        // TODO: Add constructor logic here
    }


}