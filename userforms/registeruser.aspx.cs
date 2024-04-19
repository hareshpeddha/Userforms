using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Services.Description;
using System.Xml.Linq;

namespace userforms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            // Read the values entered by the user
            string Name = name.Text;
            string username = usrname.Text;
            string password = pwd.Text;
            string confirmPassword = confirmpwd.Text;

            // Perform validation, e.g., check if passwords match
            if (password != confirmPassword)
            {
                Response.Write("<script>alert('password miss  match')</script>");
                return;
            }

            // Assuming you have a SQL Server database connection string defined in the web.config file
            

            try
            {
                // Create a new SQL connection and command to insert the data into the database table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO Users (Name, Username, Password) VALUES (@Name, @Username, @Password)";
                    using (SqlCommand command = new SqlCommand(insertQuery, connection))
                    {
                        // Add parameters to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password);

                        // Execute the SQL command to insert the data into the table
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }

                // Optionally, you can redirect the user to a different page after successful registration
                // Response.Redirect("ThankYouPage.aspx");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during data insertion
                // You may log the error or display an error message to the user
            }
            
        }

        protected void clear_Click(object sender, EventArgs e)
        {
            // Clear the form fields
            name.Text = "";
            usrname.Text = "";
            pwd.Text = "";
            confirmpwd.Text = "";
        }

        protected void CONVERT_Click(object sender, EventArgs e)
        {

            try
            {
                // Step 1: Retrieve data from the database
                DataTable dataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT  top 1 * FROM users order by UserID desc";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataAdapter.Fill(dataTable);
                    }
                }

                // Step 2: Convert data to XML.
                string xmlString = ConvertDataTableToXml(dataTable);

                // Step 3: Convert XML to JSON....
                string jsonString = ConvertXmlToJson(xmlString);
                string filePath = Server.MapPath("~/App_Data/users.json");
                // Write the JSON string to a file
                File.AppendAllText(filePath, jsonString);

                // Optionally, you can use the JSON string (jsonString) as needed
                // For example, you can send it as a response or perform further processing

                // Example: Output JSON to console
                //Convert data to XML ti text local to remote 
                Console.WriteLine(jsonString);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during data retrieval or conversion
                Console.WriteLine("An error occurred: " + ex.Message);
            }

        }
        private string ConvertDataTableToXml(DataTable dataTable)
        {
            // Create a DataSet and add the DataTable to it
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            // Use a StringWriter to write the XML output
            StringWriter stringWriter = new StringWriter();
            dataSet.WriteXml(stringWriter, XmlWriteMode.WriteSchema); // WriteSchema includes schema information

            // Return the XML string
            return stringWriter.ToString();
        }
        private string ConvertXmlToJson(string xmlString)
        {
            // Parse the XML string into an XDocument
            XDocument xmlDoc = XDocument.Parse(xmlString);

            // Convert XDocument to JSON string using Newtonsoft.Json
            string jsonString = JsonConvert.SerializeXNode(xmlDoc);


            return jsonString;
        }
    }
}