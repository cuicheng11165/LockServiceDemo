using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockServiceDemo
{
    using System.Threading;

    public class LockService
    {
        public LockService(string lockName)
        {

        }

        public bool TryLock()
        {
            return false;
        }



        public void ReleaseLock()
        {
        }

    }

    class LockHandler
    {
        private ILockRepository LockRepository { set; get; }

        public void Enter(string lockName)
        {
            while (this.LockRepository.Insert(lockName, 1000))
            {
                Thread.Sleep(1000);
            }
        }

        public void Release(string lockName)
        {
            this.LockRepository.Remove(lockName);
        }
    }


    interface ILockRepository
    {
        /// <summary>
        /// return true if success, false if fail.Make sure rowkey must be unique in target storage.
        /// </summary>
        /// <param name="rowKey"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        bool Insert(string rowKey, int timeout);

        /// <summary>
        /// return true if success
        /// </summary>
        /// <param name="rowKey"></param>
        void Remove(string rowKey);
    }


}
