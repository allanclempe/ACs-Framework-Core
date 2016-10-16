namespace ACs.Framework.Web.Core.Events
{
    public interface IEvent<in TIn, out TOut>
    {
        TOut DoIt(TIn model);
    }
}
