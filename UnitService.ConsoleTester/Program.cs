using UnitService.Library.Models;

// Will have to define fraction later


Dimension dimension= new Dimension();
dimension.LengthExp = 1;
dimension.TimeExp = -1;

var t = Dimension.Parse("[Length]^3[Mass][Time]^(-1/3)");

Console.WriteLine(dimension.ToString());