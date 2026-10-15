using System;

public static class EventBus
{
    private static class Channel<T>
    {
        public static Action<T> handlers;
    }

    public static void Subscribe<T>(Action<T> handler)
    {
        Channel<T>.handlers += handler;
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        Channel<T>.handlers -= handler;
    }

    public static void Publish<T>(T message)
    {
        Channel<T>.handlers?.Invoke(message);
    }
}
