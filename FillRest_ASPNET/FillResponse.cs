using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillRest_ASPNET
{
  /// <summary>
  /// Classe per deserializzazione della risposta JSON del servizio FILL
  /// </summary>
    class DetailElem
    {
        public string IstatRegione { get; set; }
        public string Regione { get; set; }
        public string IstatProv { get; set; }
        public string IstatComune { get; set; }
        public string ProvEstesa { get; set; }
        public string CAB { get; set; }
        public string Catasto { get; set; }
        public string Belfiore { get; set; }
        public int CodComune { get; set; }
        public int CodStrada { get; set; }
        public string Dug { get; set; }
        public string Toponimo { get; set; }
        public string Civico { get; set; }
        public string ComuneMultiCap { get; set; }
        public string StradaMultiCap { get; set; }
    }

    class ShortElem
    {
        public string Comune { get; set; }
        public string Frazione { get; set; }
        public string Dug { get; set; }
        public string Toponimo { get; set; }
        public string Indirizzo { get; set; }
    }

    class CivElem
    {
        public string CivAdd { get; set; }
        public string CivShort { get; set; }
        public int NumCivico { get; set; }
        public string Esponente { get; set; }
        public string CasellaPostale { get; set; }
        public double KM { get; set; }
        public string Colore { get; set; }
        public string Presso { get; set; }
    }

    class AlterElem
    {
        public int Cap { get; set; }
        public int Comune { get; set; }
        public int Dug { get; set; }
        public int Frazione { get; set; }
        public int Prov { get; set; }
        public int Via { get; set; }
    }

    class GeocodElem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public string OMI { get; set; }
        public string Sez1991 { get; set; }
        public string Sez2001 { get; set; }
        public string Sez2011 { get; set; }
        public string Side { get; set; }
        public string Warning { get; set; }
    }

    class PostalFormatElem
    {
        public string Comune { get; set; }
        public string ComuneShort { get; set; }
        public string Dug { get; set; }
        public string DugCompl { get; set; }
        public string Frazione { get; set; }
        public string FrazioneShort { get; set; }
        public string IsFrazionePT { get; set; }
        public string IsStradaPT { get; set; }
        public string RigaAggiuntiva { get; set; }
        public string RigaEdificio { get; set; }
        public string RigaIndirizzo { get; set; }
        public string RigaLocalita { get; set; }
        public string Toponimo { get; set; }
        public string ToponimoShort { get; set; }
    }

    class FillCand
    {
        public string Prov { get; set; }
        public string Comune { get; set; }
        public string Frazione { get; set; }
        public string Cap { get; set; }
        public string Indirizzo { get; set; }
        public int ScoreComune { get; set; }
        public int ScoreStrada { get; set; }

        public DetailElem Detail { get; set; }
        public ShortElem Short { get; set; }
        public CivElem Civico { get; set; }
        public AlterElem Alter { get; set; }
        public GeocodElem Geocod { get; set; }
        public PostalFormatElem FormatoPostale { get; set; }
    }

    class FillResponse
    {
      
        public int Norm { get; set; }
        public int CodErr { get; set; }
        public int NumCand { get; set; }
        public List<FillCand> Output { get; set; }

    }
}
