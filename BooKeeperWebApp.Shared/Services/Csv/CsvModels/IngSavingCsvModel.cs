using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class IngSavingCsvModel
{
    [Column("Datum")]
    [DisplayFormat(DataFormatString = "yyyy-MM-dd")]
    public DateTime Date { get; set; }

    [Column("Omschrijving")]
    public string Description { get; set; }

    [Column("Rekening")]
    public string Account { get; set; }
    
    [Column("Rekening naam")]
    public string AccountName { get; set; }

    [Column("Tegenrekening")]
    public string OtherAccount { get; set; }

    [Column("Af Bij")]
    public string Direction { get; set; }

    [Column("Bedrag")]
    public double Amount { get; set; }

    [Column("Valuta")]
    public string Valuta { get; set; }

    [Column("Mutatiesoort")]
    public string MutationType { get; set; }

    [Column("Mededelingen")]
    public string Comment { get; set; }

    [Column("Saldo na mutatie")]
    public double AmountAfterMutation { get; set; }
}
