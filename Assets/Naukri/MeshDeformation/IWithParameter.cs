namespace Naukri.MeshDeformation
{
    public interface IWithParameter<TParameter>
    {
        public DeformableObject target { get; }

        public TParameter parameters { get; }
    }
}
