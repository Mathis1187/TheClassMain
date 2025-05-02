namespace TheClassMain.Service
{
    using TheClassMain.Model;

    /// <summary>
    /// Defines the <see cref="Session" />
    /// </summary>
    public static class Session
    {
        /// <summary>
        /// Gets or sets the CurrentCustomer
        /// </summary>
        public static Customer CurrentCustomer { get; set; }
    }
}
