using LMSApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LMSApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public BookController(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select BookId,BookName,BookPhoto,SectionName,BookCount,BookAuthor from dbo.BOOK";
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
        public JsonResult Post(Book book)
        {
            string query = @"insert into dbo.BOOK
            (BookName,BookPhoto,SectionName,BookCount,BookAuthor)
            values (
            '" + book.BookName + @"'
            ,'" + book.BookPhoto + @"'
            ,'" + book.SectionName + @"'
            ,'" + book.BookCount + @"'
            ,'" + book.BookAuthor + @"'
            
            )";
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
        public JsonResult Put(Book book)
        {
            string query = @"update dbo.BOOK set 
            BookName='" + book.BookName + @"'
           ,BookPhoto='" + book.BookPhoto + @"'
           ,SectionName='" + book.SectionName + @"'
           ,BookCount='" + book.BookCount + @"' 
           ,BookAuthor='" + book.BookAuthor + @"'
            where BookId=" + book.BookID + @"
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
            string query = @"delete from dbo.BOOK
            where BookId=" + id + @"
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
        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                String fileName = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + fileName;

                using (var stream=new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);

                }
                return new JsonResult(fileName);

            }
            catch(Exception)
            {
                return new JsonResult("anonymous.png");

            }
        

        }
        [Route("GetAllSectionNames")]
        //[HttpGet]
        public JsonResult GetAllSectionNames()
        {
            string query = @"select SectionName from dbo.BOOKSECTION";
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

    }
}
