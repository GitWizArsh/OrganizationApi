using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrganizationApplication.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Mvc.Routing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata.Ecma335;

namespace OrganizationApplication.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        private readonly CompanyContext context;

        public CompanyController(IConfiguration configuration, CompanyContext companyContext)
        {
            _configuration = configuration;
                context = companyContext;
        }
        //[HttpGet("{id}")}]
        //[Route("GetCompanyById")]
        //public string GetCompanyByID(long id,Company company)
        //{
        //    using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
        //    {
        //        String query = "SELECT * FROM Firm WHERE CompanyId = @CompanyId";
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            //command.Parameters.AddWithValue("@Companyid", company.CompanyId);
        //            command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
        //            command.Parameters.AddWithValue("@CompanyAddress", company.CompanyAddress);
        //            command.Parameters.AddWithValue("@PhoneNumber", company.PhoneNumber);
        //            command.Parameters.AddWithValue("@CompanyId", company.CompanyId);
        //            //parameter.SourceVersion = DataRowVersion.Original;

        //            connection.Open();
        //            int result = command.ExecuteNonQuery();

        //            // Check Error
        //            if (result < 0)
        //                Console.WriteLine("404 Error data not found!");
        //        }
        //    }
        //    return "Data Located";

        //}



        [HttpGet]
        [Route("GetCompanies")]
        public string GetCompany()
        {
            SqlConnection cn = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Firm", cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Company> Companylist = new List<Company>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Company company = new Company();
                    company.CompanyId = Convert.ToInt32(dt.Rows[i]["CompanyId"]);
                    company.CompanyName = dt.Rows[i]["CompanyName"].ToString();
                    company.CompanyAddress = dt.Rows[i]["CompanyAddress"].ToString();
                    company.PhoneNumber = dt.Rows[i]["CompanyPhone"].ToString();
                    Companylist.Add(company);
                }

            }
            if (Companylist.Count > 0)
            {
                return JsonConvert.SerializeObject(Companylist);
            }
            else
            {
                response.StatusCode = 404;
                response.ErrorMessage = "Data not found";
                return JsonConvert.SerializeObject(response);
            }


        }
        [HttpPost]
        [Route("PostCompanies")]
        public string PostCompany(Company company)
        {


            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
            {
                String query = "INSERT INTO Firm(CompanyName, CompanyAddress, CompanyPhone) VALUES (@Name, @Address, @Phone)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", company.CompanyName);
                    command.Parameters.AddWithValue("@Address", company.CompanyAddress);
                    command.Parameters.AddWithValue("@Phone", company.PhoneNumber);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }
            }


            //// Create the InsertCommand.
            //SqlCommand command = new SqlCommand(
            //    "INSERT INTO Firm(CompanyName, CompanyAddress, CompanyPhone) VALUES ('PP', @CompanyAddress, @CompanyPhone)", cn);

            //// Add the parameters for the InsertCommand.
            // command.Parameters.Add("@CompanyName", SqlDbType.VarChar,100, company.CompanyName);
            //command.Parameters.Add("@CompanyAddress", SqlDbType.VarChar, 100, company.CompanyAddress);
            //command.Parameters.Add("@CompanyPhone",SqlDbType.Int, company.PhoneNumber);
            //cn.Open();
            //adapter.InsertCommand = command;
            //var rowsAffected = adapter.InsertCommand.ExecuteNonQuery();
            //cn.Close();
            return "Record has been inserted";

            //cn.Open();
            //int i = command.ExecuteNonQuery();
            //cn.Close();

            //if (i > 0)
            //{
            //    return "Record has been inserted";
            //}
            //else
            //{
            //    response.StatusCode = 300;
            //    response.ErrorMessage = "Record cannot be inserted";
            //    return JsonConvert.SerializeObject(response);
            //}

            //return JsonConvert.SerializeObject(adapter);
        }

        [HttpPut]
        [Route("UpdateCompanies")]
        public String PutCompany(Company company)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
            {
                String query = "UPDATE Firm SET CompanyName = @CompanyName, CompanyAddress = @CompanyAddress, CompanyPhone = @PhoneNumber WHERE CompanyId = @CompanyId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@Companyid", company.CompanyId);
                    command.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                    command.Parameters.AddWithValue("@CompanyAddress", company.CompanyAddress);
                    command.Parameters.AddWithValue("@PhoneNumber", company.PhoneNumber);
                    command.Parameters.AddWithValue("@CompanyId", company.CompanyId);
                    //parameter.SourceVersion = DataRowVersion.Original;

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error updating data!");
                }

            }
            return "Record has been Updated";

        }
        [HttpDelete]
        [Route("RemoveCompany")]
        public String DeleteCompany(long id, Company company)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
            {
                String query = "DELETE FROM Firm WHERE CompanyId = @Companyid";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Companyid", company.CompanyId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error deleting record!");
                }
            }
            return "Record has been Deleted";

        }
        [HttpPost]
        [Route("AddNewUsers")]
        public string AddUsers(User user)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
            {
                String query = "INSERT INTO Usr(FirstName, LastName, UserAddress, UserEmail, PhoneNumber) VALUES (@FirstName, @LastName, @Address, @Email, @PhoneNumber)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Address", user.UserAddress);
                    command.Parameters.AddWithValue("@Email", user.UserEmail);
                    command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        return "Error inserting data into Database!";
                }

            }
            return "Record has been Inserted";

        }
        [HttpPost]
        [Route("UserCompany")]
        public string AddUserCompany(UserCompany userCompany)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("CompConnection").ToString()))
            {
                string query = "INSERT INTO UserCompany(CompanyId, UserId, FromDate, ToDate) VALUES (@CompanyId, @UserId, @FromDate, @ToDate)";

                using (SqlCommand command = new SqlCommand(query, connection)) 
                {
                    command.Parameters.AddWithValue("@CompanyId", userCompany.CompanyId);
                    command.Parameters.AddWithValue("@UserId", userCompany.UserId);
                    command.Parameters.AddWithValue("@FromDate", userCompany.FromDate);
                    command.Parameters.AddWithValue("@ToDate", userCompany.ToDate);

                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        return "Error inserting data into Database!";
                }
            }
            return "Record has been Inserted";

        }
    }
}
