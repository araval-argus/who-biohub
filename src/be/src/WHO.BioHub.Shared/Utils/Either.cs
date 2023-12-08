namespace WHO.BioHub.Shared.Utils;

public class Either<L, R>
{
    private readonly L? _l;
    private readonly R? _r;

    private readonly bool _isLeft;

    public Either(L l)
    {
        _l = l;
        _r = default;
        _isLeft = true;
    }

    public Either(R r)
    {
        _r = r;
        _l = default;
        _isLeft = false;
    }

    public bool IsLeft => _isLeft;
    public bool IsRight => !IsLeft;

    public L? Left => _l;
    public R? Right => _r;
}