namespace ReSharp.Tests.Patterns.Command
{
    internal class Counter
    {
        #region Constructors

        public Counter(int count)
        {
            Count = count;
        }

        #endregion Constructors

        #region Properties

        public int Count
        {
            get;
            set;
        }

        #endregion Properties
    }
}