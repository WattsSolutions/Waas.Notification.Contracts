using Waas.Notification.Contracts.Sample;

Console.WriteLine("=================================================");
Console.WriteLine("  WaaS Notification Contracts — Sample Consumer");
Console.WriteLine("=================================================");
Console.WriteLine();
Console.WriteLine("Simulating events as they would arrive from the WaaS event bus.");
Console.WriteLine("In production, replace EventSimulator with your Azure Service Bus");
Console.WriteLine("or Event Grid receiver and pass the raw JSON payloads to EventDispatcher.");
Console.WriteLine();

foreach (var (eventType, json) in EventSimulator.GenerateSampleEvents())
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"► {eventType}");
    Console.ResetColor();

    EventDispatcher.Dispatch(eventType, json);

    Console.WriteLine();
}

Console.WriteLine("Done.");
