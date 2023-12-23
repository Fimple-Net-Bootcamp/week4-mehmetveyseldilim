namespace VirtualPetCare.Data.Exceptions
{
    public abstract partial class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message)
        {

        }
        
    }
}