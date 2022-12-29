/*
 * 
 * 
 * Copyright Ryan Callstrom 2022, All rights reserved
 * 
 * 
 */
using MessagePipe;

namespace CCW.GoW.DataService.Service;

public class MessageService
{
    readonly IDistributedPublisher<int, string> _publisher;
    public static readonly int _AddItem = 109391828, _RemoveItem = 109391829, _UpdateStatus = 109391830, _WriteLine = 109391831;

    public MessageService(IDistributedPublisher<int, string> publisher)
    {
        _publisher = publisher;
    }
    public async Task AddItem(string name) => await _publisher.PublishAsync(_AddItem, name);

    public async Task RemoveItem(string name) => await _publisher.PublishAsync(_RemoveItem, name);

    public async Task UpdateStatus(string status) => await _publisher.PublishAsync(_UpdateStatus, status);

    public async Task WriteLine(string message) => await _publisher.PublishAsync(_WriteLine, message);
}
