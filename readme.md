# SlackNet
An easy-to-use and flexible API for writing Slack bots in .NET, built on top of a comprehensive Slack API client.

## Getting Started
There are three NuGet packages available to install, depending on your use case.
  - [SlackNet](https://www.nuget.org/packages/SlackNet/): A comprehensive Slack API client for .NET.
  - [SlackNet.Bot](https://www.nuget.org/packages/SlackNet.Bot/): Easy-to-use API for writing Slack bots.
  - [SlackNet.AspNetCore](https://www.nuget.org/packages/SlackNet.AspNetCore/): ASP.NET Core integration for receiving requests from Slack.

### SlackNet
To use the Web API:
```c#
var api = new SlackApiClient("<your API token here>");
```
then start calling methods:
```c#
var channels = await api.Channels.List();
```

To use the RTM API:
```c#
var rtm = new SlackRtmClient("<your API token here>");
await rtm.Connect();
rtm.Events.Subscribe(/* handle every event */);
rtm.Messages.Subscribe(/* handle message events */);
```

### SlackNet.Bot
```c#
var bot = new SlackBot("<your bot token here>");
await bot.Connect();
```
You can handle messages in a number of ways:
```c#
// .NET events
bot.OnMessage += (sender, message) => { /* handle message */ };

// Adding handlers
class MyMessageHandler: IMessageHandler { 
    public Task HandleMessage(IMessage message) {
        // handle message
    }
}
bot.AddHandler(new MyMessageHandler());

// Subscribing to Rx stream
bot.Messages.Subscribe(/* handle message */);
```
The easiest way to reply to messages is by using their `ReplyWith` method. Replies will be sent to same channel and thread as the message being replied to.
```c#
message.ReplyWith("simple text message");
message.ReplyWith(new BotMessage {
    Text = "more complicated message",
    Attachments = { new Attachment { Title = "attachments!" } }
});
message.ReplyWith(async () => {
    // Show typing indication in Slack while message is build built
    var asyncInfo = await SomeAsyncMethod();
    return new BotMessage { Text = "async message: " + asyncInfo };
});
```
SlackNet.Bot includes a simplified API for getting common information from Slack:
```c#
var user = await bot.GetUserByName("someuser");
var channels = await bot.GetChannels();
// etc.
```
Everything is cached, so go nuts getting the information you need. You can clear the cache with `bot.ClearCache()`.

### SlackNet.AspNetCore
In your Startup class:
```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddSlackNet(c => c.UseApiToken("<your API token here>"));
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseSlackNet(c => c.VerifyWith("<your verification token here>"));
}
```

See the `SlackNet.EventsExample` project for more detail.