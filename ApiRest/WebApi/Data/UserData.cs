using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public static class UserData
    {
        private static String errorMessage = "Error";
        private static UserFlag validateUser(User user)
        {
            if (user.Name == "Cedula inexistente" || user.Degree == "Cedula inexistente")
            {
                user.Name = "";
                user.Degree = "";
                user.Cedula = "";
            }

            ValidationContext vc = new ValidationContext(user);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(user, vc, results, true);

            List<ValidationResult> listError = results.ToList();

            if (!isValid)
            {
                return new UserFlag()
                {
                    User = new User()
                    {
                        Company = LoopArray("Company", user.Company, listError),
                        Cedula = LoopArray("Cedula", user.Cedula, listError),
                        Name = LoopArray("Name", user.Name, listError),
                        Degree = LoopArray("Degree", user.Degree, listError),
                        Email = LoopArray("Email", user.Email, listError),
                        Phone = LoopArray("Phone", user.Phone, listError)
                    },
                    Message = errorMessage,
                    Error = true
                };
            }

            DataUser validateCedula = CedulaData.GetDataByCedula(user.Cedula);

            if (validateCedula != null)
            {
                if (user.Name != validateCedula.FullName)
                {
                    user.Name = "";
                }
                if (user.Degree != validateCedula.Degree)
                {
                    user.Degree = "";
                }
            }
            else
            {
                user.Cedula = "";
            }
            if (user.Cedula == "" || user.Name == "" || user.Degree == "")
            {
                return new UserFlag()
                {
                    User = user,
                    Message = errorMessage,
                    Error = true
                };
            }
            return null;
        }
        private static String LoopArray(String parameter, String value, List<ValidationResult> errorList)
        {
            foreach(ValidationResult error in errorList)
            {
                String name = error.MemberNames.First().ToString();
                if(name == parameter)
                {
                    return "";
                }
            }
            return value;
        }
        public static UserFlag RegisterUser(User user)
        {
            UserFlag userFlag = validateUser(user);
            if(userFlag != null)
            { 
                return userFlag; 
            }
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand command = new SqlCommand("insert into Users(Company, Cedula, Name, Degree, Email, Phone) values (@company,@cedula,@name,@degree,@email,@phone)", oConexion);
                command.Parameters.AddWithValue("@company", user.Company);
                command.Parameters.AddWithValue("@cedula", user.Cedula);
                command.Parameters.AddWithValue("@name", user.Name);
                command.Parameters.AddWithValue("@degree", user.Degree);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@phone", user.Phone);
                try
                {
                    oConexion.Open();
                    int result = command.ExecuteNonQuery();
                    if (result < 0)
                        return new UserFlag()
                        {
                            User = user,
                            Message = errorMessage,
                            Error = true
                        };
                    return new UserFlag()
                    {
                        User = user,
                        Message = "Datos guardados correctamente",
                        Error = false
                    };
            }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        return new UserFlag()
                        {
                            User = user,
                            Message = "Este usuario ya esta registrado",
                            Error = true
                        };
                    }
                    return new UserFlag()
                    {
                        User = user,
                        Message = errorMessage,
                        Error = true
                    };
                }
                catch (Exception ex)
                {
                    return new UserFlag()
                    {
                        User = user,
                        Message = errorMessage,
                        Error = true
                    };
                }
            }
        }
        public static List<User> ListarUsers()
        {
            List<User> oListaUsuario = new List<User>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.rutaConexion))
            {
                SqlCommand command = new SqlCommand("Select Company,Cedula, Name, Degree, Email, Phone from users", oConexion);
                try
                {
                    oConexion.Open();
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            oListaUsuario.Add(new User()
                            {
                                Company = dr["Company"].ToString(),
                                Cedula = dr["Cedula"].ToString(),
                                Name = dr["Name"].ToString(),
                                Degree = dr["Degree"].ToString(),
                                Email = dr["Email"].ToString(),
                                Phone = dr["Phone"].ToString()
                            });
                        }
                    }
                    return oListaUsuario;
                }
                catch (Exception ex)
                {
                    return oListaUsuario;
                }
            }
        }
    }
}

