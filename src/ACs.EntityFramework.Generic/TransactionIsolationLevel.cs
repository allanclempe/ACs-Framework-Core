﻿namespace ACs.EntityFramework.Generic
{
    public enum TransactionIsolationLevel
    {
		Unspecified = -1,
		Chaos = 16,
		ReadUncommitted = 256,
		ReadCommitted = 4096,
		RepeatableRead = 65536,
		Serializable = 1048576,
		Snapshot = 16777216,
	}
}
