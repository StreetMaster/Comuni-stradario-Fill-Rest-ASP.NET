using RestSharp;
using System;

namespace FillRest_ASPNET
{
    /// <summary>
    /// Esempio di utilizzo del servizio WS FILL per la verifica e la normalizzazione degli indirizzi italiani 
    /// 
    /// L'end point del servizio è 
    ///     https://streetmaster.streetmaster.it/smrest/webresources/fill
    ///     
    /// Per l'utilizzo registrarsi sul sito http://streetmaster.it e richiedere la chiave per il servizio FILL 
    /// Il protocollo di comunicazione e' in formato JSON
    /// Per le comunicazioni REST è utilizzata la libreria opensource RestSharp (http://restsharp.org/)
    /// 
    ///  2016 - Software by StreetMaster (c)
    /// </summary>
    /// 
    public partial class DemoFill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCallVerify_Click(object sender, EventArgs e)
        {

            outArea.Style["Border"] = "none";
            outArea.Style["Border-color"] = "#336600";


            // inizializzazione client del servizio FILL
            var clientFill = new RestSharp.RestClient();
            clientFill.BaseUrl = new Uri("https://streetmaster.streetmaster.it");

            var request = new RestRequest("smrest/webresources/fill", Method.GET);
            request.RequestFormat = DataFormat.Json;

            // valorizzazione input
            // valorizzazione input
            // per l'esempio viene valorizzato un insieme minimo dei parametri
            request.AddParameter("Key", txtKey.Text);
            request.AddParameter("Localita", txtComune.Text);
            request.AddParameter("Cap", txtCap.Text);
            request.AddParameter("Provincia", txtProv.Text);
            request.AddParameter("Indirizzo", txtIndirizzo.Text);
            request.AddParameter("Localita2", txtFrazione.Text);
            request.AddParameter("Dug", String.Empty);
            request.AddParameter("Civico", String.Empty);

            // chiamata al servizio
            var response = clientFill.Execute<FillResponse>(request);
            var outCall = response.Data;


            // output
            if (outCall.Norm==1)
            {
                // verifica OK
                txtCap.Text = outCall.Output[0].Cap;
                txtProv.Text= outCall.Output[0].Prov;
                txtComune.Text = outCall.Output[0].Comune;
                txtFrazione.Text = outCall.Output[0].Frazione;
                txtIndirizzo.Text = outCall.Output[0].Indirizzo;
                string htmlOut = "<p><font color=\"green\">INDIRIZZO VALIDO</font></p>";

                // e' riportato in output solo un sottoinsieme di esempio di tutti i valori restituiti

                htmlOut += "<table>";

                htmlOut += "<tr><td>Regione</td>";
                htmlOut += "<td>" + outCall.Output[0].Detail.Regione +  "</td></tr>"; ;

                htmlOut += "<tr><td>Istat Prov</td>";
                htmlOut += "<td>" + outCall.Output[0].Detail.IstatProv + "</td></tr>"; ;

                htmlOut += "<tr><td>Istat Comune</td>";
                htmlOut += "<td>" + outCall.Output[0].Detail.IstatComune + "</td></tr>"; ;

                htmlOut += "<tr><td>X</td>";
                htmlOut += "<td>" + outCall.Output[0].Geocod.X + "</td></tr>"; ;

                htmlOut += "<tr><td>Y</td>";
                htmlOut += "<td>" + outCall.Output[0].Geocod.Y + "</td></tr>"; ;


                htmlOut += "</table>";
                outArea.InnerHtml = htmlOut;
            }
            else
            {
                // verifica KO, gestione errore

                // errore di licenza
                if (outCall.CodErr == 997)
                    outArea.InnerHtml = "<p><font color=\"red\">LICENSE KEY NON RICONOSCIUTA</font></p>";
                else if (outCall.CodErr == 123)
                    outArea.InnerHtml = "<p><font color=\"red\">NON E' STATO VALORIZZATO IL COMUNE</font></p>";
                else if (outCall.CodErr == 124)
                    outArea.InnerHtml = "<p><font color=\"red\">COMUNE\\FRAZIONE NON RICONOSCIUTO</font></p>";
                else if (outCall.CodErr == 125)
                {
                    String htmlOut= "<p><font color=\"red\">COMUNE\\FRAZIONE AMBIGUO</font></p>";

                    htmlOut += "<table>";
                    foreach (FillCand outElem in outCall.Output)
                    {
                        htmlOut += "<tr><td>";

                        htmlOut += outElem.Cap + " "+ outElem.Comune+ " " + outElem.Prov;
                        if (outElem.Frazione != string.Empty)
                            htmlOut += " - " + outElem.Frazione;
                        htmlOut += "</td></tr>";
                    }
                    htmlOut += "</table>";
                    outArea.InnerHtml = htmlOut;
                }
                else if (outCall.CodErr == 466)
                {
                    txtCap.Text = outCall.Output[0].Cap;
                    txtProv.Text = outCall.Output[0].Prov;
                    txtComune.Text = outCall.Output[0].Comune;
                    txtFrazione.Text = outCall.Output[0].Frazione;
                    outArea.InnerHtml = "<p><font color=\"red\">NON E' STATO VALORIZZATO L'INDIRIZZO</font></p>";
                }
                else if (outCall.CodErr == 467)
                {
                    txtCap.Text = outCall.Output[0].Cap;
                    txtProv.Text = outCall.Output[0].Prov;
                    txtComune.Text = outCall.Output[0].Comune;
                    txtFrazione.Text = outCall.Output[0].Frazione;
                    outArea.InnerHtml = "<p><font color=\"red\">INDIRIZZO NON RICONOSCIUTO</font></p>";
                }
                else if (outCall.CodErr == 468)
                {
                    txtCap.Text = outCall.Output[0].Cap;
                    txtProv.Text = outCall.Output[0].Prov;
                    txtComune.Text = outCall.Output[0].Comune;
                
                    String htmlOut = "<p><font color=\"red\">INDIRIZZO AMBIGUO</font></p>";
                    htmlOut += "<table>";
                    foreach (FillCand outElem in outCall.Output)
                    {
                        htmlOut += "<tr><td>";

                        htmlOut += outElem.Cap + " " + outElem.Indirizzo;
                        if (outElem.Frazione != string.Empty)
                            htmlOut += " (" + outElem.Frazione + ")";
                        htmlOut += "</td></tr>";
                    }
                    htmlOut += "</table>";
                    outArea.InnerHtml = htmlOut;
                }
                else if (outCall.CodErr == 455)
                {
                    txtCap.Text = outCall.Output[0].Cap;
                    txtProv.Text = outCall.Output[0].Prov;
                    txtComune.Text = outCall.Output[0].Comune;
                    txtFrazione.Text = outCall.Output[0].Frazione;
                    txtIndirizzo.Text = outCall.Output[0].Indirizzo;
                    outArea.InnerHtml = "<p><font color=\"red\">CAP INCONGRUENTE SU VIA MULTICAP</font></p>";
                }
            }
            outArea.Style["Border"] = "groove";
        }
    }
}