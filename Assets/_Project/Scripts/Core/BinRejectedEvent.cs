public readonly struct BinRejectedEvent
{
    public readonly BinType bin;

    public BinRejectedEvent(BinType bin)
    {
        this.bin = bin;
    }
}
