using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Xml.Linq;
using WebApi.Models;

namespace WebApi.Data
{
    public class CedulaData
    {
        public static DataUser GetDataByCedula(String cedula)
        {
            CedulaResponse deserializedCedulaResponse = new CedulaResponse();
            DataUser dataUserResponse = null;

            if (string.IsNullOrEmpty(cedula) || string.IsNullOrWhiteSpace(cedula))
            {
                return dataUserResponse;
            }

            var url = $"https://cedulaprofesional.sep.gob.mx/cedula/buscaCedulaJson.action?json=%7B%27maxResult%27:%27100%27,%27nombre%27:%27%27,%27paterno%27:%27%27,%27materno%27:%27%27,%27idCedula%27:%27"+cedula+"%27%7D";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return dataUserResponse;
                        using (StreamReader objReader = new StreamReader(strReader, System.Text.Encoding.Default))
                        {
                            string responseBody = objReader.ReadToEnd();

                            deserializedCedulaResponse = JsonConvert.DeserializeObject<CedulaResponse>(responseBody);

                            if (deserializedCedulaResponse != null && deserializedCedulaResponse.items != null && deserializedCedulaResponse.items.Length > 0)
                            {
                                dataUserResponse = new DataUser()
                                {
                                    FullName = deserializedCedulaResponse.items[0].nombre + " " + deserializedCedulaResponse.items[0].paterno + " " + deserializedCedulaResponse.items[0].materno,
                                    Degree = deserializedCedulaResponse.items[0].titulo
                                };
                            }
                            return dataUserResponse;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return dataUserResponse;
            }
        }
    }
}