using System.Data;
using System.Security.AccessControl;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using LoginRegistrarApp.Models;

namespace LoginRegistrarApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RegistrarController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public RegistrarController(IConfiguration _configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registrar")]

        public string registrar(Registrar registrar)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StringConexaoSQLServer").toString());

            SqlCommand cmd = new SqlCommand("INSERT INTO Registrar(UserName,Password,Email,IsActive) VALUES('" + registrar.UserName+ "','" + registrar.Password+ "', '" + registrar.Email+ "', '" + registrar.IsActive+ "')", connection);
        
            connection.Open();
            int i = cmd.ExcuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                return "Dados Inseridos";
            } 
            else
            {
                return "Erro ao inserir dados";
            }
        }

        [HttpPost]
        [Route("login")]
        public string login(Registrar registrar)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StringConexaoSQLServer").toString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Registrar WHERE Email = '"+registrar.Email+"' AND Password = '"+registrar.Password+"' AND IsActive = 1 ", connection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            if(dataTable.Rows.Count > 0)
            {
                return "Dados encontrados!";
            }
            else 
            {
                return "Usuario Invalido";
            }
        }
    }
}