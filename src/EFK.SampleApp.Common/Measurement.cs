namespace EFK.SampleApp.Common;

using System.ComponentModel.DataAnnotations;

public class Measurement
{
    [Key]
    public Guid Id { get; set; }

    public DateTime Timestamp { get; set; }

    public double Value { get; set; }
}
