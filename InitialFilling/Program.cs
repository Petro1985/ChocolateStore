using InitialFilling;

Console.WriteLine("DB initiation started");
var initialDb = new DbInitiator();
await initialDb.InitDB();
Console.WriteLine("DB initiation finished");