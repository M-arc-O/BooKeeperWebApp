using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BooKeeperWebApp.Shared.Services.Csv.CsvModels;
public class RegiobankCsvModel
{
    [Column(Order = 0)]
    [DisplayFormat(DataFormatString = "dd-MM-yyyy")]
    public DateTime Date { get; set; }

    [Column(Order = 1)]
    public string Account { get; set; }

    [Column(Order = 2)]
    public string OtherAccount { get; set; }

    [Column(Order = 3)]
    public string Description { get; set; }

    [Column(Order = 8)]
    public double AmountBeforeMutation { get; set; }

    [Column(Order = 10)]
    public double Amount { get; set; }
    
    [Column(Order = 17)]
    public string Comment { get; set; }
}
