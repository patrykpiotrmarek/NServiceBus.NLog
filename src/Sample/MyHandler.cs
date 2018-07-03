using System.Threading.Tasks;
using NServiceBus_6;
using NServiceBus_6.Logging;

public class MyHandler : IHandleMessages<MyMessage>
{
    static ILog logger = LogManager.GetLogger(typeof(MyHandler));

    public Task Handle(MyMessage message, IMessageHandlerContext context)
    {
        logger.Info("Hello from MyHandler");
        return Task.FromResult(0);
    }
}