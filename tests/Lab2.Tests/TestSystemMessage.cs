using Itmo.ObjectOrientedProgramming.Lab2.Addressee;
using Itmo.ObjectOrientedProgramming.Lab2.Archiver;
using Itmo.ObjectOrientedProgramming.Lab2.Formatter;
using Itmo.ObjectOrientedProgramming.Lab2.Loggers;
using Itmo.ObjectOrientedProgramming.Lab2.Messages;
using Itmo.ObjectOrientedProgramming.Lab2.Users;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab2.Tests;

public class TestSystemMessage
{
    [Fact]
    public void UserAndMessage_ShouldCheckMessageStatus_WhenMessageIsNotRead()
    {
        var message = new Message("Title", "Body", Priority.Low);
        var user = new User();

        user.ReceivedMessage(message);

        MessageIsReadResult res = user.IsRead(message.Id);
        Assert.True(res is MessageIsReadResult.IsNotRead);
    }

    [Fact]
    public void UserAndMessage_ShouldCheckMessageStatus_WhenMessageIsRead()
    {
        var message = new Message("Title", "Body", Priority.Low);
        var user = new User();

        user.ReceivedMessage(message);
        user.ToReadMessage(message.Id);

        MessageIsReadResult res = user.IsRead(message.Id);
        Assert.True(res is MessageIsReadResult.IsRead);
    }

    [Fact]
    public void UserAndMessage_ShouldReadMessage_WhenMessageIsAlreadyRead()
    {
        var message = new Message("Title", "Body", Priority.Low);
        var user = new User();

        user.ReceivedMessage(message);
        user.ToReadMessage(message.Id);
        MessageToReadResult res = user.ToReadMessage(message.Id);

        Assert.True(res is MessageToReadResult.MessageToIsAlreadyRead);
    }

    [Fact]
    public void AddresseeImportance_ShouldNoSendMessageUser_WhenMessagePriorityLessUserPriority()
    {
        IAddressee addressee = Substitute.For<IAddressee>();
        var filter = new AddresseeImportanceProxy(addressee, Priority.Medium);
        var message = new Message("Title", "Body", Priority.Low);

        filter.SendMessage(message);

        addressee.DidNotReceive().SendMessage(Arg.Any<Message>());
    }

    [Fact]
    public void MockAddresseeLog_ShouldLogMessage_WhenAddresseeLogDecorator()
    {
        ILogger logger = Substitute.For<ILogger>();
        var user = new User();
        var addresseeLogger = new AddresseeLogDecorator(new AddresseeUser(user), logger);
        var message = new Message("Title", "Body", Priority.Low);

        addresseeLogger.SendMessage(message);

        logger.Received(1).Log("TitleBody");
    }

    [Fact]
    public void FormatterArchiver_ShouldCallArchive_WhenMessageSaveInArchiver()
    {
        IFormatter formatter = Substitute.For<IFormatter>();
        var archiver = new FormatterArchiver(formatter);
        var message = new Message("Title", "Body", Priority.Low);

        archiver.Archive(message);

        formatter.Received(1).WriteTitle("Title");
        formatter.Received(1).WriteBody("Body");
    }

    [Fact]
    public void AddresseeImportance_ShouldUserReceievedMessage_WhenMessageHighAndLowPriority()
    {
        var message = new Message("Title", "Body", Priority.Medium);
        var user = new User();
        var addressee1 = new AddresseeImportanceProxy(new AddresseeUser(user), Priority.Low);
        var addressee2 = new AddresseeImportanceProxy(new AddresseeUser(user), Priority.High);

        addressee1.SendMessage(message);
        addressee2.SendMessage(message);
        MessageToReadResult res = user.ToReadMessage(message.Id);

        Assert.True(res is not MessageToReadResult.MessageToNotFound);
    }
}