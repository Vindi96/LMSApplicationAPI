using LMSApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select SectionId,SectionName from dbo.BOOKSECTION";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LMSAppCon");
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(query, mycon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();

                }

            }
            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Section sec)
        {
            string query = @"insert into dbo.BOOKSECTION values ('" + sec.SectionName + @"')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LMSAppCon");
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(query, mycon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();

                }

            }
            return new JsonResult("Added Successfully");




        }
        [HttpPut]
        public JsonResult Put(Section sec)
        {
            string query = @"update dbo.BOOKSECTION set SectionName='" + sec.SectionName + @"'
            where SectionId=" + sec.SectionID + @"
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LMSAppCon");
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(query, mycon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();


                }
            }
            return new JsonResult("Updated Successfully");



        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.BOOKSECTION
            where SectionId=" + id + @"
            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("LMSAppCon");
            SqlDataReader myReader;

            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycommand = new SqlCommand(query, mycon))
                {
                    myReader = mycommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();


                }
            }
            return new JsonResult("Deleted Successfully");



        }


    }
}
