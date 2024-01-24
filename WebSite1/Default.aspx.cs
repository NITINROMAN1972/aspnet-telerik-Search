using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class _Default : System.Web.UI.Page
{

    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindCustomersAndGrid();
            BindDropDownCountry();
            //Radgrid1.Visible = false;

        }
       
    }

    protected void Radgrid1_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        BindCustomersAndGrid();
        

    }

    protected void Radgrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }

    protected void RadGrid1_ItemCreated(object sender, GridItemEventArgs e)
    {
        // Your event handler code here
    }



    //The Grid will change when the button is being clicked
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Radgrid1.Visible = true;
        BindCustomersAndGrid();
        Radgrid1.DataBind(); // Rebind the grid data
    }

    //Populating dropdown values

    private void BindDropDownCountry()
    {
        // connection string  
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            using (SqlCommand com = new SqlCommand("SELECT CountryID, CountryName FROM Country", con))
            {
                // table name   
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);  // fill dataset  
                ddlCountry.DataSource = dt;      //assigning datasource to the dropdownlist 
                ddlCountry.DataTextField = "CountryName";
                ddlCountry.DataValueField = "CountryID";
                ddlCountry.DataBind();  //binding dropdownlist 
                ddlCountry.Items.Insert(0, new ListItem("--select--", ""));
                con.Close();
            }
        }

    }


    private void BindDropDownState(string _StrCountry)
    {
      //  string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //con.Open();
            SqlCommand com = new SqlCommand("SELECT StateID, StateName FROM State  where countryid='"+_StrCountry +"'", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddlState.DataSource = ds.Tables[0]; // Assigning datasource to the dropdownlist  
            ddlState.DataTextField = "StateName"; // Text field name of table displayed in the dropdown       
            ddlState.DataValueField = "StateID";  // To retrieve the specific textfield name   
            ddlState.DataBind(); // Binding dropdownlist 
            ddlState.Items.Insert(0, new ListItem("--select--", ""));
            //con.Close();
        }
    }

    private void BindDropDownCity(string _StrState)
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
        // connection string  
        using (SqlConnection con = new SqlConnection(connectionString))
        {
           // con.Open();
            SqlCommand com = new SqlCommand("SELECT CityID, CityName FROM City where Stateid='" + _StrState + "'", con);
            // table name   
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);  // fill dataset  
            ddlCity.DataSource = ds.Tables[0]; // Assigning datasource to the dropdownlist  
            ddlCity.DataTextField = "CityName"; // Text field name of table displayed in the dropdown       
            ddlCity.DataValueField = "CityID";  // To retrieve the specific textfield name   
            ddlCity.DataBind(); // Binding dropdownlist 
            ddlCity.Items.Insert(0, new ListItem("--select--", ""));
            //con.Close();
        }
    }

    private void BindCustomersAndGrid()
    {
        // Retrieve connection string from Web.config
        string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string sql = @"SELECT E.EmployeeID,   
    E.FirstName,
    E.LastName,
    C.CountryName,
    S.StateName,
    CI.CityName,
    E.BirthDate,
    E.JobTitle,
    E.Salary,
    E.Date
FROM Employees E
JOIN Country C ON E.CountryID = C.CountryID
JOIN State S ON E.StateID = S.StateID
JOIN City CI ON E.CityID = CI.CityID WHERE 1 = 1";

            // Check if Employee ID is provided for filtering
            //if (!string.IsNullOrEmpty(TxtEMPID.Text.Trim()))
            //{
            //    sql += " AND EmployeeID = @EmployeeID";
            //}           
            // Check if a value is selected in the dropdown
            if (ddlCountry .SelectedIndex > 0)
            {
                sql += " AND CountryName = @DropdownCountry";
            }

            if (ddlState.SelectedIndex > 0)
            {
                sql += " AND StateName = @DropdownState";
            }

            if (ddlCity.SelectedIndex > 0)
            {
                sql += " AND CityName = @DropdownCity";
            }

            if (!string.IsNullOrEmpty(txtFromDate.Value))
            {
                sql += " AND CONVERT(DATE, E.[Date]) >= @FromDate";
            }

            // Check if To Date is provided for filtering
            if (!string.IsNullOrEmpty(txtToDate.Value))
            {
                sql += " AND CONVERT(DATE, E.[Date]) <= @ToDate";
            }


            SqlCommand cmd = new SqlCommand(sql, con);

            // Add parameters for Employee ID filtering
            //if (!string.IsNullOrEmpty(TxtEMPID.Text.Trim()))
            //{
            //    cmd.Parameters.AddWithValue("@EmployeeID", TxtEMPID.Text.Trim());
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
            //}

            // Add parameters for Dropdown filtering
            if (ddlCountry .SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@DropdownCountry", ddlCountry .SelectedItem.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DropdownCountry", DBNull.Value);
            }


            if (ddlState.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@DropdownState", ddlState.SelectedItem.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DropdownState", DBNull.Value);
            }

            if (ddlCity.SelectedIndex > 0)
            {
                cmd.Parameters.AddWithValue("@DropdownCity", ddlCity.SelectedItem.Text);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DropdownCity", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(txtFromDate.Value))
            {
                cmd.Parameters.AddWithValue("@FromDate", Convert.ToDateTime(txtFromDate.Value));
            }
            else
            {
                cmd.Parameters.AddWithValue("@FromDate", DBNull.Value);
            }

            if (!string.IsNullOrEmpty(txtToDate.Value))
            {
                cmd.Parameters.AddWithValue("@ToDate", Convert.ToDateTime(txtToDate.Value));
            }
            else
            {
                cmd.Parameters.AddWithValue("@ToDate", DBNull.Value);
            }
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            con.Close();

            Radgrid1.DataSource = dt;
            // Set the current page index to the first page
            //Radgrid1.DataBind();
        }
    }

    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    BindCustomersAndGrid();
    //}



    //private void BindCustomers()
    //{
    //    // Retrieve connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employee WHERE (@FirstName IS NULL OR FirstName = @FirstName)", con))
    //        {
    //            cmd.Parameters.AddWithValue("@FirstName", string.IsNullOrEmpty(DropDownList1.SelectedValue) ? (object)DBNull.Value : DropDownList1.SelectedValue);

    //            con.Open();
    //            SqlDataReader reader = cmd.ExecuteReader();

    //            Radgrid1.DataSource = reader;
    //            Radgrid1.DataBind();

    //            con.Close();
    //        }
    //    }
    //}



    //private void BindGrid()
    //{
    //    // Retrieve connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();
    //        string sql = "SELECT * FROM Employee WHERE 1 = 1";

    //        // Check if Employee ID is provided for filtering
    //        if (!string.IsNullOrEmpty(TxtEMPID.Text.Trim()))
    //        {
    //            sql += " AND  EmployeeID = @EmployeeID";
    //        }

    //        if (!string.IsNullOrEmpty(TxtFirstName.Text.Trim()))
    //        {
    //            sql += " AND FirstName LIKE @FirstName";
    //        }

    //        SqlCommand cmd = new SqlCommand(sql, con);

    //        // Add parameters for Employee ID filtering
    //        if (!string.IsNullOrEmpty(TxtEMPID.Text.Trim()))
    //        {
    //            cmd.Parameters.AddWithValue("@EmployeeID", TxtEMPID.Text.Trim());
    //        }


    //        // Add parameters for First Name filtering
    //        if (!string.IsNullOrEmpty(TxtFirstName.Text.Trim()))
    //        {
    //            cmd.Parameters.AddWithValue("@FirstName", "%" + TxtFirstName.Text.Trim() + "%");
    //        }

    //        SqlDataAdapter ad = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        ad.Fill(dt);
    //        con.Close();

    //        Radgrid1.DataSource = dt;
    //        // Set the current page index to the first page
    //        Radgrid1.DataBind();

    //    }
    //}



    //private void BindGrid()
    //{
    //    // Retrieve connection string from Web.config
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();
    //        string sql = "SELECT * FROM Employee";
    //        SqlCommand cmd = new SqlCommand(sql, con);

    //        SqlDataAdapter ad = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();
    //        ad.Fill(dt);
    //        con.Close();

    //        Radgrid1.DataSource = dt;
    //    }
    //}





    //protected void Radgrid1_InsertCommand(object sender, GridCommandEventArgs e)
    //{
    //    GridEditableItem editedItem = e.Item as GridEditableItem;
    //    Hashtable newValues = new Hashtable();
    //    editedItem.ExtractValues(newValues);
    //    string employeeIDString = newValues["EmployeeID"].ToString();

    //    if (int.TryParse(employeeIDString, out int employeeID))
    //    {
    //        // The conversion was successful, and 'employeeID' now contains the integer value.
    //        // You can use 'employeeID' in your code.
    //    }
    //    else
    //    {
    //        // Handle the case where the conversion failed. Log an error, set a default value, etc.
    //    }
    //    string employeeFirstName = newValues["FirstName"].ToString();
    //    string employeeLastName = newValues["LastName"].ToString();
    //    string EmployeeSalary = newValues["Salary"].ToString();
    //    string hireDateString = newValues["HireDate"].ToString();

    //    if (DateTime.TryParse(hireDateString, out DateTime HiringDate))
    //    {
    //        // The conversion was successful, and 'hiringDate' now contains the DateTime value.
    //        // You can use 'hiringDate' in your code.
    //    }
    //    else
    //    {
    //        // Handle the case where the conversion failed. Log an error, set a default value, etc.
    //    }
    //    string Email = newValues["Email"].ToString();
    //    string Department = newValues["Department"].ToString();
    //    string birthDateString = newValues["BirthDate"].ToString();

    //    if (DateTime.TryParse(birthDateString, out DateTime birthDate))
    //    {
    //        // The conversion was successful, and 'birthDate' now contains the DateTime value.
    //        // You can use 'birthDate' in your code.
    //    }
    //    else
    //    {
    //        // Handle the case where the conversion failed. Log an error, set a default value, etc.
    //    }

    //    string Address = newValues["Address"].ToString();
    //    // ... Extract other values similarly

    //    // Insert the new record into the database
    //    InsertEmployee(employeeIDString, employeeFirstName, employeeLastName, EmployeeSalary, HiringDate, Email, Department, birthDate, Address  /*, ... other parameters */);

    //    // Rebind the RadGrid to refresh the data
    //    Radgrid1.Rebind();
    //    //BindGrid();
    //}

    //private void InsertEmployee(string empid, string firstName, string lastName, string salary, DateTime hiredate, string email, string department, DateTime birthdate, string address)
    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;
    //    string insertSql = "INSERT INTO Employee (EmployeeID, FirstName, LastName, Salary, HireDate, Email, Department, BirthDate, Address) VALUES (@EmployeeID, @FirstName, @LastName, @Salary, @HireDate, @Email, @Department, @BirthDate, @Address)";

    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(insertSql, con))
    //        {
    //            cmd.Parameters.AddWithValue("@EmployeeID", empid);
    //            cmd.Parameters.AddWithValue("@FirstName", firstName);
    //            cmd.Parameters.AddWithValue("@LastName", lastName);
    //            cmd.Parameters.AddWithValue("@Salary", salary);
    //            cmd.Parameters.AddWithValue("@HireDate", hiredate);
    //            cmd.Parameters.AddWithValue("@Email", email);
    //            cmd.Parameters.AddWithValue("@Department", department);
    //            cmd.Parameters.AddWithValue("@BirthDate", birthdate);
    //            cmd.Parameters.AddWithValue("@Address", address);

    //            // ... Add other parameters similarly

    //            con.Open();
    //            cmd.ExecuteNonQuery();
    //        }
    //    }
    //    Radgrid1.Rebind();
    //}

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex > 0)
        {
            BindDropDownState(ddlCountry.SelectedValue.ToString());
        }
        else
        {
            // No value selected in Country dropdown, clear State dropdown
            ddlState.Items.Clear();
            ddlCity.Items.Clear();
        }
        //Radgrid1.Visible = ddlCountry.SelectedIndex > 0 || ddlState.SelectedIndex > 0 || ddlCity.SelectedIndex > 0;

        BindCustomersAndGrid();
        Radgrid1.DataBind();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
        {
            BindDropDownCity(ddlState.SelectedValue.ToString());
        }
        else
        {
            // No value selected in Country dropdown, clear State dropdown
            ddlCity.Items.Clear();
        }

        //Radgrid1.Visible = ddlCountry.SelectedIndex > 0 || ddlState.SelectedIndex > 0 || ddlCity.SelectedIndex > 0;

        BindCustomersAndGrid();
        Radgrid1.DataBind();
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {

        //Radgrid1.Visible = ddlCountry.SelectedIndex > 0 || ddlState.SelectedIndex > 0 || ddlCity.SelectedIndex > 0;

        BindCustomersAndGrid();
        Radgrid1.DataBind();
    }
}
