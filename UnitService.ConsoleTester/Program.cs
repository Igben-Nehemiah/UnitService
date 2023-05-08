using UnitService.Library.Models;

// Will have to define fraction later


Dimension dimension= new Dimension();
dimension.LengthExp = 1;
dimension.TimeExp = -1;

var t = Dimension.Parse(@"[Length]/[Time]/[Mass]");


Console.WriteLine(dimension == t);
Console.WriteLine(t.ToString());