using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class User
{
    string UserName = string.Empty;
    string FirstName=string.Empty;
    string LastName = string.Empty;
    string EmailID = string.Empty;
    string Designation = string.Empty;
}
/// <summary>
/// Summary description for UserDatasource
/// </summary>
public class UserDatasource
{
	public UserDatasource()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public  void GetUserDetails()
    {

    }
}