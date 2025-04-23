using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wsCheckUsuario
{
	public partial class mpPrincipal : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            // Validacion de la sesion Activa
            if (Session["nomUsuario"].ToString() == "" &&
                Session["usuUsuario"].ToString() == "" &&
                Session["urlUsuario"].ToString() == "" &&
                Session["rolUsuario"].ToString() == "" )
            {
                //Mensaje de acceso denegado y enviar a wsAcceso.aspx
                Response.Write("<script language='javascript'> " +
                                         "alert('Acceso Denegado ...');" + "</script>");
                Response.Write("<script language= 'javascript' >" +
                    "document.location.href='wsAcceso.aspx';"
                    + "</script>");
            }


            // Actualizacion de etiquetas de la aplicacion
            Label1.Text = Application["nomEmpresa"].ToString();
            Label6.Text = Session["nomUsuario"].ToString() + " (" +
                          Session["usuUsuario"].ToString() + ") - " +
                          Session["rolUsuario"].ToString();
            //Configuracion de la foto del usuario en sesion
            Image2.ImageUrl = Session["urlUsuario"].ToString();


        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void ImageButton4_Click(object sender, ImageClickEventArgs e)
        {
            // Cargar la sesion del usuario
            Session["nomUsuario"] = "";
            Session["urlUsuario"] = "";
            Session["usuUsuario"] = "";
            Session["rolUsuario"] = "";
            Response.Write("<script language='javascript'> " +
                                          "alert('Sesion cerrada exitosamente !');" + "</script>");
            Response.Write("<script language= 'javascript' >" +
                "document.location.href='wsAcceso.aspx';"
                + "</script>");

        }
    }
}