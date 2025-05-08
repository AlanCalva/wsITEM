using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wsCheckUsuario.Models;
using System.Text;

namespace wsCheckUsuario
{
	public partial class Formulario_web2 : System.Web.UI.Page
	{
		protected async void Page_Load(object sender, EventArgs e)
		{

            // Validacion de carga de pagina (postBack)
            if (Page.IsPostBack == false)
            {
                //Llamada para ejecucion del metodo
                await cargaDatosTipoUsuario();
                
            }

            


        }

        // Creación del método asíncrono para ejecutar el
        // endpoint vwTipoUsuario
        private async Task cargaDatosTipoUsuario()
        {



            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración de la peticion HTTP
                    string apiUrl = "https://localhost:44337/check/usuario/vwTipoUsuario";
                    // Ejecución del endpoint
                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();

                    // Validación del estatus OK
                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);
                        // ------------------------------------------
                        JArray jsonArray = (JArray)objRespuesta.datos["vwTipoUsuario"];
                        // Convertir JArray a DataTable
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonArray.ToString());
                        // -------------------------------------------
                        // Visualización de los datos formateados DropDownList
                        DropDownList1.DataSource = dt;
                        DropDownList1.DataTextField = "descripcion";
                        DropDownList1.DataValueField = "clave";
                        DropDownList1.DataBind();
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        // Creación del método asíncrono para ejecutar el
        // endpoint spInsUsuario
        private async Task cargaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""nombre"":""" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44337/check/usuario/spinusuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario registrado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El nombre de usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El tipo de usuario no existe');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //Nombre
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                              "alert('El nombre esta vacío');" +
                              "</script>");
            }
            else
            {
                //Apellido paterno
                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                  "alert('El apellido paterno esta vacío');" +
                                  "</script>");
                }
                else
                {
                    //Apellido materno
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                      "alert('El apellido materno esta vacío');" +
                                      "</script>");
                    }
                    else
                    {
                        //Uusario
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                          "alert('El usuario esta vacío');" +
                                          "</script>");
                        }
                        else
                        {
                            //Contraseña
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                              "alert('La contraseña esta vacío');" +
                                              "</script>");
                            }
                            else
                            {
                                //Ruta Foto
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                  "alert('La ruta esta vacío');" +
                                                  "</script>");
                                }
                                else
                                {
                                    //Ejecucion asincrona del metodo de insercion de usuarios
                                    await cargaDatos();

                                }

                            }

                        }

                    }

                }

            }

               

        }


        private async Task buscarUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string clave = TextBox8.Text.Trim();
                    string apiUrl = "https://localhost:44337/check/usuario/spbususuario?clave=" + clave;

                    HttpResponseMessage respuesta = await client.GetAsync(apiUrl);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        JObject objRespuesta = JsonConvert.DeserializeObject<JObject>(resultado);

                        if (objRespuesta["datos"] != null && objRespuesta["datos"]["spBusUsuario"] != null)
                        {
                            JArray jsonArray = (JArray)objRespuesta["datos"]["spBusUsuario"];

                            if (jsonArray.Count > 0)
                            {
                                JObject usuario = (JObject)jsonArray[0];

                                TextBox1.Text = usuario["USU_CVE_USUARIO"]?.ToString();
                                TextBox2.Text = usuario["USU_NOMBRE"]?.ToString();
                                TextBox3.Text = usuario["USU_APELLIDO_PATERNO"]?.ToString();
                                TextBox4.Text = usuario["USU_APELLIDO_MATERNO"]?.ToString();
                                TextBox5.Text = usuario["USU_USUARIO"]?.ToString();
                                TextBox6.Text = usuario["USU_CONTRASENA"]?.ToString();
                                TextBox7.Text = usuario["USU_RUTA"]?.ToString();
                                DropDownList1.SelectedValue = usuario["TIP_CVE_TIPOUSUARIO"]?.ToString();
                            }
                            else
                            {
                                Response.Write("<script>alert('Usuario no encontrado');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('No se encontraron datos en la respuesta.');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('Error al conectarse con la API');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error inesperado: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }





        protected async void ImageButton1_Click1(object sender, ImageClickEventArgs e)
        {
            await buscarUsuario();
        }


        //ENDPOINT PARA ACTUALIZAR DATOS
        private async Task actualizaDatos()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""cve"":""" + TextBox1.Text + "\"," +
                                  "\"nombre\":\"" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44337/check/usuario/spupdusuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario actualizado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('CLAVE de usuario NO existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 2)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('El usuario ya existe');" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 3)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Nombre de usuario ya en uso');" +
                                           "</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            //Nombre
            if (TextBox2.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                              "alert('El nombre esta vacío');" +
                              "</script>");
            }
            else
            {
                //Apellido paterno
                if (TextBox3.Text == "")
                {
                    Response.Write("<script language='javascript'>" +
                                  "alert('El apellido paterno esta vacío');" +
                                  "</script>");
                }
                else
                {
                    //Apellido materno
                    if (TextBox4.Text == "")
                    {
                        Response.Write("<script language='javascript'>" +
                                      "alert('El apellido materno esta vacío');" +
                                      "</script>");
                    }
                    else
                    {
                        //Uusario
                        if (TextBox5.Text == "")
                        {
                            Response.Write("<script language='javascript'>" +
                                          "alert('El usuario esta vacío');" +
                                          "</script>");
                        }
                        else
                        {
                            //Contraseña
                            if (TextBox6.Text == "")
                            {
                                Response.Write("<script language='javascript'>" +
                                              "alert('La contraseña esta vacío');" +
                                              "</script>");
                            }
                            else
                            {
                                //Ruta Foto
                                if (TextBox7.Text == "")
                                {
                                    Response.Write("<script language='javascript'>" +
                                                  "alert('La ruta esta vacío');" +
                                                  "</script>");
                                }
                                else
                                {
                                    //Ejecucion asincrona del metodo de insercion de usuarios
                                    await actualizaDatos();

                                }

                            }

                        }

                    }

                }

            }

        }


        //ENDPONT PARA ELIMINAR 
        private async Task eliminarUsuario()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Configuración del Json que se enviará
                    String data = @"{
                                  ""cve"":""" + TextBox1.Text + "\"," +
                                  "\"nombre\":\"" + TextBox2.Text + "\"," +
                                  "\"apellidoPaterno\":\"" + TextBox3.Text + "\"," +
                                  "\"apellidoMaterno\":\"" + TextBox4.Text + "\"," +
                                  "\"usuario\":\"" + TextBox5.Text + "\"," +
                                  "\"contrasena\":\"" + TextBox6.Text + "\"," +
                                  "\"ruta\":\"" + TextBox7.Text + "\"," +
                                  "\"tipo\":\"" + DropDownList1.SelectedValue + "\"" +
                                  "}";
                    // Configuración del contenido del <body> a enviar
                    HttpContent contenido = new StringContent
                                (data, Encoding.UTF8, "application/json");
                    // Ejecución de la petición HTTP
                    string apiUrl = "https://localhost:44337/check/usuario/spdelusuario";
                    // ----------------------------------------------
                    HttpResponseMessage respuesta =
                        await client.PostAsync(apiUrl, contenido);
                    // ---------------------------------------------------
                    // Validación de recepción de respuesta Json
                    clsApiStatus objRespuesta = new clsApiStatus();
                    // ---------------------------------------------------

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado =
                                await respuesta.Content.ReadAsStringAsync();
                        objRespuesta = JsonConvert.DeserializeObject<clsApiStatus>(resultado);

                        // Bandera de estatus del proceso
                        if (objRespuesta.ban == 0)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario eliminado exitosamente');" +
                                           "</script>");
                            Response.Write("<script language='javascript'>" +
                                           "document.location.href='Formulario web2.aspx';" +
                                           "</script>");
                        }
                        if (objRespuesta.ban == 1)
                        {
                            Response.Write("<script language='javascript'>" +
                                           "alert('Usuario no encontrado');" +
                                           "</script>");
                        }
                       
                    }
                    else
                    {
                        Response.Write("<script language='javascript'>" +
                                       "alert('Error de conexión con el servicio');" +
                                       "</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script language='javascript'>" +
                               "alert('Error de la aplicación, intentar nuevamente');" +
                               "</script>");
            }
        }

        protected async void Button3_Click(object sender, EventArgs e)
        {
            //buscar clave
            if (TextBox8.Text == "")
            {
                Response.Write("<script language='javascript'>" +
                              "alert('La clave esta vacía No puedo eliminar');" +
                              "</script>");
            }
            else
            {
               
                   //Ejecucion asincrona del metodo de insercion de usuarios
                    await eliminarUsuario();

                                
            }

        }




    }
}