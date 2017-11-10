namespace LockServiceDemo
{
    using System;
    using System.Threading.Tasks;

    public interface ILockRepository
    {
        /// <summary>
        ///     return true if success, false if fail or key already exists.
        /// </summary>
        void InsertUniqueKey(String rowKey, Int32 timeout);

        /// <summary>
        ///     return true if success, false if fail or key already exists.
        /// </summary>
        Task InsertUniqueKeyAsync(String rowKey, Int32 timeout);    

        /// <summary>
        ///     return true if success
        /// </summary>
        void RemoveKey(String rowKey);


        /// <summary>
        ///     return true if success
        /// </summary>
        Task RemoveKeyAsync(String rowKey);
    }
}