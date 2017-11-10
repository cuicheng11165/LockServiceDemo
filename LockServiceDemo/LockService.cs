namespace LockServiceDemo
{
    using System;
    using System.Threading.Tasks;

    public class LockService
    {
        private TimeSpan checkInterval;
        private TimeSpan expire;
        private Int32 lockCount;
        private readonly String lockName;

        private Int32 retryCount = 3;
        private TimeSpan retryInterval;

        public LockService(String lockName) : this(lockName, new TimeSpan(0, 0, 1, 0, 0), new TimeSpan(0, 0, 0, 0, 100))
        {
        }

        public LockService(String lockName, TimeSpan expire, TimeSpan checkInterval)
        {
            this.lockName = lockName;
            this.expire = expire;
            this.checkInterval = checkInterval;
        }

        internal ILockRepository LockRepository { private set; get; }

        private void Aquire()
        {
            if (this.LockResource())
            {
                this.lockCount++;
            }
        }

        private Boolean LockResource()
        {
            try
            {
                this.LockRepository.InsertUniqueKey(this.lockName, this.expire.Milliseconds);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void ExtendExpiration(TimeSpan time){
            
        }


        public static LockService CreateLock()
        {
            return new LockService("");
        }

        public static async Task<LockService> CreateLockAsync()
        {
            throw new NotImplementedException();
        }


        public void ReleaseLock()
        {
        }
    }
}