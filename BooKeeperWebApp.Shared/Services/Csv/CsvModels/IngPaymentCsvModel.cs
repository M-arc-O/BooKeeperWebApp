using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class IngPaymentCsvModel
{
    [Column("Datum")]
    [DisplayFormat(DataFormatString = "yyyyMMdd")]
    public DateTime Date { get; set; }

    [Column("Naam / Omschrijving")]
    public string Description { get; set; }

    [Column("Rekening")]
    public string Account { get; set; }

    [Column("Tegenrekening")]
    public string OtherAccount { get; set; }

    [Column("Code")]
    public string Code { get; set; }

    [Column("Af Bij")]
    public string Direction { get; set; }

    [Column("Bedrag (EUR)")]
    public double Amount { get; set; }

    [Column("Mutatiesoort")]
    public string MutationType { get; set; }

    [Column("Mededelingen")]
    public string Comment { get; set; }

    [Column("Saldo na mutatie")]
    public double AmountAfterMutation { get; set; }

    [Column("Tag")]
    public string Tag { get; set; }
}
